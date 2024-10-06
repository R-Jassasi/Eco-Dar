using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitGame : MonoBehaviour
{
        // Use this for initialization
        void Start()
        {
            //Set Cursor to not be visible
            Cursor.visible = true;
        }
   


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
