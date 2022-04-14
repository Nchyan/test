using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialoguePiece
{
    public string ID;
    public Sprite img;
    public string note;
    [TextArea]
    public string text;

    public QuestData_SO quest;

    public List<DialogueOption> optionList = new List<DialogueOption>();
}
