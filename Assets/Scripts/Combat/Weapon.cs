using RPG.Core;
using System;
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
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(Animator animator, Transform rightHand, Transform leftHand) {

            DestroyOldWeapon(rightHand, leftHand);

            if (equippedPrefab != null) {
                Transform hand = GetTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(equippedPrefab, hand);
                weapon.name = weaponName;
            }

            if (weaponOverride != null) {
                animator.runtimeAnimatorController = weaponOverride;
            }
            else {
                var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
                if(overrideController != null) {
                    animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
                }
            }

        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand) {

            Transform oldWeapon = rightHand.Find(weaponName);

            if (oldWeapon == null) {

                oldWeapon = leftHand.Find(weaponName);

                if (oldWeapon == null) { return; }  
                
            }

            Destroy(oldWeapon.gameObject);
        }

        public bool HasProjectile() {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target) {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }


        private Transform GetTransform(Transform rightHand, Transform leftHand) {
            Transform hand;

            if (isRightHanded) { hand = rightHand; }
            else { hand = leftHand; }

            return hand;
        }

        public float GetWeaponDamage() {
            return weaponDamage;
        }

        public float GetWeaponRange() {
            return weaponRange;
        }



    }
}
