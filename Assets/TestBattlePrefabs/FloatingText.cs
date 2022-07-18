using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 1.0f;
    private float offsetScale = 0.15f;
    private Vector3 targetPosition;
    private float animSpeed = 5f;

    void Start()
    {
        var camera = GameObject.Find("Main Camera");
        var distance = Vector3.Distance(camera.transform.position, transform.position);
        transform.position += new Vector3(0, 2f, 0);
        targetPosition = transform.position + new Vector3(0, 1f + offsetScale * distance, -distance * offsetScale);
        transform.rotation = camera.transform.rotation;
        GetComponent<TextMesh>().fontSize *= (int) distance / 3;
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        transform.localPosition += (targetPosition - transform.localPosition) * animSpeed * Time.deltaTime;
    }
}
