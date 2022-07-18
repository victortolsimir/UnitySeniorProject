using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkForward : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private string walkAnimationName = "Walk Forward";
    [SerializeField]
    private float animSpeed = 1f;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.speed = animSpeed;
    }

    public void StartWalk()
    {
        animator.SetBool(walkAnimationName, true);
    }

    public void StopWalk()
    {
        animator.SetBool(walkAnimationName, false);
    }
}
