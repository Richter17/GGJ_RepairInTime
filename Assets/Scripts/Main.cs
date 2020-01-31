using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


    enum GameState { Landing, Gameplay, WinScreen, LoseScreen, PauseScreen};
public class Main : MonoBehaviour
{
    private GameState m_gameState;
    private Dictionary<GameState, int> m_stateToSceneIndexMap;
    private bool m_IsInSceneTransition = false;

    private Canvas WinScreen;

    private ISceneController m_SceneController;

    
    private void Start()
    {
        m_stateToSceneIndexMap = new Dictionary<GameState, int>();
        m_stateToSceneIndexMap.Add(GameState.Landing, 0);

        StartCoroutine(SwitchState(GameState.Landing));

    }

    IEnumerator SwitchState(GameState state)
    {
        if (m_IsInSceneTransition)
        {
            yield return null;
        }
        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(m_stateToSceneIndexMap[state]);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            if (!m_IsInSceneTransition)
            {
                m_IsInSceneTransition = true;
            }
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneAt(m_stateToSceneIndexMap[state]));
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
        m_gameState = state;
        InitScene();
    }

    private void InitScene()
    {
        switch (m_gameState)
        {
            case GameState.Landing:
                break;
            case GameState.Gameplay:
                break;
            case GameState.WinScreen:
                break;
            case GameState.LoseScreen:
                break;
            case GameState.PauseScreen:
                break;
            default:
                break;
        }
    }

}
