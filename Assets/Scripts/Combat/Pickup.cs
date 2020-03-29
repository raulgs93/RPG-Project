using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    [SerializeField] Weapon weapon;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<Fighter>().EquipWeapon(weapon);
            Destroy(gameObject);
        }
    }
}
