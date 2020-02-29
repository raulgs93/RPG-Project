using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {

        [SerializeField] float health = 100f;

        bool isAlive = true;

        
        public void TakeDamage(float damage) {

            health = Mathf.Max(health - damage, 0);

            if (isAlive) {
                if (health == 0) {
                    Die();
                }
                else {
                    GetHit();
                }
            }
            
        }

        public void Die() {
            GetComponent<Animator>().SetTrigger("die");
            isAlive = false;
            GetComponent<ActionScheduler>().CancelAction();
        }

        public void GetHit() {
            GetComponent<Animator>().SetTrigger("getHit");
            
        }

        public bool GetIsAlive() {
            return isAlive;
        }

    }

}
