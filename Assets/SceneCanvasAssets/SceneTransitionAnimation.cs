using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionAnimation : MonoBehaviour
{
    private static SceneTransitionAnimation instance;

    [SerializeField]
    private float countdown = 0.3f;

    private IEnumerator coroutine;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private IEnumerator Start()
    {
        GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(countdown);
        FadeIn();
    }

    public static void PlayFadeIn()
    {
        if (instance)
            instance.FadeIn();
    }

    private void FadeIn()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = FadeInCoroutine();
        StartCoroutine(coroutine);
    }

    private IEnumerator FadeInCoroutine()
    {
        var image = GetComponent<Image>();
        var color = image.color;

        while (color.a > 0)
        {
            color.a -= Time.deltaTime / 2f;
            image.color = color;
            yield return null;
        }
    }

    public static void PlayFadeOut()
    {
        if (instance)
            instance.FadeOut();
    }

    private void FadeOut()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = FadeOutCoroutine();
        StartCoroutine(coroutine);
    }

    private IEnumerator FadeOutCoroutine()
    {
        var image = GetComponent<Image>();
        var color = image.color;

        while (color.a < 1)
        {
            color.a += Time.deltaTime / 1.5f;
            image.color = color;
            yield return null;
        }
    }
}
