using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnContoller : MonoBehaviour
{
    [SerializeField] Transform CamManager;
    List<Camera> cameras = new List<Camera>();
    Button[] Btns;

    private void Awake()
    {
        Camera Lcam = CamManager.transform.Find("LeftCam").GetComponent<Camera>();
        Camera Rcam = CamManager.transform.Find("RightCam").GetComponent<Camera>();

        cameras.Add(Lcam);
        cameras.Add(Rcam);

        Btns = new Button[transform.Find("Btns").childCount];
        int count = Btns.Length;

        for (int i = 0; i < count; i++)
        {
            Btns[i] = transform.Find("Btns").GetChild(i).GetComponent<Button>();
        }
        
    }
    void Start()
    {
        Btns[0].onClick.AddListener(() => { cameras[0].enabled = true; cameras[1].enabled = false; });
        Btns[1].onClick.AddListener(() => { cameras[0].enabled = false;  cameras[1].enabled = true;  });
        Btns[2].onClick.AddListener(() => { cameras[0].enabled = false;  cameras[1].enabled = false; });
    }


}
