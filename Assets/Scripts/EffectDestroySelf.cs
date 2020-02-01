using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroySelf : MonoBehaviour
{
    private ParticleSystem m_ps;
    // Start is called before the first frame update
    void Start()
    {
        m_ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_ps.isPlaying)
            Destroy(gameObject);
    }
}
