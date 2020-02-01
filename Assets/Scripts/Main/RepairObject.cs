using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GGJ.RepairTheme
{
    public class RepairObject : MonoBehaviour, IRepairs
    {
        public int Index = -1;
        [SerializeField]
        ParticleSystem m_hitEffect;
        private Rigidbody2D m_rigid;
        private DraggableObject m_draggable;
        private PolygonCollider2D m_collider;
        private ObjectHealth m_health;
        private AudioSource m_as;
        public bool IsAlive
        {
            get
            {
                if (!m_health) return true;
                return m_health.IsAlive;
            }
        }

        public event RepairObjectDestroyedHandler RepairDestroyed;

        private void Start()
        {
            m_rigid = GetComponent<Rigidbody2D>();
            m_collider = GetComponentInChildren<PolygonCollider2D>();
            m_draggable = GetComponent<DraggableObject>();
            m_health = GetComponent<ObjectHealth>();
            m_as = GetComponent<AudioSource>();
            if(m_health)
            {
                m_health.HitObject += OnHitObject;
                m_health.HealthDepleted += DestorySelf;
            }
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

        private void OnHitObject(Vector2 pos, Transform other)
        {
            var ps = Instantiate(m_hitEffect, pos, Quaternion.identity);
            ps.Play();
            HitSoundContainer sound = other.GetComponent<HitSoundContainer>();
            if (sound)
                sound.PlayHit();

        }

        public void DestorySelf()
        {
            m_as?.Play();
            RepairDestroyed?.Invoke();
        }
    }
}