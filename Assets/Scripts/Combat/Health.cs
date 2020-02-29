using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {

        [SerializeField] float health = 100f;

        bool isAlive = true;

        
        public void TakeDamage(float damage) {

            health = Mathf.Max(health - damage, 0);
            print("health");


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
        }

        public void GetHit() {
            GetComponent<Animator>().SetTrigger("getHit");
            
        }

    }

}
