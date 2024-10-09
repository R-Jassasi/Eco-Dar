using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class missionUpdate : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI missionText;

    // This method can be called through the Unity Event system to update the TextMeshPro text
    public void UpdateText(string newText)
    {
        missionText.text = newText;
    }
}
