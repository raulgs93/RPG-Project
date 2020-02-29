using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{


    public class AIController : MonoBehaviour
    {


        [SerializeField] float chaseDistance = 5f;

        GameObject player;
        Fighter fighter;
        Health health;

        private void Start() {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();

        }


        void Update() {

            if (!health.GetIsAlive()) { return; }

            if (InAttackDistance(player) && fighter.CanAttack(player)) {
                fighter.Attack(player.gameObject);
            }
            else {
                fighter.Cancel();
            }
        }

        public bool InAttackDistance(GameObject target) {
            float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
            return distanceToPlayer < chaseDistance;
        }

    }


}
