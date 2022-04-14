using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Text optionTxt;
    public Button button;
    public DialoguePiece piece;
    public string nextID;//��ѡ������һ�����

    private bool takeQuest;//��option�Ƿ�����ȡ����

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSelectOption);
    }
    public void setOption(DialoguePiece piece, DialogueOption option)
    {
        this.piece = piece;
        optionTxt.text = option.text;
        nextID = option.index;
        takeQuest = option.takeQuest;
    }
    void OnSelectOption() 
    {
        if (piece.quest != null)
        {
            if (takeQuest)//��������
            {
                var thisTask = new QuestManager.QuestTask
                {
                    questData = Instantiate(piece.quest)
                };
                if (QuestManager.Instance.HaveQuest(thisTask.questData))//�����ظ�ȡ������
                {
                    if (QuestManager.Instance.GetTask(thisTask.questData).IsComplete)//�ж������Ƿ����
                    { 
                        thisTask.questData.GiveReward();
                        QuestManager.Instance.GetTask(thisTask.questData).IsEnd = true;
                    }           
                }
                else //��������
                {
                    QuestManager.Instance.taskList.Add(thisTask);
                    QuestManager.Instance.GetTask(thisTask.questData).IsStart = true ;//��������״̬

                    foreach (var requireItem in thisTask.questData.getRequireNameList())//�����������ݣ���ȡ����ǰ����Ŀ����Ʒ�������
                    {
                        InventoryManager.Instance.checkQuestItemInBag(requireItem);
                    }
                }
            }
                
        }

        if (nextID == "")//����ת�����Ի�
            DialogueUI.Instance.dialoguePanel.SetActive(false);
        else//��ת����ӦID�ĶԻ���
            DialogueUI.Instance.printMainPiece(DialogueUI.Instance.curDialogueData.dialoguePieceDir[nextID]);
    }
}
