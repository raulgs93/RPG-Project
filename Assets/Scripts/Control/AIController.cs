﻿using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using RPG.Resources;


namespace RPG.Control
{


    public class AIController : MonoBehaviour {


        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] float dwellTime = 1f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        

        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;
        ActionScheduler action;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
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
            }
            else if (timeSinceLastSawPlayer < suspicionTime) {
                SuspiciousBehaviour();
            }
            else {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers() {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour() {

            Vector3 nextPosition = guardPosition;


            if (patrolPath != null) {

                if (AtWaypoint()) {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }

           
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > dwellTime) {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
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
            timeSinceLastSawPlayer = 0;
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
