using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectPanel : MonoBehaviour
{
    public static InspectPanel instance;

    public TMP_Text itemInspectName;
    public TMP_Text itemInspectDescription;
    public Image itemInspectImage;

    private void Awake()
    {
        instance = this;
    }
}
