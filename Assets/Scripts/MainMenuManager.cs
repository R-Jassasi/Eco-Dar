using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject howToPlayPanel; // Reference to the How to Play panel

    // Function to load a scene by its index in the build settings
    public void PlayGame(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    // Function to show the How to Play panel
    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }

    // Function to hide the How to Play panel
    public void HideHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }

    // Function to quit the game
    public void QuitGame()
    {
        Debug.Log("Game is quitting"); // For testing in the Editor
        Application.Quit();
    }
}
