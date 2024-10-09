using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.5f;  // Speed of the fade effect

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        fadeImage.canvasRenderer.SetAlpha(1.0f);  // Start fully opaque

        // Fade out
        fadeImage.CrossFadeAlpha(0.0f, fadeSpeed, false);

        // Wait for the fade out to complete
        yield return new WaitForSeconds(fadeSpeed);

        // Optionally, load the next scene here
    }

    public void FadeToScene(int sceneIndex)
    {
        StartCoroutine(FadeOut(sceneIndex));
    }

    IEnumerator FadeOut(int sceneIndex)
    {
        fadeImage.canvasRenderer.SetAlpha(0.0f);  // Start fully transparent

        // Fade in
        fadeImage.CrossFadeAlpha(1.0f, fadeSpeed, false);

        // Wait for the fade in to complete
        yield return new WaitForSeconds(fadeSpeed);

        // Load the next scene
        SceneManager.LoadScene(sceneIndex);
    }
}
