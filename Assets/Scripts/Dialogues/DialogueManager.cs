using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text messageText;

    private Queue<string> messages;

    [System.NonSerialized] public bool inDialogue;

    void Start()
    {
        dialogueBox.SetActive(false);
        messages = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        inDialogue = true;
        Time.timeScale = 0f;

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
        inDialogue = false;
        Time.timeScale = 1.0f;
        dialogueBox.SetActive(false);
    }


}