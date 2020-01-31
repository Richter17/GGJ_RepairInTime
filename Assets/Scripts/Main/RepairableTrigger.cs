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
            if(repairPiece && repairPiece.Index == index)
            {
                repairPiece.RemovePhysics();
                repairPiece.transform.parent = transform.parent;
                Repaired?.Invoke(this);
                StartCoroutine(TweenToPosition(piece.transform, 2f));
            }

        }

        private IEnumerator TweenToPosition(Transform obj ,float duration)
        {
            float currentTime = 0;
            while (currentTime < duration)
            {
                obj.localPosition = Vector3.Lerp(obj.localPosition, transform.localPosition, currentTime / duration);
                obj.localRotation = Quaternion.Lerp(obj.localRotation, transform.localRotation, currentTime / duration);
                currentTime += Time.deltaTime;
                yield return null;
            }

            obj.localRotation = transform.localRotation;
            obj.localPosition = transform.localPosition;
        }
    }
}