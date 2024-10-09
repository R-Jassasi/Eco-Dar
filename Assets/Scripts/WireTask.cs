using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Required for UI elements
using TMPro;  // Required for TextMeshPro
using UnityEngine.SceneManagement;  // Required for scene transitions

public class WireTask : MonoBehaviour
{
    public List<Color> _wireColors = new List<Color>();  // List of wire colors
    public List<Wire> _leftWires = new List<Wire>(); 
    public List<Wire> _rightWires = new List<Wire>();

    public Wire CurrentDraggedWire;
    public Wire CurrentHoveredWire;

    public bool IsTaskCompleted = false;

    public GameObject taskCompleteMessage;  // The message to display after task completion
    public Button continueButton;  // The "Continue" button
    public TextMeshProUGUI messageText;  // TextMeshPro for the message

    private void Start()
    {
        taskCompleteMessage.SetActive(false);  // Hide the task completion message at the start
        continueButton.onClick.AddListener(OnContinueButtonClicked);  // Add listener for the button

        AssignWireColors();  // Assign colors to wires at the start

        StartCoroutine(CheckTaskCompletion());
    }

    // Function to assign colors to the left and right wires randomly
    private void AssignWireColors()
    {
        List<Color> availableColors = new List<Color>(_wireColors);  // Copy of the color list
        List<int> leftWireIndices = new List<int>();
        List<int> rightWireIndices = new List<int>();

        // Fill index lists with the indices of left and right wires
        for (int i = 0; i < _leftWires.Count; i++) leftWireIndices.Add(i);
        for (int i = 0; i < _rightWires.Count; i++) rightWireIndices.Add(i);

        while (availableColors.Count > 0 && leftWireIndices.Count > 0 && rightWireIndices.Count > 0)
        {
            // Pick a random color, a left wire, and a right wire
            Color pickedColor = availableColors[Random.Range(0, availableColors.Count)];
            int leftIndex = leftWireIndices[Random.Range(0, leftWireIndices.Count)];
            int rightIndex = rightWireIndices[Random.Range(0, rightWireIndices.Count)];

            // Assign the color to the left and right wires
            _leftWires[leftIndex].SetColor(pickedColor);
            _rightWires[rightIndex].SetColor(pickedColor);

            // Remove used color and wire indices from the lists
            availableColors.Remove(pickedColor);
            leftWireIndices.Remove(leftIndex);
            rightWireIndices.Remove(rightIndex);
        }
    }

    private IEnumerator CheckTaskCompletion()
    {
        while (!IsTaskCompleted)
        {
            int successfulWires = 0;

            for (int i = 0; i < _rightWires.Count; i++)
            {
                if (_rightWires[i].IsSuccess)
                {
                    successfulWires++;
                }
            }

            if (successfulWires >= _rightWires.Count)
            {
                Debug.Log("TASK COMPLETED");
                IsTaskCompleted = true;

                // Display the task complete message and button
                ShowCompletionMessage();
            }
            else
            {
                Debug.Log("TASK INCOMPLETED");
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    // Function to show the completion message and continue button
    private void ShowCompletionMessage()
    {
        taskCompleteMessage.SetActive(true);  // Show the message UI
        messageText.text = "Task Completed!";  // Set the message text
    }

    // This method is triggered when the continue button is clicked
    private void OnContinueButtonClicked()
    {
        SceneManager.LoadScene(5);  // Load scene 3 (adjust the index based on your setup)
    }
}
