using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.RepairTheme {
    public class RepairableTrigger : MonoBehaviour
    {

        public int index = -1;
        public delegate void RepairedHandler(RepairableTrigger trigger);
        public event RepairedHandler Repaired;

        private void OnTriggerEnter2D(Collider2D piece)
        {
            RepairObject repairPiece = piece.GetComponent<RepairObject>();
            if(!repairPiece && repairPiece.Index == index)
            {
                repairPiece.RemovePhysics();
            }

        }
    }
}