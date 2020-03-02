using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {

        GameObject player;

        void Start() {
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        void EnableControl(PlayableDirector playableDirector) {
            player.GetComponent<PlayerController>().enabled = true;
        }

        void DisableControl(PlayableDirector playableDirector) {
            player.GetComponent<ActionScheduler>().CancelAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

    }

}