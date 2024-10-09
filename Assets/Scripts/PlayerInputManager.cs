using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep it between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool isUIInputEnabled = false;

    public void EnableUIInput()
    {
        isUIInputEnabled = true;
    }

    public void DisableUIInput()
    {
        isUIInputEnabled = false;
    }

    private void Update()
    {
        if (!isUIInputEnabled)
        {
            // Handle player movement input
            HandlePlayerMovementInput();
        }
    }

    private void HandlePlayerMovementInput()
    {
        // Your existing player movement code here
    }
}
