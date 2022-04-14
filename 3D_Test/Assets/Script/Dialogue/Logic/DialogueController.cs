using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueController : MonoBehaviour
{
    public DialogueData_SO DialogueData;//当前的对话数据
    public bool canTalk = false;

    //void Awake()
    //{
    //    initDataDir();
    //}
    //void initDataDir()
    //{
    //    print("ww");
    //    DialogueData.initDir();
    //}
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && DialogueData != null)
            canTalk = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && DialogueData != null)
        { 
            canTalk = false;
            DialogueUI.Instance.dialoguePanel.SetActive(false);//脱离范围后关闭对话框
        }  
    }
    void Update()
    {
        if (canTalk && Input.GetMouseButtonDown(1))//右键开始对话
            StartDialogue();
    }
    void StartDialogue() 
    {
        DialogueUI.Instance.updateDialogue(DialogueData);
        DialogueUI.Instance.printMainPiece(DialogueData.dialoguePieceList[0]);//从该对话第0句开始
    }
}
