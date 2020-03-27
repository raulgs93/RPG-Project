using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;


namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
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


        public object CaptureState() {
            return isPlayable;
        }

        public void RestoreState(object state) {
            isPlayable = (bool)state;
        }

    }
}
