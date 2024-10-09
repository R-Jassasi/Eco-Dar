using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class goBackToScene0 : MonoBehaviour
{
     // This method can be called from the button click event
    public void GoBackToScene1()
    {
        SceneManager.LoadScene(2); // Loads the scene with build index 1
    }
}
