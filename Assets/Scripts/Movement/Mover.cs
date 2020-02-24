using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    [SerializeField] Transform target;
    Ray lastRay;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0)) {
            moveToCursor();
        }

        updateAnimator();

    }

    private void updateAnimator() {

        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("speedForward", speed);
    }

    private void moveToCursor() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        bool hasHit = Physics.Raycast(ray, out rayHit);

        if (hasHit) {
            GetComponent<NavMeshAgent>().destination = rayHit.point;
        }

    }
}
