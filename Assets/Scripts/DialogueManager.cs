using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI messageText;

    private Queue<string> messages;

    void Start()
    {
        dialogueBox.SetActive(false);
        messages = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);

        nameText.text = dialogue.name;

        messages.Clear();

        foreach (string message in dialogue.messages)
        {
            messages.Enqueue(message);
        }

        DisplayNextMessage();
    }

    public void DisplayNextMessage()
    {
        if (messages.Count == 0)
        {
            EndDialogue();
            return;
        }

        string message = messages.Dequeue();
        messageText.text = message;
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }


}