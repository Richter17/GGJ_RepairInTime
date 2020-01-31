using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
namespace GGJ.Audio
{
    public interface IMixerController
    {
        AudioMixer Master { get; }

        void ResetMixer();
        void ControlMixer(MixerArgs arg);
        void ControlMixer(MixerArgs[] args);
        void ClearParamters(string paramName);
        void ClearParamters(string[] paramsNames);
        void Mute(bool active);

    }

    public class MixerArgs
    {
        public string Name;
        public float Value;
        public MixerArgs(string name, float val)
        {
            Name = name;
            Value = val;
        }
    }
}