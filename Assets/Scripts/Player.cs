using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public float A = 0;
    public float B = 1;

    [Range(0,1)] //way to modify - more useful
    public float T = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Vector3 move = new Vector3(0, 0, 0); //set only once at the beginning 

    // Update is called once per frame
    void Update()
    {
        //float lerp = Mathf.Lerp(A, B, T);

        
        float angle = 0;
        float speed = 0;


        if (Input.GetKey(KeyCode.LeftArrow)) { speed = -0.1f; }
        if (Input.GetKey(KeyCode.RightArrow)) { speed = 1.1f; }
        if (Input.GetKey(KeyCode.LeftArrow)) { angle -= 1f; }
        if (Input.GetKey(KeyCode.RightArrow)) { angle += 1f; }
        //if (Input.GetKey(KeyCode.UpArrow)) { move.y += 1.1f; }
        //if (Input.GetKey(KeyCode.DownArrow)) { move.y -= 0.1f; }
        //if (Input.GetKey(KeyCode.UpArrow)) { forward.y += 1f; }
        //if (Input.GetKey(KeyCode.DownArrow)) { forward.y -= 1f; }

        Vector3 move = this.transform.forward * speed;
        

        //this.transform.forward = forward;
        this.transform.Rotate(Vector3.up, angle);
        this.transform.Translate(move); //this.transform.position += move; //this.transform.position = move;
        
        //Debug.Log(T);
    }
}
