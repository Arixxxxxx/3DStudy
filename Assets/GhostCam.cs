using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    
    [SerializeField] private float mouseSen = 100;
    [SerializeField] private float camSpeed = 5;

    private Vector3 rateteValue; // ������ ȸ���� �����
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;

    }
    private void Start()
    {
        rateteValue = cam.transform.rotation.eulerAngles;   // ���ʹϾ� -> ���Ϸ��ޱ۷� ����
    }

    private void Update()
    {
        moving();
        rotating();
    }

    Vector3 MovePos;
    private void moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cam.transform.position += Vector3.forward * camSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cam.transform.position -= Vector3.forward * camSpeed * Time.deltaTime;
        }
    }
    private void rotating()
    {

    }
}
