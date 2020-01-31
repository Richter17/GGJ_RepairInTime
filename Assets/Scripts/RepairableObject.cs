using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ.RepairTheme
{
    public delegate void RepairCompletedHandler(RepairableObject rep);
    public delegate void RepairedPieceHandler(int piecesLeft);
    public class RepairableObject : MonoBehaviour, IRepairs
    {
        [SerializeField]
        private List<RepairableTrigger> m_repairTriggers;

        public event RepairCompletedHandler RepairCompleted;
        public event RepairedPieceHandler RepairedPiece;
        public event RepairObjectDestroyedHandler RepairDestroyed;

        private ObjectHealth m_health;

        // Start is called before the first frame update
        private void Start()
        {
            m_health = GetComponent<ObjectHealth>();
            if(m_health)
                m_health.HealthDepleted += DestorySelf;
            m_repairTriggers = GetComponentsInChildren<RepairableTrigger>().ToList();
            foreach (var trig in m_repairTriggers)
            {
                trig.Repaired += OnPieceReapired;
            }
        }

        private void OnPieceReapired(RepairableTrigger trigger)
        {
            Debug.Log("repaired!");
            m_repairTriggers.Remove(trigger);
            if (m_repairTriggers.Count > 0)
                RepairedPiece?.Invoke(m_repairTriggers.Count);
            else
                RepairCompleted?.Invoke(this);
        }

        public void DestorySelf()
        {
            RepairDestroyed?.Invoke();
        }
    }
}