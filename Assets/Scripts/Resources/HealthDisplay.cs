using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{

    public class HealthDisplay : MonoBehaviour
    {

        Health health;

        void Awake() {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        void Update() {
            GetComponent<Text>().text = ComposeHealthText();
        }

        private string ComposeHealthText() {
            return String.Format("{0:0}%", health.GetPercentage());
        }
    }

}