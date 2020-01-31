using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ.RepairTheme
{
    public delegate void RepairCompletedHandler();
    public delegate void RepairedPieceHandler(int piecesLeft);
    public class RepairableObject : MonoBehaviour
    {
        [SerializeField]
        private List<RepairableTrigger> m_repairTriggers;

        public event RepairCompletedHandler RepairCompleted;
        public event RepairedPieceHandler RepairedPiece;
        // Start is called before the first frame update
        private void Start()
        {
            m_repairTriggers = GetComponentsInChildren<RepairableTrigger>().ToList();
            foreach (var trig in m_repairTriggers)
            {

            }
        }

        private void OnPieceReapired()
        {

        }
    }
}