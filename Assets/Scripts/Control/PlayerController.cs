using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            moveToCursor();
        }
    }

    public void moveToCursor() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        bool hasHit = Physics.Raycast(ray, out rayHit);

        if (hasHit) {
           GetComponent<Mover>().moveTo(rayHit.point);
        }

    }
}
