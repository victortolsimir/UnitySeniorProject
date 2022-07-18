using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColorTest : MonoBehaviour
{
    public List<GameObject> items;
    public ArmorColorBlock block;

    bool setColor = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            setColor = !setColor;
            if (setColor)
            {
                foreach(GameObject item in items)
                    item.GetComponent<Renderer>().SetPropertyBlock(block.GetMaterialPropertyBlock());
            }
            else
            {
                foreach (GameObject item in items)
                    item.GetComponent<Renderer>().SetPropertyBlock(null);
            }
            
        }
    }
}
