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
        }
        else
        {
            return prevTarget;
        }
    }
}