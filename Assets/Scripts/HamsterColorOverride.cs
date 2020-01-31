using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HamsterColorOverride : MonoBehaviour
{
    [SerializeField]
    private Color overrideColor;
    [SerializeField]
    private bool Override;
    private SpriteRenderer[] m_renderers;
    // Start is called before the first frame update
    void Start()
    {
        m_renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Override)
        {
            foreach (var sp in m_renderers)
            {
                sp.color = overrideColor;
            }
            Override = false;
        }
    }
}
