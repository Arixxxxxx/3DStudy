using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    
    [SerializeField] private float mouseSen = 100;
    [SerializeField] private float camSpeed = 5;
    [SerializeField] private float RotaionSpeed = 15;
    [SerializeField] GameObject Bullet;
    [SerializeField] Transform Tong;
    Queue<GameObject> BulletTong = new Queue<GameObject>();


    private Vector3 rateteValue; // 현재의 회전값 저장용
    private Camera cam;
    float MouseX, MouseY;
    private void Awake()
    {
        cam = Camera.main;
        
        for (int i = 0; i < 50; i++) 
        {
            GameObject obj_ = Instantiate(Bullet, Tong.transform.position,Quaternion.identity,Tong.transform);
            obj_.gameObject.SetActive(false);
            BulletTong.Enqueue(obj_);
        }
        
    }
    private void Start()
    {
        rateteValue = cam.transform.rotation.eulerAngles;   // 쿼터니언 -> 오일러앵글로 변경
    }

    private void Update()
    {
        moving();
        rotating();
        shoot();
    }

    Vector3 MovePos;
    public bool isRoationOk;

    float RoteX, RoteY;
    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject obj = BulletTong.Dequeue();
            obj.transform.position = cam.transform.position;
            obj.transform.rotation = cam.transform.rotation;
          
            obj.gameObject.SetActive(true);
        }
    }
    private void moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //cam.transform.position += cam.transform.rotation * Vector3.forward * camSpeed * Time.deltaTime;
           cam.transform.position += cam.transform.TransformDirection(Vector3.forward) * camSpeed * Time.deltaTime; // 곱하던가 transform함수를 쓰셈
        }


        if (Input.GetKey(KeyCode.S))
        {
            cam.transform.position -= cam.transform.rotation * Vector3.forward * camSpeed * Time.deltaTime; // 로컬좌표로 가려면 로테이션을 곱하면됨
        }
        if (Input.GetKey(KeyCode.Space))
        {
            cam.transform.position += Vector3.up * camSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.C))
        {
            cam.transform.position += Vector3.down * camSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cam.transform.position += cam.transform.rotation * Vector3.left * camSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cam.transform.position += cam.transform.rotation * Vector3.right * camSpeed * Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(1))
        {
            isRoationOk = true;
        }
        if (Input.GetMouseButton(1))
        {
            if (isRoationOk)
            {
                return;
            }
             else if (!isRoationOk)
            {
                isRoationOk = true;
            }
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRoationOk = false;
        }
        



    }
    private void RoteMathf()
    {
        if (isRoationOk)
        {
            MousePos = cam.ScreenToViewportPoint(Input.mousePosition);
            if (MousePos.x < 0.5f)
            {
                RoteX = -1;
            }
            else if(MousePos.x > 0.5f)
            {
                RoteX = 1;
            }
            if(MousePos.y < 0.5f)
            {
                RoteY = 1;
            }
            else if(MousePos.y > 0.5f)
            {
                RoteY = -1;
            }

            cam.transform.eulerAngles += new Vector3(RoteY, RoteX, cam.transform.rotation.z) * RotaionSpeed * Time.deltaTime;
            Debug.Log($"{MousePos}");
            Debug.Log($"{RoteX},{RoteY}");
        }

    }
    Vector3 MousePos;
    private void rotating()
    {
        MouseX = Input.GetAxisRaw("Mouse X") * mouseSen * Time.deltaTime;
        MouseY = Input.GetAxisRaw("Mouse Y") * mouseSen * Time.deltaTime;
        if (isRoationOk)
        {
            cam.transform.eulerAngles += new Vector3(MouseY * -1, MouseX, cam.transform.rotation.z) * RotaionSpeed *  Time.deltaTime;
        }
        
        Debug.Log($"{MouseX}, {MouseY}");
    }
}
