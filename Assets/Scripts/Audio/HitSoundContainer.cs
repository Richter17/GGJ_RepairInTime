using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HitSoundContainer : MonoBehaviour
{
    [SerializeField]
    private bool randomPitch;
    private AudioSource m_as;
    void Start()
    {
        m_as = GetComponent<AudioSource>();
        m_as.playOnAwake = false;
        m_as.loop = false;
        if (m_as.isPlaying) m_as.Stop();
    }

    public void PlayHit()
    {
        if (randomPitch)
            m_as.pitch = Random.Range(0.9f, 1.10f);
        m_as.Play();
    }

}
