using RPG.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {


        [SerializeField] float fadeInTime = 1.5f;

        const string defaultSaveFile = "save";


        IEnumerator Start() {

            Fader fader = FindObjectOfType<Fader>();
            fader.FadeInImmediate();
            
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.L)){
                Load();
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                Save();
            }
        }

        public void Save() {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public  void Load() {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }

}