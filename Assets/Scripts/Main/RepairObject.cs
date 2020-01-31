using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GGJ.RepairTheme
{
    public class RepairObject : MonoBehaviour, IRepairs
    {
        public int Index = -1;

        private Rigidbody2D m_rigid;
        private DraggableObject m_draggable;
        private PolygonCollider2D m_collider;
        private ObjectHealth m_health;

        public event RepairObjectDestroyedHandler RepairDestroyed;

        private void Start()
        {
            m_rigid = GetComponent<Rigidbody2D>();
            m_collider = GetComponentInChildren<PolygonCollider2D>();
            m_draggable = GetComponent<DraggableObject>();
            m_health = GetComponent<ObjectHealth>();
            if(m_health)
                m_health.HealthDepleted += DestorySelf;
        }

        public void RemovePhysics()
        {
            m_rigid.velocity = Vector3.zero;
            m_rigid.isKinematic = true;
            m_collider.enabled = false;
            if (m_draggable)
            {
                m_draggable.EndDrag();
                m_draggable.CanBeDragged = false;
            }
        }

        public void DestorySelf()
        {
            RepairDestroyed?.Invoke();
        }
    }
}