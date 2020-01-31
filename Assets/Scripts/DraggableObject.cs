using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DraggableObject : MonoBehaviour
{
    public float TimeMul = 1;
    public float speedMul = 1;
    public float maxSpeed = 15;

    public float minTimeScale = 0.1f;
    public float maxTimeScale = 0.85f;

    private Rigidbody2D m_rigid;
    private Vector3 m_mouseLastPosition;
    private Vector3 m_dragDelta;
    private SpriteRenderer m_arrow;
    private float m_arrowRatio;
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_arrow = transform.Find("Arrow")?.GetComponent<SpriteRenderer>();
        SetTimeScale(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        m_rigid.AddForce(m_dragDelta);
        Debug.Log(m_rigid.velocity.magnitude);
        m_rigid.velocity = Vector3.ClampMagnitude(m_rigid.velocity, maxSpeed);
        Vector2 arrowSize = m_arrow.size;
        arrowSize.x = Mathf.Lerp(2.56f, 8f, m_rigid.velocity.magnitude / maxSpeed);
        m_arrow.size = arrowSize;
        float angle = Vector3.Angle(Vector3.right, m_rigid.velocity.normalized);
        m_arrow.transform.localEulerAngles = new Vector3(0, 0, m_rigid.velocity.y > 0 ? angle: -angle);
        //m_arrow.transform.localRotation = Quaternion.Euler(m_rigid.velocity.normalized);
    }


    private void OnMouseDown()
    {
        m_mouseLastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector3 mouseRelative = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float delta = Vector3.Distance(mouseRelative, m_mouseLastPosition) * TimeMul;
        m_dragDelta = (mouseRelative - transform.position) * speedMul;
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
        Time.timeScale = Mathf.Lerp(minTimeScale, maxTimeScale, scale);
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
