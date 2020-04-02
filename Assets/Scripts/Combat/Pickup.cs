using RPG.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    [SerializeField] Weapon weapon;
    [SerializeField] float respawnTime = 5f;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Player")) {
            other.GetComponent<Fighter>().EquipWeapon(weapon);
            StartCoroutine(HideForSeconds(respawnTime));
        }
    }

    private IEnumerator HideForSeconds(float time) {

        HidePickup();

        yield return new WaitForSeconds(time);

        ShowPickup();

    }

    private void HidePickup() {

        GetComponent<Collider>().enabled = false;

        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }

    private void ShowPickup() {

        GetComponent<Collider>().enabled = true;

        foreach (Transform child in transform) {
            child.gameObject.SetActive(true);
        }

    }
}
