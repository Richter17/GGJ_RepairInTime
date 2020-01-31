using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairObject : MonoBehaviour
{
    public int Index = -1;

    private Rigidbody2D m_rigid;
    private DraggableObject m_draggable;    
    private PolygonCollider2D m_collider;

    private void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_collider = GetComponentInChildren<PolygonCollider2D>();
        m_draggable = GetComponent<DraggableObject>();
    }

    public void RemovePhysics()
    {
        m_rigid.velocity = Vector3.zero;
        m_rigid.isKinematic = true;
        m_collider.enabled = false;
        if(m_draggable)
            m_draggable.CanBeDragged = false;
    }

}
