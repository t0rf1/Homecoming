using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Lock : MonoBehaviour
{
    UiManager uiManager;
    public GameObject player;
    public string code;
    public GameObject parrent;
    bool _unlocking = false;
    public bool isLocked;
    bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        isLocked = true;
        uiManager = GameObject.Find("UiCanvas").GetComponent<UiManager>();
        code = "392";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact") && isLocked)
            {
                EnableLock();
            }
        }
        
        if(_unlocking && Input.GetKeyDown(KeyCode.Escape))
        {
            CancelUnlocking();
        }

        if (_unlocking && Input.GetKeyDown(KeyCode.Return))
        {
            CheckCombination();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    public void CheckCombination()
    {
        
        int correctUnlocks = 0;
        int i = 0;
        foreach (Transform child in parrent.transform)
        {
            int codedSlot = (int)Char.GetNumericValue(code[i]);
            int slot = child.GetComponent<Slot>().number;
            if(slot == codedSlot)
            {
                correctUnlocks++;
            }
            i++;
        }

        if (correctUnlocks == code.Length)
        {
            isLocked = false;
            UnlockLock();
        }
        else
        {
            Debug.Log("Wrong combination");
        }
    }

    void EnableLock()
    {
        Debug.Log("Unlocking");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.SetActive(false);
        _unlocking = true;
        uiManager.lockPanel.SetActive(true);
    }

    void CancelUnlocking()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.SetActive(true);
        _unlocking = false;
        uiManager.lockPanel.SetActive(false);
    }
    void UnlockLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.SetActive(true);
        _unlocking = false;
        uiManager.lockPanel.SetActive(false);
    }
}
