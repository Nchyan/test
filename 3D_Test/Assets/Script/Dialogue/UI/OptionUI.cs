using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Text optionTxt;
    public Button button;
    public DialoguePiece piece;
    public string nextID;//该选项导向的下一条语句

    private bool takeQuest;//该option是否能领取任务

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
            if (takeQuest)//接受任务
            {
                var thisTask = new QuestManager.QuestTask
                {
                    questData = Instantiate(piece.quest)
                };
                if (QuestManager.Instance.HaveQuest(thisTask.questData))//存在重复取消接受
                {
                    if (QuestManager.Instance.GetTask(thisTask.questData).IsComplete)//判断任务是否完成
                    { 
                        thisTask.questData.GiveReward();
                        QuestManager.Instance.GetTask(thisTask.questData).IsEnd = true;
                    }           
                }
                else //接受任务
                {
                    QuestManager.Instance.taskList.Add(thisTask);
                    QuestManager.Instance.GetTask(thisTask.questData).IsStart = true ;//更改任务状态

                    foreach (var requireItem in thisTask.questData.getRequireNameList())//更新任务数据（领取任务前存在目标物品的情况）
                    {
                        InventoryManager.Instance.checkQuestItemInBag(requireItem);
                    }
                }
            }
                
        }

        if (nextID == "")//无跳转结束对话
            DialogueUI.Instance.dialoguePanel.SetActive(false);
        else//跳转至对应ID的对话中
            DialogueUI.Instance.printMainPiece(DialogueUI.Instance.curDialogueData.dialoguePieceDir[nextID]);
    }
}
