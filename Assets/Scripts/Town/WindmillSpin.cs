using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillSpin : MonoBehaviour
{
    [SerializeField]

    private GameObject spinThing;

    public float time = 0.1f;

    private void Start()
    {
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        Quaternion startRotation = spinThing.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(new Vector3(0, 0, 5)) * startRotation;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            spinThing.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / time);
            yield return null;
        }
        spinThing.transform.rotation = endRotation;

        StartCoroutine(Spin());
    }

}
