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
    private int m_lastLevelIndex = 1;


    private ISceneController m_SceneController;

    private UIManager m_UIManager;
    
    private void Start()
    {
        m_UIManager = FindObjectOfType<UIManager>();
        m_UIManager.Init();
        m_UIManager.UIHomeRequest += GoToMainMenu;
        m_UIManager.UINextRequest += GoToNextLevel;      

    }

    private void GoToGameplayLevel(int index)
    {
        
    }

    private void GoToMainMenu()
    {
        LoadScene(0);
    }

    private void GoToNextLevel()
    {
        m_lastLevelIndex++;
        LoadScene(m_lastLevelIndex);
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        if (m_IsInSceneTransition)
        {
            yield return null;
        }

        m_IsInSceneTransition = true;

        yield return new WaitForSeconds(3);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

        while (!async.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneAt(sceneIndex));
 
        m_SceneController = null;
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            if (m_SceneController == null)
            {
                m_SceneController = obj.GetComponentInChildren<ISceneController>();
            }          
        }

        m_IsInSceneTransition = false;
        InitScene();
    }

    private void InitScene()
    {
        m_SceneController.LevelWon += (() => m_UIManager.ShowUICanvas(UIState.Win));
        m_SceneController.LevelLost += (() => m_UIManager.ShowUICanvas(UIState.Lose));
    }

    private void OnHomeScreen()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(1);
    }

}
