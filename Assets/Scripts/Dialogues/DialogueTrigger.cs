using I2.Loc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager dialogueManager;
    [SerializeField] public Dialogue dialogue;

    public List<string> messagesString;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Interact()
    {
        CopyLists();

        if (dialogueManager.inDialogue)
        {
            dialogueManager.DisplayNextMessage();
        }
        else
        {
            dialogueManager.StartDialogue(dialogue.nameLocalized, messagesString);
        }
    }

    //Interact but with message string list as parameter from outside
    public void Interact(List<string> newMessages)
    {
        if (dialogueManager.inDialogue)
        {
            dialogueManager.DisplayNextMessage();
        }
        else
        {
            dialogueManager.StartDialogue(dialogue.nameLocalized, newMessages);
        }
    }

    //Converts localized string list to string list
    public void CopyLists()
    {
        messagesString.Clear();
        foreach (LocalizedString message in dialogue.messagesLocalized)
        {
            messagesString.Add(message);
        }
    }
}
