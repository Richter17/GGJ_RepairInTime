using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.TimeScale;
public class DraggableObject: MonoBehaviour
{
    public float MaxObjectSpeed = 10;

    public float DisToTimeRatio = 1;

    public float ForceMultiplyer = 20;

    public bool CanBeDragged;
    public bool IsDragged { get; private set; }
    private ITimeController m_timeController;
    private Rigidbody2D m_rigid;
    private Vector3 m_mouseLastPosition;
    private Vector3 m_dragDelta;
    private SpriteRenderer m_arrow;
    private float m_arrowRatio;
    private float m_prevoiusTimeScale;

    protected virtual void Start()
    {
        m_timeController = TimeScaleController.GetTimeController();
        m_rigid = GetComponent<Rigidbody2D>();
        m_arrow = transform.Find("Arrow")?.GetComponent<SpriteRenderer>();
    }

    protected virtual void FixedUpdate()
    {
        m_rigid.AddForce(m_dragDelta);
        //Debug.Log(m_rigid.velocity.magnitude);
        m_rigid.velocity = Vector3.ClampMagnitude(m_rigid.velocity, MaxObjectSpeed);
        if (!m_arrow) return;
        Vector2 arrowSize = m_arrow.size;
        arrowSize.x = Mathf.Lerp(0, 5.12f, m_rigid.velocity.magnitude / MaxObjectSpeed);
        m_arrow.size = arrowSize;
        float angle = Vector3.Angle(Vector3.right, m_rigid.velocity.normalized);
        m_arrow.transform.eulerAngles = new Vector3(0, 0, m_rigid.velocity.y > 0 ? angle : -angle);
        //m_arrow.transform.localRotation = Quaternion.Euler(m_rigid.velocity.normalized);
    }

    protected void OnMouseDown()
    {
        if(CanBeDragged)
            StartDrag();
    }

    protected void OnMouseDrag()
    {
        if(CanBeDragged)
            UpdateDrag();
    }

    protected void OnMouseUp()
    {
        if(CanBeDragged)
            EndDrag();
    }

    protected virtual void StartDrag()
    {
        m_mouseLastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_timeController.StartTimeScaleControl(0);
        IsDragged = true;
    }
    protected virtual void UpdateDrag()
    {
        Vector3 mouseRelative = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float delta = Vector3.Distance(mouseRelative, m_mouseLastPosition) * DisToTimeRatio;
        m_dragDelta = (mouseRelative - transform.position) * ForceMultiplyer;
        m_timeController.StartTimeScaleControl(delta);
        //Debug.Log(m_rigid.velocity);
        //Debug.Log(delta);
        m_mouseLastPosition = mouseRelative;
    }
    public virtual void EndDrag()
    {
        m_timeController.EndTimeScaleControl();
        IsDragged = false;
    }
}
