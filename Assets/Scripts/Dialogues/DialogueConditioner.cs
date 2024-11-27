using System.Collections.Generic;
using UnityEngine;

public class DialogueConditioner : MonoBehaviour
{
    [SerializeField] private List<GameObject> dialogueOptions = new List<GameObject>();

    public void ChooseDialogue(int dialogueIndex)
    {
        foreach (GameObject dialogue in dialogueOptions)
        {
            dialogue.SetActive(dialogue == dialogueOptions[dialogueIndex]);
        }
    }
}
