using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commands : MonoBehaviour
{
    public static Commands instance;

    [SerializeField] public Button inspectCommand;
    [SerializeField] public Button useCommand;
    [SerializeField] public Button equipCommand;

    private void Awake()
    {
        instance = this;
    }
}
