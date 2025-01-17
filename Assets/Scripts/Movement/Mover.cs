﻿

using RPG.Core;
using RPG.Saving;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement
{

    public class Mover : MonoBehaviour, IAction, ISaveable
    {



        [SerializeField] Transform target;
        [SerializeField] NavMeshAgent navMeshAgent;
        [SerializeField] float maxSpeed = 6f;


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


        public void StartMoveAction(Vector3 destination, float speedFraction) {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction) {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public object CaptureState() {

            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            
            return data;

        }

        public void RestoreState(object state) {

            Dictionary<string, object> data = (Dictionary<string, object>)state;

            GetComponent<NavMeshAgent>().enabled = false;

            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();

            GetComponent<NavMeshAgent>().enabled = true;

        }
    }

}