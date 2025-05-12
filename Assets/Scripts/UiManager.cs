using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public Stats playerStats;

    public GameObject lockPanel;
    // Start is called before the first frame update
    void Start()
    {
        //playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = playerStats.hp.ToString();
    }
}
