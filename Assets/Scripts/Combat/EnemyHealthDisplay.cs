using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Resources;
using UnityEngine.UI;
using System;
using RPG.Combat;

public class EnemyHealthDisplay : MonoBehaviour
{

    Fighter fighter;
    Health health;

    void Awake() {
        fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
    }

    void Update() {
        health = fighter.GetTarget();
        GetComponent<Text>().text = ComposeHealthText();
    }

    private string ComposeHealthText() {

        if (health == null) return "N/A";
        
        return String.Format("{0:0}%", health.GetPercentage());
    }
}
