using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(fileName = "New TimeControllerSettings", menuName = "TimeControllerSettings", order = 0)]
    public class TimeScaleSettings : ScriptableObject
    {
        [Range(0, 1)]
        public float MinTimeScale = 0.05f;
        [Range(0, 1)]
        public float MaxTimeScale = 0.85f;
    }
}