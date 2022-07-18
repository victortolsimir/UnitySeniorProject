using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    private bool isFocus = false;
    private bool hasInteracted = false;
    protected Transform player;


    private void Update()
    {
        
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            
            if (distance <= radius)
            {
                Interact();
            }
            hasInteracted = true;
        }
    }


    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ChangeCursor();
        }
    }

    public virtual void ChangeCursor(){}

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }


    public virtual void Interact()
    {
        Debug.Log($"Interacting with {transform.name}");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
