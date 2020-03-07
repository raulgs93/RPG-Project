using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.SceneManagement;

namespace RPG.Core
{
    public class PortalController : MonoBehaviour
    {

        enum PortalIdentifier
        {
            A, B, C, D, E, F, G
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] PortalIdentifier destination;
        [SerializeField] float fadeOutTime = 0.5f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;


        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                StartCoroutine(Transition());
            }
        }



        public IEnumerator Transition() {

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);


            PortalController otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);

            yield return fader.FadeIn(fadeInTime);


            Destroy(gameObject);

        }

        private PortalController GetOtherPortal() {
            foreach (PortalController portal in FindObjectsOfType<PortalController>()) {
                if (portal == this) { continue; }
                if (portal.destination != destination) { continue; }

                return portal;
            }
            return null;
        }

        private void UpdatePlayer(PortalController otherPortal) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }

}

