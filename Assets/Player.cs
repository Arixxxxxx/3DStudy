using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody Rb;
    Vector3 playerMove;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        Rb.velocity = new Vector3(0,0,playerMove.z) * 2; 
    }
    private void MoveChar()
    {
        
    }
}
