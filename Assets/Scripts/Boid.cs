using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boid : MonoBehaviour
{
    public FlockManager Manager;
    private float Speed;
    private bool turning = false;

    // Start is called before the first frame update
    private void Start()
    {
        Speed = Random.Range(Manager.MinSpeed, Manager.MaxSpeed);
    }

    // Update is called once per frame
    public void BoidUpdate()
    {
        //step 9

        Bounds b = new Bounds(Manager.transform.position, Manager.Limits * 2);
        RaycastHit hit;
        Vector3 direction = Manager.transform.position - transform.position; //this value doesn't matter now
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.red);
        if(!b.Contains(this.transform.position))
        {
            turning = true;
        }
        else if(Physics.Raycast(this.transform.position, this.transform.forward, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }
        else
        {
            turning = false;
        }

        //if turing is true
        if(turning)
        {
            Quaternion quat = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp
                    (this.transform.rotation, quat, Manager.RotationSpeed * Time.deltaTime);
            transform.Translate(0, 0, Time.deltaTime * Speed);
            return; //exit, end function
        }

        //random speed/behaviour
        if(Random.Range(0,100) < 10) //ten percent chance the speed will change
        {
            Speed = Random.Range(Manager.MinSpeed, Manager.MaxSpeed);
        }

        if (Random.Range(0, 100) > 20) //eighty percent chance
        {
            transform.Translate(0, 0, Time.deltaTime * Speed);
            return; //exit, end function
        }


        GameObject[] boids = Manager.boids;

        Vector3 groupCenter = Vector3.zero; //1.
        float groupSpeed = 0.01f;
        int groupSize = 0;
        Vector3 avoid = Vector3.zero; //3.

        //update flock calculations
        //1. Move towards the average position of the group.
        //2. Align with the average heading/direction of the group.
        //3. Avoid crowding other flock members
        foreach(GameObject go in boids)
        {
            if(go == this.gameObject){ continue; } //skip, next "gaurd statement"

            float distance = Vector3.Distance(go.transform.position, this.transform.position);
            if(distance > Manager.NeighborDistance) { continue; }

            groupCenter += go.transform.position; //1. 
            groupSize++; //1.

            if(distance < 1.0f)
            {
                avoid += (this.transform.position - go.transform.position); //move in opposite direction
            }

            Boid boidscript = go.GetComponent<Boid>();
            groupSpeed += boidscript.Speed;
        }

        if(groupSize > 0)
        {
            groupCenter = groupCenter / groupSize + (Manager.GoalPos - this.transform.position); //avrage + goal position
            groupSpeed = groupSpeed / groupSize; //average of all boid speed

            direction = (groupCenter + avoid) - this.transform.position;
            if(direction != Vector3.zero)
            {
                Quaternion quat = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp
                    (this.transform.rotation, quat, Manager.RotationSpeed * Time.deltaTime);
            }
        }
        transform.Translate(0, 0, Time.deltaTime * Speed);
    }
}
