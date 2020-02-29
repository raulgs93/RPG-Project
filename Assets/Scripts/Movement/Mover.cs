﻿

using RPG.Core;

using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{

    public class Mover : MonoBehaviour, IAction
    {



        [SerializeField] Transform target;
        [SerializeField] NavMeshAgent navMeshAgent;


        void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update() {
            UpdateAnimator();

        }

        public void Cancel() {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator() {

            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("speedForward", speed);
        }


        public void StartMoveAction(Vector3 destination) {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination) {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

     
    }

}