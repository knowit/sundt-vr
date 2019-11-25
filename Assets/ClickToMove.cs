using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private RaycastHit hitInfo = new RaycastHit();
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                agent.destination = hitInfo.point;
                for (int i = 0; i < agent.path.corners.Length; i++)
                {
                    Debug.Log(agent.path.corners[i].ToString());
                }
            }
        }
    }
}
