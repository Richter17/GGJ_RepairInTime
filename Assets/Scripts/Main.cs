using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


    enum GameState { MainMenu, Gameplay};
public class Main : MonoBehaviour
{

    private GameState m_gameState;
    private Dictionary<GameState, int> m_stateToSceneIndexMap;
    private bool m_IsInSceneTransition = false;

    private Canvas WinScreen;
    private int m_lastLevelIndex = 2;


    private ISceneController m_SceneController;

    private UIManager m_UIManager;
    
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        m_UIManager = FindObjectOfType<UIManager>();
        DontDestroyOnLoad(m_UIManager.gameObject);
        m_UIManager.Init();
        m_UIManager.UIHomeRequest += GoToMainMenu;
        m_UIManager.UINextRequest += GoToNextLevel;      
        m_UIManager.UIRestartRequest += RestartCurrentLevel;      
        GoToMainMenu();
    }

    private void RestartCurrentLevel()
    {
       StartCoroutine(LoadScene(m_lastLevelIndex));
    }

    private void GoToMainMenu()
    {
       StartCoroutine(LoadScene(1));
    }

    private void GoToNextLevel()
    {
        m_lastLevelIndex++;
       StartCoroutine(LoadScene(m_lastLevelIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        if (m_IsInSceneTransition)
        {
            yield return null;
        }

        m_IsInSceneTransition = true;

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

        while (!async.isDone)
        {
            yield return null;
        }
 
        m_SceneController = null;
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            if (m_SceneController == null)
            {
                m_SceneController = obj.GetComponentInChildren<ISceneController>();
            }          
        }
        Debug.Log("Scene loaded");
        m_IsInSceneTransition = false;
        InitScene();
    }

    private void InitScene()
    {
        m_SceneController.Init();
        m_SceneController.LevelWon += (() => m_UIManager.ShowUICanvas(UIState.Win));
        m_SceneController.LevelLost += (() => m_UIManager.ShowUICanvas(UIState.Lose));
        m_SceneController.GoToGameplay += RestartCurrentLevel;
    }
}
