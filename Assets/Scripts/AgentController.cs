using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] goallocations;

    // Start is called before the first frame update
    private void Start()
    {
        goallocations = GameObject.FindGameObjectsWithTag("Goal");
        agent = this.GetComponent<NavMeshAgent>();
        int rand = Random.Range(0, goallocations.Length);
        agent.SetDestination(goallocations[rand].transform.position);
        float sm = Random.Range(0.1f, 1.5f); //step 4
        agent.speed = 2 * sm; //step 4
    }

    //step 3 changing agent priority
    //step 5 FPSController + NavMeshObstacle 

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            int rand = Random.Range(0, goallocations.Length);
            agent.SetDestination(goallocations[rand].transform.position);
        }
    }
}
