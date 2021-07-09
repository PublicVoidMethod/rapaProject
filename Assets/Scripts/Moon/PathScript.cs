using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{


    public GameObject prevTarget;
    public GameObject nextTarget;

    public GameObject GetTarget(bool getStatus)
    {
        if (getStatus == false)
        {
            return nextTarget;
        } else
        {
            return prevTarget;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
