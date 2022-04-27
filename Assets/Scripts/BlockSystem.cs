using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSystem : MonoBehaviour
{
    public BlockType[] BlockTypes;

    [HideInInspector]
    public Dictionary<int, Block> Blocks = new Dictionary<int, Block>();

    private void Awake() //To build the new block from the array of blocks??
    {
        for(int i = 0; i<BlockTypes.Length; i++)
        {
            BlockType blockType = BlockTypes[i];
            Block block = new Block(i, blockType.BlockName, blockType.BlockMaterial);
            Blocks.Add(i, block);
        }
    }
}

public class Block //custom class is to replace the large number of lists
{
    public int BlockID;
    public string BlockName;
    public Material BlockMaterial;

    public Block(int id, string name, Material mat)
    {
        BlockID = id;
        BlockName = name;
        BlockMaterial = mat;
    }
}

[Serializable] //Whats the use of this? 
//Is it just the way to add all the arrays into one and not declare them as seperate list of arrays?

//What is stuct?? -- allows us to 
public struct BlockType
{
    public string BlockName;
    public Material BlockMaterial;
}

