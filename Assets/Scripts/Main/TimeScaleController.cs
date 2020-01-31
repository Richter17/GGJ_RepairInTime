using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace GGJ.TimeScale
{

    public class TimeScaleController : MonoBehaviour, ITimeController
    {
        private static TimeScaleController s_instance;
        public static ITimeController GetTimeController()
        {
            if(s_instance == null)
            {
                GameObject go = new GameObject("TimeScaleController");
                s_instance = go.AddComponent<TimeScaleController>();
            }
            return s_instance;
        }

        [SerializeField]
        [Range(0, 1)]
        private float m_minTimeScale = 0.05f, m_maxTimeScale = 0.85f;

        private float m_prevoiusTimeScale;

        public float TimeScale { get; set; }

        public float MinTimeScale
        {
            get
            {
                return m_minTimeScale;
            }
        }

        public float MaxTimeScale
        {
            get { return m_maxTimeScale; }
        }

        private bool m_isTimeControlled = false;
        public bool IsTimeControlled
        {
            get { return m_isTimeControlled; }
        }

        private bool m_timePaused;
        public bool TimePaused
        {
            get { return m_timePaused; }
        }

        public event ControlTimeStartedHandler TimeInputStarted;
        public event ControlTimeFinishedHandler TimeInputFinished;
        public event UpdateControlHandler ControlTimeUpdate;


        void Start()
        {
            SetTimeScale(0);
        }

        public void PauseTime(bool active)
        {
            m_timePaused = active;
            if (active)
            {
                m_prevoiusTimeScale = TimeScale;
                Time.timeScale = 0;
                Time.fixedDeltaTime = 0;
                TimeScale = 0;
            }
            else
            {
                StartTimeScaleControl(m_prevoiusTimeScale);
            }
        }

        public void StartTimeScaleControl(float normalizedValue)
        {
            SetTimeScale(normalizedValue);
            m_isTimeControlled = true;
            TimeInputStarted?.Invoke();
        }

        public void EndTimeScaleControl()
        {
            StartTimeScaleControl(0);
            m_isTimeControlled = false;
            TimeInputFinished?.Invoke();
        }

        private void SetTimeScale(float val)
        {
            Time.timeScale = Mathf.Lerp(m_minTimeScale, m_maxTimeScale, val);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            TimeScale = val;
        }
    }
}
