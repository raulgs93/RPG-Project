
using RPG.Core;
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
        [SerializeField] Transform handTransform  = null;
        [SerializeField] Weapon weapon = null;
        

        Health target;

        float timeElapsed = Mathf.Infinity;

        private void Start() {
            SpawnWeapon();
        }

        private void Update() {

            timeElapsed += Time.deltaTime;

            if (target == null) { return; }
            if (!target.GetIsAlive()) { return; }

            if (!GetIsInRange()) {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }




        }

        private void AttackBehaviour() {

            transform.LookAt(target.transform.position);

            if (timeElapsed > timeBetweenAttacks) {
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attack");
                timeElapsed = 0;
            }
            
        }

        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }

        public void Attack(GameObject combatTarget) {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget) {

            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();

            return targetToTest != null && targetToTest.GetIsAlive();

        }


        public void Cancel() {
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
            GetComponent<Mover>().Cancel();
        }

        public void SpawnWeapon() {
            if (weapon == null) { return; }
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(animator, handTransform);
        }

        //animation event
        void Hit() {

            if(target == null) { return; }
            target.TakeDamage(weaponDamage);

        }

     
    }

}
