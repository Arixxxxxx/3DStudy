using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private List<Camera> cameras = new List<Camera>();
    bool CamAroundMode;

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
            F_BtnCamController(5);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.F1))
        {
            F_BtnCamController(5);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F2))
        {
            F_BtnCamController(0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.F2))
        {
            F_BtnCamController(0);
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F3))
        {
            F_BtnCamController(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKey(KeyCode.F3))
        {
            F_BtnCamController(1);
        }

    }

    public void F_BtnCamController(int Value)

    {
        int Count = cameras.Count;

        for(int i = 0; i < Count; i++)
        {
            if(Value > Count)
            {
                cameras[i].enabled = false;
            }
            cameras[i].enabled = Value == i;
                        
        }
    }

  
}
