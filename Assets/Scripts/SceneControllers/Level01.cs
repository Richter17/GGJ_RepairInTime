using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.RepairTheme;
using System.Linq;

public class Level01 : MonoBehaviour, ISceneController
{
    public event LevelWinHandler LevelWon;
    public event LevelFailHandler LevelLost;
    public event PauseHandler PauseRequest;
    public event GoToGameplayHandler GoToGameplay;
    public DraggableObject[] draggables;
    //public ObjectHealth[] ObjectsHealth;
    private IRepairs[] RepairObjectsArray;
    private RepairableObject m_repairableObject;
    private bool tutorialDone = false;
    private int m_tutorialStep = 0;
    [SerializeField]
    private GameObject[] tutorialOverlays;

    public void Init()
    {
        Time.timeScale = 0;
        foreach (DraggableObject draggable in draggables)
        {
            draggable.CanBeDragged = false;
        }
        m_repairableObject = FindObjectOfType<RepairableObject>();
        m_repairableObject.RepairCompleted += OnRepairComplete;

        RepairObjectsArray = FindObjectsOfType<MonoBehaviour>().OfType<IRepairs>().ToArray();
        foreach (var reapirPiece in RepairObjectsArray)
        {
            reapirPiece.RepairDestroyed += OnLevelLost;
        }
        NextTutorialStep();
    }

    private void OnLevelLost()
    {
        LevelLost();
    }

    private void StartGame()
    {
        foreach (DraggableObject draggable in draggables)
        {
            draggable.CanBeDragged = true;
        }
        Time.timeScale = 0;
    }

    private void NextTutorialStep()
    {
        if (tutorialOverlays.Length > m_tutorialStep)
        {
            foreach (GameObject overlay in tutorialOverlays)
            {
                overlay.SetActive(false);
            }
            tutorialOverlays[m_tutorialStep].SetActive(true);
            m_tutorialStep++;
        }
        else
        {
            foreach (GameObject overlay in tutorialOverlays)
            {
                overlay.SetActive(false);
            }
            tutorialDone = true;
            StartGame();
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0 && !tutorialDone)
        {
            NextTutorialStep();
        }

        if (Input.GetMouseButtonUp(0) && !tutorialDone)
        {
            NextTutorialStep();
        }
    }
    // Update is called once per frame
    private void OnRepairComplete(RepairableObject objectRef)
    {
        LevelWon();
    }
}
