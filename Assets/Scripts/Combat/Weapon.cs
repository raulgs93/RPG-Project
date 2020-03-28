using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController weaponOverride = null;
        [SerializeField] GameObject weapon = null;

        public void Spawn(Animator animator, Transform handTransform) {

            Instantiate(weapon, handTransform);
            animator.runtimeAnimatorController = weaponOverride;

        }
    }
}
