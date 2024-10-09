using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;  

public class GoWireTask : MonoBehaviour
{
    public SceneFader sceneFader;  // Reference to the SceneFader script
    public TextMeshProUGUI promptText;        // UI Text element for the "Press E" prompt

    private bool isPlayerInRange = false;  // Check if the player is in the trigger zone

    void Start()
    {
        promptText.gameObject.SetActive(false);  // Hide the prompt at the start
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;  // Player is in range
            promptText.gameObject.SetActive(true);  // Show the "Press E" prompt
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;  // Player left the range
            promptText.gameObject.SetActive(false);  // Hide the "Press E" prompt
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Alpha1))  // Check if player presses 1
        {
            sceneFader.FadeToScene(3);  // Replace 1 with your scene index
        }

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Alpha2))  // Check if player presses 2
        {
            sceneFader.FadeToScene(4); 
        }
    }
}
