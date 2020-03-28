using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController weaponOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponDamage = 20f;
        [SerializeField] float weaponRange = 2f;

        public void Spawn(Animator animator, Transform handTransform) {

            if (equippedPrefab != null) {
                Instantiate(equippedPrefab, handTransform);
            }

            if (weaponOverride != null) {
                animator.runtimeAnimatorController = weaponOverride;
            }

        }

        public float GetWeaponDamage() {
            return weaponDamage;
        }

        public float GetWeaponRange() {
            return weaponRange;
        }



    }
}
