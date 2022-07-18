using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class ArmorColorBlock : ScriptableObject
{
    [Header("Color Properties")]
    public Color Primary;
    public Color Secondary;
    public Color Leather_Primary;
    public Color Metal_Primary;
    public Color Leather_Secondary;
    public Color Metal_Dark;
    public Color Metal_Secondary;

    private string[] properties = { "_Color_Primary", "_Color_Secondary", "_Color_Leather_Primary", 
                                    "_Color_Metal_Primary", "_Color_Leather_Secondary", 
                                    "_Color_Metal_Dark", "_Color_Metal_Secondary" };

    public MaterialPropertyBlock GetMaterialPropertyBlock()
    {
        Color[] colors = { Primary, Secondary, Leather_Primary, Metal_Primary, Leather_Secondary, Metal_Dark, Metal_Secondary };
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        for(int i = 0; i < properties.Length; i++)
        {
            if(colors[i].a != 0)
                block.SetColor(properties[i],colors[i]);
        }
        return block;
    }

}

