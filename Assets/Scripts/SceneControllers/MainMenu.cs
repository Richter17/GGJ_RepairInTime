using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, ISceneController
{
    public event LevelWinHandler LevelWon;
    public event LevelFailHandler LevelLost;
    public event PauseHandler PauseRequest;
    public event GoToGameplayHandler GoToGameplay;

    public Button PlayBtn;
    public void Init()
    {
        PlayBtn.onClick.AddListener(OnPlayBtnClick);
    }

    public void OnPlayBtnClick()
    {
        GoToGameplay();
    }

}
