using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
namespace GGJ.Audio
{
    public class SoundController : MonoBehaviour, IMixerController
    {
        private static SoundController s_instace;
        public static IMixerController GetMixer()
        {
            if(s_instace == null)
            {
                GameObject g = new GameObject("SoundController");
                s_instace = g.AddComponent<SoundController>();
            }
            return s_instace;
        }


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

        public void ControlMixer(MixerArgs arg, bool animate)
        {
            ControlMixer(new MixerArgs[] { arg }, animate);
        }

        public void ControlMixer(MixerArgs[] args, bool animate)
        {
            if (!m_master) return;
            foreach (var arg in args)
            {
                if (!animate)
                    if (!m_master.SetFloat(arg.Name, arg.Value)) Debug.LogWarning("Couldn't find " + arg.Name + " paramater in the mixer");
                    else { }
                else
                        StartCoroutine(Animate(arg,0.5f));
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

        private void Awake()
        {
            if (s_instace == null) s_instace = this;
            else if(s_instace != this) Destroy(gameObject);
        }

        private IEnumerator Animate(MixerArgs arg, float dur)
        {
            if (!m_master) yield break;
            if (!m_master.GetFloat(arg.Name, out float startVal)) yield break;
            float inc = startVal - arg.Value < 0 ? 1 : -1;
            float t = 0;
            float cur_val = startVal;
            while (t < dur)
            {
                cur_val += 0.015f / dur * inc;
                m_master.SetFloat(arg.Name, cur_val);
                yield return null;
            }

            m_master.SetFloat(arg.Name, arg.Value);

        }
    }
}