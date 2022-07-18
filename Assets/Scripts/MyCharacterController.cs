using ECM.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyCharacterController : BaseCharacterController
{
    Camera cam;
    public Interactable focus;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Space(10)]
    [SerializeField]
    private bool canJump = false;
    private bool canMove = true;

    private void Start()
    {
        cam = Camera.main;
    }

    protected override void Animate()
    {
        // If there is no animator, return

        if (animator == null)
            return;

        if (!canMove) return;

        // Compute move vector in local space

        var move = transform.InverseTransformDirection(moveDirection);

        // Update the animator parameters
        
        var forwardAmount = move.z;

        if (forwardAmount > 0)
        {
            FootstepManager.PlaySound();
        }
        else
        {
            FootstepManager.StopSound();
        }

        animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", Mathf.Atan2(move.x, move.z), 0.1f, Time.deltaTime);

        animator.SetBool("OnGround", movement.isGrounded);

        animator.SetBool("Crouch", isCrouching);

        if (!movement.isGrounded)
            animator.SetFloat("Jump", movement.velocity.y, 0.1f, Time.deltaTime);

        // Calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)

        var runCycle = Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime + 0.2f, 1.0f);
        var jumpLeg = (runCycle < 0.5f ? 1.0f : -1.0f) * forwardAmount;

        if (movement.isGrounded)
            animator.SetFloat("JumpLeg", jumpLeg);
    }

    protected override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                LayerMask playerLayer = gameObject.layer;

                if (Physics.Raycast(ray, out hit, 100, playerLayer))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    SetFocus(interactable);
                }
            }
            
        }
    }

    protected override void Jump()
    {
        if (canJump) base.Jump();
    }

    protected override void MidAirJump()
    {
        if (canJump) base.MidAirJump();
    }
    protected override void UpdateRotation()
    {
        if (canMove) base.UpdateRotation();
    }

    private void SetFocus(Interactable newFocus)
    {
        if (focus != newFocus)
        {
            focus?.OnDefocused();
            focus = newFocus;
        }
        focus?.OnFocused(transform);
    }

    public void DisableMovement()
    {
        canMove = false;
        movement.Pause(true);
        FootstepManager.StopSound();
        animator.SetFloat("Forward", 0);
    }

    public void EnableMovement()
    {
        movement.Pause(false, false);
        canMove = true;
    }
}
