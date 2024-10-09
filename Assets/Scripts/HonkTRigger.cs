using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonkTRigger : MonoBehaviour
{
  // AudioSource to play the sound when the car passes by
    public AudioSource Sound;

    // Names of the cars that should trigger the sound
    public string car1Name = "Taxi";
    public string car2Name = "SUV";

    // This function is called when a collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered has the name of either car
        if (other.gameObject.name == car1Name || other.gameObject.name == car2Name)
        {
            // Play the sound if it is not already playing
            if (!Sound.isPlaying)
            {
                Sound.Play();
            }
        }
    }
}
