using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startBtn;
    public Button continueBtn;
    public Button quitBtn;

    private void Awake()
    {
        startBtn = transform.GetChild(0).GetComponent<Button>();
        continueBtn = transform.GetChild(1).GetComponent<Button>();
        quitBtn = transform.GetChild(2).GetComponent<Button>();

        startBtn.onClick.AddListener(StartGame);
        continueBtn.onClick.AddListener(ContinueGame);
        quitBtn.onClick.AddListener(QuitGame);
    }

    void StartGame()//��ʼ��Ϸ
    {
        PlayerPrefs.DeleteAll();
        SceneController.Instance.TransToFirstLevel();
    }

    void ContinueGame()//������Ϸ
    {

    }

    void QuitGame()//�˳���Ϸ
    {
        Application.Quit();
    }
}
