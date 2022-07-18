using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Goal
{

    public string description;
    public bool completed;
    public int currentAmount;
    public int requiredAmount;
    public string goalType;

    public virtual void Init()
    {
        //default int stuff
    }

    public void Evaluate()
    {
        if (currentAmount >= requiredAmount)
        {
            Debug.Log("" + currentAmount + "/" + requiredAmount);
            Complete();
        }
    }

    public void Complete()
    {
        
        completed = true;
       
        Debug.Log("Goal is completed");
    }


}
