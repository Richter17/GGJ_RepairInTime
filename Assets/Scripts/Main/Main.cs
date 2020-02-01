using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GGJ.Audio;

    enum GameState { MainMenu, Gameplay};
public class Main : MonoBehaviour
{
    private const int FIRST_LEVEL = 2;
    private GameState m_gameState;
    private Dictionary<GameState, int> m_stateToSceneIndexMap;
    private bool m_IsInSceneTransition = false;

    private Canvas WinScreen;
    private int m_lastLevelIndex = FIRST_LEVEL;


    private ISceneController m_SceneController;
    private IMixerController m_mixer;

    private UIManager m_UIManager;
    
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        m_mixer = SoundController.GetMixer();
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
       m_UIManager.ToggleBG(true);
       StartCoroutine(LoadScene(1));
    }

    private void GoToNextLevel()
    {
        m_lastLevelIndex++;
        if (m_lastLevelIndex >= SceneManager.sceneCountInBuildSettings)
        {
            m_lastLevelIndex = FIRST_LEVEL;
            GoToMainMenu();
            return;
        }
        StartCoroutine(LoadScene(m_lastLevelIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        m_UIManager.HideUI();
        if (m_IsInSceneTransition)
        {
            yield return null;
        }
        
        m_UIManager.ToggleBG(sceneIndex == 1);

        m_IsInSceneTransition = true;
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
        async.allowSceneActivation = false;
        m_UIManager.ShowLoading();
        m_mixer.ControlMixer(new MixerArgs("BG_Volume", -100), false);
        float delayCount = 0;
        while (async.progress < 0.89f || delayCount < 0.5f)
        {
            delayCount += 0.01f;
            m_UIManager.LoadingBar.fillAmount = Mathf.Min(delayCount / 0.5f ,async.progress);
            yield return null;
        }
        
        m_UIManager.LoadingBar.fillAmount = 1;
        async.allowSceneActivation = true;
        yield return async.isDone;
        
        m_mixer.ControlMixer(new MixerArgs("BG_Volume", -10), false);
        m_UIManager.HideLoading();
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
        m_SceneController.LevelWon += (() => m_UIManager.ShowUICanvas(UIState.Win, (m_lastLevelIndex - FIRST_LEVEL + 1)));
        m_SceneController.LevelLost += (() => m_UIManager.ShowUICanvas(UIState.Lose, (m_lastLevelIndex - FIRST_LEVEL + 1)));
        m_SceneController.GoToGameplay += RestartCurrentLevel;
    }
}
