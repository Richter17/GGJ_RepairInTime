using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.RepairTheme;
using System.Linq;

public class Level05 : MonoBehaviour, ISceneController
{
    public event LevelWinHandler LevelWon;
    public event LevelFailHandler LevelLost;
    public event PauseHandler PauseRequest;
    public event GoToGameplayHandler GoToGameplay;

    //public ObjectHealth[] ObjectsHealth;
    private IRepairs[] RepairObjectsArray;
    private RepairableObject m_repairableObject;

    public void Init()
    {
        m_repairableObject = FindObjectOfType<RepairableObject>();
        m_repairableObject.RepairCompleted += OnRepairComplete;

        RepairObjectsArray = FindObjectsOfType<MonoBehaviour>().OfType<IRepairs>().ToArray();
        foreach (var reapirPiece in RepairObjectsArray)
        {
            reapirPiece.RepairDestroyed += OnLevelLost;
        }
    }

    private void OnLevelLost()
    {
        LevelLost();
    }

    private void StartGame()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    private void OnRepairComplete(RepairableObject objectRef)
    {
        LevelWon();
    }
}
