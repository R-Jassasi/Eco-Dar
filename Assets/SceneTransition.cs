using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void GoToNextScene()
    {
        // Get the current scene index and increment it to go to the next one
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if the next scene index is within the bounds of the scene count
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next scene available. Resetting to the first scene.");
            SceneManager.LoadScene(6); // Optionally, go back to the first scene if there are no more scenes
        }
    }
}
