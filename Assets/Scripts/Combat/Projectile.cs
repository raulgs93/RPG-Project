using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{

    public class Projectile : MonoBehaviour
    {

        [SerializeField] float projectileSpeed = 10f;
        
        Health target = null;
        float damage = 0;


        private void Update() {
            if (target == null) { return; }

            transform.LookAt(GetAimPosition());
            transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
        }

        private void OnTriggerEnter(Collider other) {

            Health targetHealth = other.GetComponent<Health>();

            if ( targetHealth == target) {
                targetHealth.TakeDamage(damage);
                Destroy(gameObject);
            }
            else {
                return;
            }

        }

        private Vector3 GetAimPosition() {

            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) { return target.transform.position; }

            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        
        }


        public void SetTarget(Health target, float damage) {
            this.target = target;
            this.damage = damage;
        }
    }

}