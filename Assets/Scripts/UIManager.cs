using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIState { Win, Lose, Pause}

public delegate void UIHomeRequestHandler();
public delegate void UINextRequestHandler();
public delegate void UIRestartRequestHandler();
public class UIManager : MonoBehaviour
{
    public event UIHomeRequestHandler UIHomeRequest;
    public event UINextRequestHandler UINextRequest;
    public event UIRestartRequestHandler UIRestartRequest;

    private Dictionary<UIState, Canvas> m_UIStateToCanvasMap;

    public Canvas WinScreenCanvas;
    public Button WinBackButton;
    public Button WinNextButton;

    public Canvas LoseScreenCanvas;
    public Button LoseBackButton;
    public Button LoseRestartButton;

    public Canvas PauseCanvas;
    public Button PauseHomeButton;
    public Button PauseRestartButton;
    public Button PauseBackButton;

    private Canvas m_CurrentCanvas; 

    public void Init()
    {
        m_UIStateToCanvasMap = new Dictionary<UIState, Canvas>();
        m_UIStateToCanvasMap.Add(UIState.Lose, LoseScreenCanvas);
        m_UIStateToCanvasMap.Add(UIState.Win, WinScreenCanvas);
        m_UIStateToCanvasMap.Add(UIState.Pause, PauseCanvas);

        WinBackButton.onClick.AddListener(() => UIHomeRequest());
        WinNextButton.onClick.AddListener(() => UINextRequest());

        LoseBackButton.onClick.AddListener(() => UIHomeRequest());
        LoseRestartButton.onClick.AddListener(() => UIRestartRequest());
    }

    public void ShowUICanvas(UIState state)
    {
        if (m_CurrentCanvas != null)
        {
        m_CurrentCanvas.gameObject.SetActive(false);
        }
        m_CurrentCanvas = m_UIStateToCanvasMap[state];
        m_CurrentCanvas.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        m_CurrentCanvas.gameObject.SetActive(false);
    }

}
