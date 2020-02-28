using RPG.core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 20f;
        Transform target;

        float timeElapsed;

        private void Update() {

            timeElapsed += Time.deltaTime;

            if (target == null) {
                return;
            }

            if (!GetIsInRange()) {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }




        }

        private void AttackBehaviour() {

            if (timeElapsed > timeBetweenAttacks) {
                GetComponent<Animator>().SetTrigger("attack");
                timeElapsed = 0;
            }
            
        }

        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.position) <= weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel() {
            target = null;
        }

        //animation event
        void Hit() {

            target.GetComponent<Health>().TakeDamage(weaponDamage);

        }
     
    }

}
