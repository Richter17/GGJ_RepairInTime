using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIState { Win, Lose, Pause}
public class UIManager : MonoBehaviour
{
    private Dictionary<UIState, Canvas> m_UIStateToCanvasMap;

    private Canvas WinScreenCanvas;
    private Button WinBackButton;
    private Button WinNextButton;

    private Canvas LoseScreenCanvas;
    private Button LoseBackButton;
    private Button LoseNextButton;

    private Canvas PauseCanvas;

    private Canvas m_CurrentCanvas; 

    public void Init()
    {
        m_UIStateToCanvasMap = new Dictionary<UIState, Canvas>();
        m_UIStateToCanvasMap.Add(UIState.Lose, LoseScreenCanvas);
        m_UIStateToCanvasMap.Add(UIState.Win, WinScreenCanvas);
    }

    public void ShowWinMenu()
    {
        m_CurrentCanvas.gameObject.SetActive(false);
        m_CurrentCanvas = WinScreenCanvas;
        m_CurrentCanvas.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        m_CurrentCanvas.gameObject.SetActive(false);
    }
    public void ShowLoseMenu()
    {
        m_CurrentCanvas.gameObject.SetActive(false);
        m_CurrentCanvas = LoseScreenCanvas;
        m_CurrentCanvas.gameObject.SetActive(true);
    }

}
