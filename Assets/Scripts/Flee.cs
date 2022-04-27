using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : MonoBehaviour
{
    private GameObject[] agents;

    // Start is called before the first frame update
    private void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("Agent");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            foreach(GameObject agent in agents)
            {
                agent.GetComponent<AgentFleeController>().DetectNewObstacle(this.transform.position);
            }
        }
    }
}
