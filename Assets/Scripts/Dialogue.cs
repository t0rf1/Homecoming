using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using I2.Loc;

[System.Serializable]
public class Dialogue
{
    public LocalizedString name;

    //[TextArea(1, 5)]
    public List<LocalizedString> messages;
}
