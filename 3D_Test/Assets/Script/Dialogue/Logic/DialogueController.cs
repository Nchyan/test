using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueController : MonoBehaviour
{
    public DialogueData_SO DialogueData;//��ǰ�ĶԻ�����
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
            DialogueUI.Instance.dialoguePanel.SetActive(false);//���뷶Χ��رնԻ���
        }  
    }
    void Update()
    {
        if (canTalk && Input.GetMouseButtonDown(1))//�Ҽ���ʼ�Ի�
            StartDialogue();
    }
    void StartDialogue() 
    {
        DialogueUI.Instance.updateDialogue(DialogueData);
        DialogueUI.Instance.printMainPiece(DialogueData.dialoguePieceList[0]);//�ӸöԻ���0�俪ʼ
    }
}
