using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private List<Camera> cameras = new List<Camera>();


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        Camera Lcams = transform.Find("LeftCam").GetComponent<Camera>();
        cameras.Add(Lcams);

        Camera Rcams = transform.Find("RightCam").GetComponent<Camera>();
        cameras.Add(Rcams);
    }

    private void Start()
    {
        int ListCount = cameras.Count;
        Debug.Log(ListCount);

        for (int i = 0; i < ListCount; i++)
        {
            cameras[i].enabled = false;
        }
    }

    private void Update()
    {
        OnOffCamera();
    }

    private void OnOffCamera()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F1))
        {
           cameras[0].enabled = false;
            cameras[1].enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.F1))
        {
            cameras[0].enabled = false;
            cameras[1].enabled = false;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F2))
        {
            cameras[0].enabled = true;
            cameras[1].enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.F2))
        {
            cameras[0].enabled = true;
            cameras[1].enabled = false;
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F3))
        {
            cameras[0].enabled = false;
            cameras[1].enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.F3))
        {
            cameras[0].enabled = false;
            cameras[1].enabled = true;
        }

    }

    public void F_BtnCamController(int Value)
    {
        int Count = cameras.Count;
        for(int i = 0; i < Count; i++)
        {
            cameras[i].enabled = Value == i;
        }
    }

  
}
