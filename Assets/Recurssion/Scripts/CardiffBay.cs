using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardiffBay : MonoBehaviour
{
    public GameObject Cell;
    public int Recurse = 3;

    private void Grow(int recurse, Transform parent, float scale = 1)
    {
        if (recurse == 0 )
        {
            return; //if recurse is 0, exit
        }

        

        recurse--;
        GameObject cellA = Instantiate(Cell, parent.transform.position, Quaternion.identity, null);
        GameObject cellB = Instantiate(Cell, parent.transform.position, Quaternion.identity, null);
        cellA.name = "CellA" + recurse;
        cellB.name = "CellB" + recurse;

        //TODO: rotate the objects     
        Vector3 dirA = parent.right;
        Vector3 dirB = parent.forward - parent.right; //45 degree angle
        cellA.transform.forward = dirA;
        cellB.transform.forward = dirB;

        //TODO: move the objects  
        
        cellA.transform.localPosition = parent.position + (dirA * scale);
        cellB.transform.localPosition = parent.position + (dirB * scale);

        //TODO: scale the objects
        scale = scale / 1.5f;
        cellA.transform.localScale = cellA.transform.localScale * scale;
        cellB.transform.localScale = cellB.transform.localScale * scale;

        //cellA.transform.parent = parent; cellB.transform.parent = parent;

        //recurse
        Grow(recurse, cellA.transform, scale);
        Grow(recurse, cellB.transform, scale);
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        GameObject cell = Instantiate(Cell, this.transform.position, Quaternion.identity, null);
        Grow(Recurse, cell.transform);
    }

    // Update is called once per frame
    void Update()
    {        
    }
}
