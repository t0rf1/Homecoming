using I2.Loc;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text messageText;

    private Queue<string> messages;

    [System.NonSerialized] public bool inDialogue;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        dialogueBox.SetActive(false);
        messages = new Queue<string>();
    }

    public void StartDialogue(LocalizedString nameLocalized, List<string> messagesString)
    {
        inDialogue = true;
        Time.timeScale = 0f;

        dialogueBox.SetActive(true);

        nameText.text = nameLocalized;

        messages.Clear();

        foreach (string message in messagesString)
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