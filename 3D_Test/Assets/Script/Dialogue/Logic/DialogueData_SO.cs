using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData_SO : ScriptableObject
{
    public List<DialoguePiece> dialoguePieceList = new List<DialoguePiece>();
    public Dictionary<string, DialoguePiece> dialoguePieceDir = new Dictionary<string, DialoguePiece>();

#if UNITY_EDITOR
    void OnValidate()//���ڱ༭����ִ�е��´����Ϸ���ֵ����
    {
        dialoguePieceDir.Clear();
        foreach (var piece in dialoguePieceList)
        {
            if (!dialoguePieceDir.ContainsKey(piece.ID))
                dialoguePieceDir.Add(piece.ID, piece);
        }
    }
#else
    void Awake()//��֤�ڴ��ִ�е���Ϸ���һʱ���öԻ��������ֵ�ƥ�� 
    {
        dialogueIndex.Clear();
        foreach (var piece in dialoguePieces)
        {
            if (!dialogueIndex.ContainsKey(piece.ID))
                dialogueIndex.Add(piece.ID, piece);
        }
    }
#endif

    public QuestData_SO getQuest()
    {
        QuestData_SO tempQuest = null;
        foreach (var p in dialoguePieceList)
        {
            if (p.quest != null)
                tempQuest = p.quest;
        }
        return tempQuest;
    }
}
