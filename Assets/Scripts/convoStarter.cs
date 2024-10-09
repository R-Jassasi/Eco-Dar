using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class convoStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the conversation
            ConversationManager.Instance.StartConversation(myConversation);

            // Disable player movement input
            PlayerInputManager.Instance.EnableUIInput();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // End the conversation if necessary
            ConversationManager.Instance.EndConversation();

            // Enable player movement input again
            PlayerInputManager.Instance.DisableUIInput();
        }
    }
}
