using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DraggableObject : MonoBehaviour
{
    public float TimeMul = 1;
    public float speedMul = 1;
    private Rigidbody2D m_rigid;
    private Vector3 m_mouseLastPosition;
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        SetTimeScale(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        m_mouseLastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector3 mouseRelative = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float delta = Vector3.Distance(mouseRelative, m_mouseLastPosition) * TimeMul;
        m_rigid.velocity = (mouseRelative - transform.position) * speedMul;
        SetTimeScale(delta);
        //Debug.Log(m_rigid.velocity);
        //Debug.Log(delta);
        m_mouseLastPosition = mouseRelative;
    }

    private void OnMouseUp()
    {
        SetTimeScale(0);
    }

    private void SetTimeScale(float scale)
    {
        Time.timeScale = Mathf.Clamp(scale, 0, 1);
        Time.fixedDeltaTime = scale * 0.02f;
    }
}
