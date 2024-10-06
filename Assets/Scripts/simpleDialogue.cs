using System.Collections;
using UnityEngine;
using TMPro;

public class simpleDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText; // Use TMP_Text for TextMeshPro
    public string[] dialogue;
    private int index;

    public GameObject contBttn;
    public float wordSpeed;
    public bool playerIsClose;

    private bool isDialogueActive = false;  // Track if the dialogue is active
    private bool isTyping = false;           // Track if typing is in progress
    private Coroutine typingCoroutine;        // Reference to the typing coroutine

    void Update()
    {
        if (playerIsClose && !isDialogueActive)
        {
            StartDialogue();
        }

        if (dialogueText.text == dialogue[index])
        {
            contBttn.SetActive(true);
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true); 
        typingCoroutine = StartCoroutine(Typing());
        isDialogueActive = true;  // Dialogue is now active
    }

    // Reset text and hide dialogue panel
    public void zeroText()
    {
        dialogueText.text = ""; // Clear text
        index = 0;              // Reset index to the start
        dialoguePanel.SetActive(false); // Hide dialogue panel
        isDialogueActive = false;  // Dialogue is no longer active
        contBttn.SetActive(false); // Hide continue button
        isTyping = false;         // Ensure typing is not active
    }

    IEnumerator Typing()
    {
        isTyping = true; // Set typing to true
        dialogueText.text = ""; // Ensure text is cleared before typing
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed); // Control typing speed
        }
        isTyping = false; // Typing is finished
    }

    public void NextLine()
    {
        contBttn.SetActive(false); // Hide continue button
        if (index < dialogue.Length - 1)
        {
            index++;
            if (typingCoroutine != null) StopCoroutine(typingCoroutine); // Stop typing if active
            dialogueText.text = ""; // Clear text for new line
            typingCoroutine = StartCoroutine(Typing()); // Start typing new dialogue
        }
        else
        {
            zeroText(); // End dialogue when finished
        }
    }

    // Check if player triggers colliders
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;

            // Stop typing if active and reset text
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // Stop the typing coroutine
            }

            zeroText(); // Hide the dialogue panel and reset text
        }
    }
}
