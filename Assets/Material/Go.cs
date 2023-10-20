using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go : MonoBehaviour
{
    [SerializeField] float Speed;
    Rigidbody Rb;
    void Start()
    {
        Rb=GetComponent<Rigidbody>();
        Rb.velocity = transform.TransformDirection(Vector3.forward) * Speed;
    }

    private void OnEnable()
    {

        Debug.Log("aa");
       
    }

    
    void Update()
    {
        
    }
}
