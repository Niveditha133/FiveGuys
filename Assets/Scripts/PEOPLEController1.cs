using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(Rigidbody))]
public class PEOPLEController1 : MonoBehaviour
{
    //[SerializeField]
    //private float Speed;

    //private float Size = 1;

    //private Rigidbody _rigidbody;
    private Camera _camera;

    private bool operate = true;

    //waitime set to the timer
    [SerializeField]
    [Range(0, 5)]
    private float waiTime = 2f;

    //timer
    private float timer;

    //temporary position holder
    private Vector3 tmptPosition;

    //temporary rotation holder
    private Quaternion tmptRotation;

    // Start is called before the first frame update
    private void Start()
    {
        //_rigidbody = gameObject.GetComponent<Rigidbody>();
        //_camera = Camera.main;

        //set the initial timer
        timer = waiTime;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        TimerFunction();

        /*float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");
        //Vector3 input = new Vector3(x, 0, z);

        //Vector3 move = (input.z * _camera.transform.forward) + (input.x * _camera.transform.right);

        /if (operate == true)
        {
            _rigidbody.AddForce(move * Speed * Time.fixedDeltaTime * Size); //fixed delta time
        }*/
    }

    private void OnCollisionEnter(Collision collision)

    {
        /*if (collision.gameObject.CompareTag("BigSticky"))
        //collision.transform.parent = this.transform;
        //Size += collision.transform.localScale.magnitude;
        {
            this.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
            Destroy(collision.gameObject);
        }*/
        //gameObject.CompareTag(B) = Destroy;
        //gameobjectswithtag("BigSticky") = Destroy;
        /*if (collision.gameObject.CompareTag("SmallSticky"))
        {
            Debug.Log("small");
            this.transform.localScale -= new Vector3(1.2f, 1.2f, 1.2f);
            Destroy(collision.gameObject);
        }*/

        if (collision.gameObject.CompareTag("Stop"))
        {
            //set the boolean to false
            operate = false;

            //hold the instant position
            tmptPosition = transform.position;
            //hold the instant rotation
            tmptRotation = transform.rotation;

            Destroy(collision.gameObject);
        }
    }

    private void TimerFunction()
    {
        if (operate == false)
        {
            //freeze the positoin
            transform.position = tmptPosition;
            //freeze the rotation
            transform.rotation = tmptRotation;

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                operate = true;
                timer = waiTime;
            }
        }
    }
}