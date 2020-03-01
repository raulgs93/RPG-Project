using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {


        Health health;

        void Start() {
            health = GetComponent<Health>();
        }
        void Update() {
            
            if(!health.GetIsAlive()) { return; }

            if (InteractWithCombat()) { return; }
            else if(InteractWithMovement()) { return; }
        }

        private bool InteractWithCombat() {
            RaycastHit[] hits = Physics.RaycastAll(getMouseRay());
            foreach (RaycastHit hit in hits) {

                var target = hit.transform.GetComponent<CombatTarget>();

                if(target == null) { continue;}

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0)) {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }

                return true;
                
            }

            return false;
        }

        public bool InteractWithMovement() {

            bool hasHit = Physics.Raycast(getMouseRay(), out RaycastHit rayHit);

            if (hasHit) {

                if (Input.GetMouseButton(0)) {
                    GetComponent<Mover>().StartMoveAction(rayHit.point, 1f);
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