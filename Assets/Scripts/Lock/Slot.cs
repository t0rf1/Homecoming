using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot : MonoBehaviour
{
    public int number;
    public TextMeshProUGUI textNumber;
   

    private void Start()
    {
        textNumber = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        textNumber.text = number.ToString();
    }
}
