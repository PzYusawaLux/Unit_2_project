using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    // Coroutine for fading to black
    public IEnumerator FadeToBlack()
    {
        fadeImage.CrossFadeAlpha(1.0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);
    }

    // Coroutine for fading back
    public IEnumerator FadeBack()
    {
        fadeImage.CrossFadeAlpha(0.0f, fadeDuration, false);
        yield return new WaitForSeconds(fadeDuration);
        fadeImage.gameObject.SetActive(false); // Disable the fade image after fading back
    }
}
