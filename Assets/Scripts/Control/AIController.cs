using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{


    public class AIController : MonoBehaviour
    {


        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;

        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
        ActionScheduler action;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;
        

        private void Start() {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            action = GetComponent<ActionScheduler>();

            guardPosition = transform.position;

        }


        void Update() {

            if (!health.GetIsAlive()) { return; }

            if (InAttackDistance(player) && fighter.CanAttack(player)) {
                AttackBehaviour();
                timeSinceLastSawPlayer = 0;
            }
            else if (timeSinceLastSawPlayer < suspicionTime) {
                SuspiciousBehaviour();
            }
            else {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour() {

            Vector3 nextPosition = guardPosition;


            if (patrolPath != null) {

                if (AtWaypoint()) {
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();

            }
            mover.StartMoveAction(nextPosition);
        }

        private bool AtWaypoint() {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint() {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint() {
            return patrolPath.GetWaipoint(currentWaypointIndex);
        }


      
        private void SuspiciousBehaviour() {
            action.CancelAction();
        }

        private void AttackBehaviour() {
            fighter.Attack(player.gameObject);
        }

        public bool InAttackDistance(GameObject target) {
            float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }


}
