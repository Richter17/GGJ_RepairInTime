using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.RepairTheme;

public class Level01 : MonoBehaviour, ISceneController
{
    public event LevelWinHandler LevelWon;
    public event LevelFailHandler LevelLost;
    public event PauseHandler PauseRequest;
    public event GoToGameplayHandler GoToGameplay;

    private RepairableObject m_repairableObject;

    public void Init()
    {
         m_repairableObject = FindObjectOfType<RepairableObject>();
        m_repairableObject.RepairCompleted += OnRepairComplete;
    }

    // Update is called once per frame
private void OnRepairComplete(RepairableObject objectRef)
    {
        LevelWon();
    }
}
