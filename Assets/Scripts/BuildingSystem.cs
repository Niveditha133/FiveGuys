using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _blockSystem = GetComponent<BlockSystem>();
        _blockGUI = Instantiate(BlockGUIPrefab, _buildPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //find the blocks build location
        RaycastHit hit; //shoot ray in direction of camera from centre of screen -- Why are we hitting from the centre of the screen, can that not be from the camera itself because that's where our blockmanager is??
        if (Physics.Raycast(_camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out hit, 10, Mask)) // what is out hit??
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
        if (Input.GetMouseButton(1))            
            {
            typeSelect++; //increment type select by 1, the same as typeselect = typeselect +1;
            if(typeSelect >= _blockSystem.Blocks.Count)
            {
                typeSelect =  0;
            }
        
        }

        //build blocks
        if(_canBuild)
        {
            if(Input.GetMouseButtonDown(0))                
            {
                PlaceBlock();
            }
        }
    }

    private void PlaceBlock()
    {
        GameObject block = Instantiate(BlockPrefab, _buildPos, Quaternion.identity);
        Block type = _blockSystem.Blocks[typeSelect];
        block.name = type.BlockName;
        block.GetComponent<MeshRenderer>().material = type.BlockMaterial; // can set speed, navmesh agent, target location - add thigs in struct and can replace the information which is on struct

    }

}
