using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollideScript : MonoBehaviour
{

    //±Ê√£±‚

    public GameObject currentPath;
    public bool reverse;


    //path trigger
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == currentPath.name)
        {
            currentPath = currentPath.GetComponent<PathScript>().GetTarget(reverse);

        }
    }
}
