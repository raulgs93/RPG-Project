using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {

       

        CanvasGroup canvasGroup;

        void Start() {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        IEnumerator FadeOutIn(float time) {
            yield return FadeOut(time);
            print("Faded Out");
            yield return FadeIn(time);
            print("Faded In");
        }

        public IEnumerator FadeOut(float time) {

            while (canvasGroup.alpha < 1) {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
            
        }

        public IEnumerator FadeIn(float time) {

            while (canvasGroup.alpha > 0) {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }

        }

    }

}