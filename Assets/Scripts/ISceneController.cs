using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public delegate void LevelWinHandler();
    public delegate void LevelFailHandler();
    public delegate void PauseHandler();

public interface ISceneController
{
    event LevelWinHandler LevelWon;
    event LevelFailHandler LevelFailed;
    event PauseHandler PauseRequest;

    void Init();
}
