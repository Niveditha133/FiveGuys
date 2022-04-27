using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public float Speed = 30f;
    private Camera _camera;
    private Rigidbody _rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        _camera = Camera.main; //just grab the camera by using main
    }

    private float Size = 1;
    // Update is called once per frame
    private void FixedUpdate() //mainly for using physics operations in unity
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(x, 0, z); //getting new vectors
        //Debug.Log(input);              
        Vector3 move = (input.z * _camera.transform.forward) + (input.x * _camera.transform.right);
        _rigidBody.AddForce(move * Speed * Time.fixedDeltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sticky") && collision.transform.localScale.magnitude <= Size)
        {            
            collision.transform.parent = this.transform;
            Size += collision.transform.localScale.magnitude;
        }
    }
}
