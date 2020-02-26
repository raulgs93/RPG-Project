using UnityEngine;
using System.Collections;


namespace core
{
    public class ActionScheduler : MonoBehaviour
    {

        MonoBehaviour currentAction = null;
        public void StartAction(MonoBehaviour action) {


            if (currentAction == action) { return; }

            if (currentAction != null) {
                print("Cancelling" + currentAction);   
            }

            currentAction = action;

        }

    }

}
