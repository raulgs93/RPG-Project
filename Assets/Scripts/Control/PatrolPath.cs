using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {

        [SerializeField] float radius = .2f;
        private void OnDrawGizmos() {
            for (int i = 0; i < transform.childCount; i++) {
                Gizmos.DrawSphere(GetWaipoint(i), radius);
                Gizmos.DrawLine(GetWaipoint(i), GetWaipoint(GetNextIndex(i)));
            }
        }

        public Vector3 GetWaipoint(int i) {
            return transform.GetChild(i).position;
        }

        public int GetNextIndex(int i) {
            if (i+1 == transform.childCount) {
                return 0;
            }

            return i + 1; 
        }
    }

}