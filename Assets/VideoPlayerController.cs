using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished; // Subscribe to the video end event
        videoPlayer.Play(); // Start the video
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Load the next scene by index
        SceneManager.LoadScene(2); // Change 1 to the index of your second scene
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}
