using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.Audio;
using GGJ;
public class BGSoundBehavouir : MonoBehaviour
{
    private IMixerController m_mixer;
    private ITimeController m_TimeController;
    private AudioSource m_as;
    private float m_pitch;
    // Start is called before the first frame update
    void Start()
    {
        m_as = GetComponent<AudioSource>();
        m_mixer = SoundController.GetMixer();
        m_TimeController = TimeScaleController.GetTimeController();
        m_TimeController.TimeInputFinished += ClearTempo;
        m_pitch = 1;
    }

    private void Update()
    {
        if (m_mixer == null || m_TimeController == null) return;
        if (m_TimeController.IsTimeControlled)
        {
            ChangeTempo(m_TimeController.TimeScale);
        }
            
    }

    private void ClearTempo()
    {
        m_as.pitch = 1;
        m_mixer.ClearParamters("BG_Tempo");
    }

    private void ChangeTempo(float val)
    {
        int precentage = (int)(val * 100);

        m_as.pitch = Mathf.Lerp(1f, 2, (precentage % 10) *0.1f);
        Debug.Log(m_as.pitch);
        m_mixer.ControlMixer(new MixerArgs("BG_Tempo", 1f / m_as.pitch));
    }
}
