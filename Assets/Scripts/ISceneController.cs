using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public delegate void LevelWinHandler();
    public delegate void LevelFailHandler();
    public delegate void PauseHandler();
    public delegate void GoToGameplayHandler();

public interface ISceneController
{
    event LevelWinHandler LevelWon;
    event LevelFailHandler LevelLost;
    event PauseHandler PauseRequest;
    event GoToGameplayHandler GoToGameplay;

    void Init();
}
