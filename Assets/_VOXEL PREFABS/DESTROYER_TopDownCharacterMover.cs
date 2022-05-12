using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DESTROYER_TopDownCharacterMover : MonoBehaviour
{
    private InputHandler _input;
    public GameObject prefab;

    public LayerMask Mask;
    public GameObject BlockGUIPrefab;
    public GameObject BlockPrefab;

    //set at the beginning 
    private Camera _camera;
    private BlockSystem _blockSystem;
    private GameObject _blockGUI;

    //set at each frame
    private bool _canBuild = false;
    private Vector3 _buildPos;
    private int typeSelect = 0;

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private bool rotateTowardsMouse;

    //[SerializeField]
    //private Camera camera;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    private void Start()
    {
        _camera = Camera.main;
        StartBlock();
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        var movementVector = MoveTowardTarget(targetVector);
        if (!rotateTowardsMouse)
            RotateTowardMovementVector(movementVector);
        else
            RotateTowardMouseVector();

        BuildBlock();
    }

    private void RotateTowardMouseVector()
    {
        Ray ray = _camera.ScreenPointToRay(_input.MousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }

    private void RotateTowardMovementVector(Vector3 movementVector)
    {
        if (movementVector.magnitude == 0) { return; }

        var rotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sticky"))
        {
            collision.transform.parent = this.transform;
        }
    }*/

    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, _camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;


    }   

    private void StartBlock()
    {
        _blockSystem = GetComponent<BlockSystem>();
        _blockGUI = Instantiate(BlockGUIPrefab, _buildPos, Quaternion.identity);
        BuildBlock();
    }

    private void BuildBlock()
    {      
        //find the blocks build location
        RaycastHit hit; //shoot ray in direction of camera from centre of screen -- Why are we hitting from the centre of the screen, can that not be from the camera itself because that's where our blockmanager is??
        if (Physics.Raycast(_camera.ScreenPointToRay(new Vector3(Screen.width / 4, Screen.height / 2, 0)), out hit, 10, Mask)) // what is out hit??
        {
            Vector3 pos = hit.point; //get hit position
            _buildPos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z)); //keep the block in a grid
            _canBuild = true;
            _blockGUI.transform.position = _buildPos;//update transparent cube position
        }
        else
        {
            _canBuild = false;
        }
                
        //loop through block types 
        //if (_input.)
        {
            typeSelect++; //increment type select by 1, the same as typeselect = typeselect +1;
            if (typeSelect >= _blockSystem.Blocks.Count)
            {
                typeSelect = 0;
            }

        }

        //build blocks
        if (_canBuild)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBlock();
            }
        }       
    }

    void PlaceBlock()
    {
        GameObject block = Instantiate(BlockPrefab, _buildPos, Quaternion.identity);
        Block type = _blockSystem.Blocks[typeSelect];
        block.name = type.BlockName;
        block.GetComponent<MeshRenderer>().material = type.BlockMaterial; // can set speed, navmesh agent, target location - add thigs in struct and can replace the information which is on struct

    }
}


