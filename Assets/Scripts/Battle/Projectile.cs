using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 moveTargetPosition;
    private Action onAnimComplete;
    private bool moving = false;

    private void Update()
    {
        if (moving)
        {
            float animSpeed = 5f;
            transform.position += (moveTargetPosition - GetPosition()) * animSpeed * Time.deltaTime;

            float reachedDistance = 1.5f;
            if (Vector3.Distance(GetPosition(), moveTargetPosition) < reachedDistance)
            {
                moving = false;
                onAnimComplete?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    private Vector3 GetPosition() => transform.position;

    public void MoveToPosition(Vector3 moveTargetPosition, Action onAnimComplete)
    {
        this.moveTargetPosition = moveTargetPosition;
        this.onAnimComplete = onAnimComplete;
        moving = true;
    }
}
