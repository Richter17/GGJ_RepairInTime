using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public delegate void ControlTimeStartedHandler();
    public delegate void ControlTimeFinishedHandler();
    public delegate void UpdateControlHandler(float timeScale);
    public interface ITimeController
    {
        bool TimePaused { get; }

        float TimeScale { get; set; }
        float MinTimeScale { get; }
        float MaxTimeScale { get; }

        bool IsTimeControlled { get; }

        void PauseTime(bool active);
        void StartTimeScaleControl(float normalizedValue);
        void EndTimeScaleControl();
        event ControlTimeStartedHandler TimeInputStarted;
        event ControlTimeFinishedHandler TimeInputFinished;
        event UpdateControlHandler ControlTimeUpdate;


    }
}