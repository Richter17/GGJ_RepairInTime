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

    private Canvas WinScreenCanvas;
    private Button WinBackButton;
    private Button WinNextButton;

    private Canvas LoseScreenCanvas;
    private Button LoseBackButton;
    private Button LoseRestartButton;

    private Canvas PauseCanvas;
    private Button PauseHomeButton;
    private Button PauseRestartButton;
    private Button PauseBackButton;

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
        m_CurrentCanvas.gameObject.SetActive(false);
        m_CurrentCanvas = m_UIStateToCanvasMap[state];
        m_CurrentCanvas.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        m_CurrentCanvas.gameObject.SetActive(false);
    }

}
