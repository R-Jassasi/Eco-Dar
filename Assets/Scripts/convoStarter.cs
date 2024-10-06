using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class convoStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
}
