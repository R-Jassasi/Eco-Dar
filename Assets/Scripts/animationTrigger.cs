using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationTrigger : MonoBehaviour
{
   public Animator animator; // Assign the Animator via the Inspector
 //    public string animationTriggerName = "Talk"; // The name of the trigger in the Animator

    // This function gets called when something enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider is tagged as the player or character
        if (other.CompareTag("Player"))
        {
            // Trigger the animation
            animator.SetTrigger("Talk");
        }
   }

     // Called when the player leaves the trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the collider is tagged as the player or character
        if (other.CompareTag("Player"))
        {
            // Trigger the "Leave" animation
            animator.SetTrigger("Leave");
        }
    }
}
