﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {

        bool isPlayable = true;

        private void OnTriggerEnter(Collider other) {

            if(isPlayable && other.gameObject.CompareTag("Player")) {
                PlayCinematic();
            }

        }

        private void PlayCinematic() {
            GetComponent<PlayableDirector>().Play();
            isPlayable = false;
        }
    }
}
