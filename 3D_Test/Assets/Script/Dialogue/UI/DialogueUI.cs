using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueUI : Singleton<DialogueUI>
{
    [Header("Base Element")]
    public Image charImg;
    public Text mainText;
    public Button nextBtn;
    public GameObject dialoguePanel;

    [Header("options")]
    public RectTransform optionTrans;
    public OptionUI optionTemp;

    [Header("Data")]
    public DialogueData_SO curDialogueData;
    int curIndex = 0;//��ǰ�����еĶԻ���ַ

    public void Awake()
    {
        nextBtn.onClick.AddListener(nextPiece);
    }

    void nextPiece() 
    {
        if (curIndex < curDialogueData.dialoguePieceList.Count)
            printMainPiece(curDialogueData.dialoguePieceList[curIndex]);
        else
            dialoguePanel.SetActive(false);
    }

    public void updateDialogue(DialogueData_SO data)
    {
        curDialogueData = data;
        curIndex = 0;
    }
    public void printMainPiece(DialoguePiece piece)
    {
        dialoguePanel.SetActive(true);

        if (piece.img != null)
        {
            charImg.enabled = true;
            charImg.sprite = piece.img;
        }
        else
            charImg.enabled = false;

        mainText.text = " ";//��նԻ����ڵ�����
        mainText.DOText(piece.text, 0.25f);


        if (piece.optionList.Count == 0 && curDialogueData.dialoguePieceList.Count > 0 )
            nextBtn.enabled = true;
        else
            nextBtn.enabled = false;

        ++curIndex;
        createOptions(piece);
    }
    public void createOptions(DialoguePiece piece)
    {
        if (optionTrans.childCount > 0)//���ԭ��option
        {
            for (int i = 0; i < optionTrans.childCount; ++i)//���ԭ��ѡ��
            {
               Destroy(optionTrans.GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < piece.optionList.Count; ++i) //���ɶ�Ӧoption
        {
            var option = Instantiate(optionTemp, optionTrans);
            option.setOption(piece, piece.optionList[i]);
        }
    }
}
