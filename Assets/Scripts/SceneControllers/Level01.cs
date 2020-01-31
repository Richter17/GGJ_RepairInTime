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


    public ObjectHealth[] ObjectsHealth;
    private RepairableObject m_repairableObject;

    public void Init()
    {
         m_repairableObject = FindObjectOfType<RepairableObject>();
        m_repairableObject.RepairCompleted += OnRepairComplete;
        foreach (ObjectHealth item in ObjectsHealth)
        {
            item.HealthDepleted += OnLevelLost;
        }
    }

    private void OnLevelLost()
    {
        LevelLost();
    }
    // Update is called once per frame
private void OnRepairComplete(RepairableObject objectRef)
    {
        LevelWon();
    }
}
