﻿using RPG.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{

    public class Mover : MonoBehaviour
    {



        [SerializeField] Transform target;
        [SerializeField] NavMeshAgent navMeshAgent;


        void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update() {
            UpdateAnimator();

        }

        public void Stop() {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator() {

            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("speedForward", speed);
        }


        public void StartMoveAction(Vector3 destination) {
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination) {

            

            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }
    }

}