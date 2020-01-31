using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeepMainCamera : MonoBehaviour
{
    private Canvas m_canvas;
    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GetComponent<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_canvas.worldCamera && Camera.main)
            m_canvas.worldCamera = Camera.main;
    }
}
