using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
namespace GGJ.Audio
{
    public class SoundController : MonoBehaviour, IMixerController
    {
        [SerializeField]
        private AudioMixer m_master;
        [SerializeField]
        private string[] m_mixerParamters;
        private float m_masterVolume;
        public AudioMixer Master
        {
            get { return m_master; }
        }

        public void ClearParamters(string paramName)
        {
            ClearParamters(new string[] { paramName });
        }

        public void ClearParamters(string[] paramsNames)
        {
            if (!m_master) return;
            foreach (var param in paramsNames)
            {
                if (!m_master.ClearFloat(param)) Debug.LogWarning("Couldn't find " + param + " paramater in the mixer");
            }
        }

        public void ControlMixer(MixerArgs arg)
        {
            ControlMixer(new MixerArgs[] { arg });
        }

        public void ControlMixer(MixerArgs[] args)
        {
            if (!m_master) return;
            foreach (var arg in args)
            {
                if(!m_master.SetFloat(arg.Name, arg.Value)) Debug.LogWarning("Couldn't find " + arg.Name + " paramater in the mixer");
            }
        }

        public void Mute(bool active)
        {
            if (!m_master) return;
            string masterVolume = "Volume_Master";
            if (active)
            {
                if (m_master.GetFloat(masterVolume, out m_masterVolume))
                {
                    m_master.SetFloat(masterVolume, -100);
                }
                else
                    Debug.LogWarning("Couldn't find " + masterVolume + " paramater in the mixer");

            }
            else
            {
                m_master.SetFloat(masterVolume, m_masterVolume);
            }
        }

        public void ResetMixer()
        {
            if (!m_master) return;
            ClearParamters(m_mixerParamters);
        }
    }
}