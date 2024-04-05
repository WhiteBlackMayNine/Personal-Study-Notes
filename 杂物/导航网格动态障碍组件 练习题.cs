using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lesson62_Exercises : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0) )
        {
            RaycastHit hit;
            if( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) )
            {
                print(hit.collider.name);
                agent.SetDestination(hit.point);
            }
        }

        if( Input.GetMouseButtonDown(1) )
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, 1 << LayerMask.NameToLayer("Wall")))
            {
                hit.collider.gameObject.SetActive(false);
            }
        }

        if( agent.velocity == Vector3.zero )
            animator.SetInteger("Speed", 0);
        else
            animator.SetInteger("Speed", 1);
    }
}
