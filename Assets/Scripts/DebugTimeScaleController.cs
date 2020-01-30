using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTimeScaleController : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float m_timeScale = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Cursor.visible = !Cursor.visible;
        }

        //Time.timeScale = m_timeScale;
        //Time.fixedDeltaTime = m_timeScale * 0.02f;
    }
}
