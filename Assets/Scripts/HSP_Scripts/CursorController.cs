using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public CursorLockMode lockMode = CursorLockMode.None;

    void Start()
    {
        Cursor.lockState = lockMode;
    }
}
