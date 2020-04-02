using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{

    public class Projectile : MonoBehaviour
    {

        [SerializeField] float projectileSpeed = 10f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float destroyDelay = 10f;
        
        Health target = null;
        float damage = 0;

        private void Start() {
            transform.LookAt(GetAimPosition());
        }

        private void Update() {
            if (target == null) { return; }

            if (isHoming && target.GetIsAlive()) {
                transform.LookAt(GetAimPosition());
            }

            transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);

        }

        private void OnTriggerEnter(Collider other) {

            Health targetHealth = other.GetComponent<Health>();

            if ( targetHealth == target && target.GetIsAlive()) {
                targetHealth.TakeDamage(damage);

                if (hitEffect != null) {
                    GameObject particleHit = Instantiate(hitEffect, GetAimPosition(), transform.rotation);
                }

                Destroy(gameObject);
            }
            else {
                Destroy(gameObject, destroyDelay);
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