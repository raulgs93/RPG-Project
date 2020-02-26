using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        void Update() {
            if (InteractWithCombat()) { return; }
            else if(InteractWithMovement()) { return; }
            print("nothing to do.");

        }

        private bool InteractWithCombat() {
            RaycastHit[] hits = Physics.RaycastAll(getMouseRay());
            foreach (RaycastHit hit in hits) {

                var target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;

                if (Input.GetMouseButtonDown(0)) {
                    GetComponent<Fighter>().Attack(target);
                }

                return true;
                
            }

            return false;
        }

        public bool InteractWithMovement() {

            bool hasHit = Physics.Raycast(getMouseRay(), out RaycastHit rayHit);

            if (hasHit) {

                if (Input.GetMouseButton(0)) {
                    GetComponent<Mover>().StartMoveAction(rayHit.point);
                }
              
                return true;
            }

            return false;

        }

        private static Ray getMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}