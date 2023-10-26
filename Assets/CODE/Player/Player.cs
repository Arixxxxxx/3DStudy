using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player instance;


    private Rigidbody Rb;
    private Vector3 playerMove;
    private bool PlayerMode;
    private CapsuleCollider CapsColl;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float RayDisVlaue;
    private GameManager gm;
    [SerializeField] bool isGround;
    Camera cam;
    Vector3 OriginCamPos;

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        CapsColl = GetComponent<CapsuleCollider>();
        Rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        gm = GameManager.Instance;
        cam  = Camera.main;
        OriginCamPos = cam.transform.position;
    }
    private void FixedUpdate()
    {
        MoveChar();
        //movePlayer();
    }

    private void Update()
    {
        ModeCheaker();
        CheakGrounded();
        rotating();
    }

    private void CheakGrounded()
    {
        isGround = false;

        if (Rb.velocity.y <= 0)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, CapsColl.height * 0.55f, LayerMask.GetMask("Ground"));
            Debug.DrawLine(transform.position, Vector3.down, Color.red, CapsColl.height * 0.55f);
        }
    }

    Vector3 moveDir;
    private void movePlayer()
    {
        if (!PlayerMode) { return; }

        moveDir.x = InputHorizontal() ;
        moveDir.z = InputVertical();
        moveDir.y = Rb.velocity.y;
        Rb.velocity = transform.TransformDirection(moveDir);
        
    }

    float MouseX, MouseY;
    [SerializeField] Transform Head, Leg;
    Vector3 RotatVec;
    [SerializeField] float mouseSen;
    [SerializeField] Vector3 PlayerRotY;
    [SerializeField] Vector3 MovePosCam;
    [SerializeField] Vector3 PlayerVec;
    [SerializeField] Vector3 MovePosPlayer;
    [SerializeField] float CamRotSpeed;
    float MouseYY;

    Vector3 StartVec;
    Vector3 CurVec;

    float StartMouseY;
    float CurMouseY;
    float SumMouseY;

    bool MouseYEnd;

    Vector3 CurCamRot;

    private void CamZoomin(string Value)
    {
        switch (Value)
        {

            case "IN":

                break;

            case "OUT":

                break;

        }

    }

    float curVecY;
    private void rotating()
    {
        if(!PlayerMode) { return; }
        if (Input.GetKey(KeyCode.Mouse1) == false) 
        {
            StartMouseY = 0;
            CurMouseY = 0;
            return; 
        }

        if (!MouseYEnd)
        {
            MouseYEnd = true;
            StartVec = cam.ViewportToWorldPoint(Input.mousePosition);
            StartMouseY = StartVec.y;
        }
       
        MouseX = Input.GetAxisRaw("Mouse X") * mouseSen * Time.deltaTime;
        MouseY = Input.GetAxisRaw("Mouse Y") * mouseSen * Time.deltaTime;
        RotatVec.y = MouseX; // 좌우방향
        transform.rotation *= Quaternion.Euler(0, RotatVec.y, 0);

        RotatVec.x = MouseY * -1;
        
        Debug.Log($"MouseX = {MouseY}, MouseY = {RotatVec.x}");

        
        MouseYY = Mathf.Sign(MouseY);
        
        RotatVec.x = Mathf.Clamp(RotatVec.x, -45, 45);

        PlayerRotY = F_GetPlayerPosAndRot("Rot");
        PlayerVec = F_GetPlayerPosAndRot("Pos");

        CurCamRot.x = cam.transform.eulerAngles.x + RotatVec.x;

       
        cam.transform.LookAt(PlayerVec);
        if(RotatVec.x > 0)
        {
            if (cam.transform.position.y > 8)
            {
                cam.transform.position = new Vector3(cam.transform.position.x, 8, cam.transform.position.z);
            }
            cam.transform.position += cam.transform.TransformDirection(new Vector3(0, cam.transform.position.y + RotatVec.x, 0)) * Time.deltaTime;
        }
        else if(RotatVec.x < 0)
        {
            if(cam.transform.position.y < 0.5f)
            {
                cam.transform.position = new Vector3(cam.transform.position.x, 0.5f, cam.transform.position.z);
            }
            cam.transform.position -= cam.transform.TransformDirection(new Vector3(0, cam.transform.position.y - RotatVec.x, 0)) * Time.deltaTime;

        }
        


        //Debug.Log($"{MouseX} / {CurMouseY}  / {SumMouseY} ");

        //if (MouseYY > 0)
        //{
        //    if (cam.transform.position.y > 8) 
        //    {
        //        return;
        //    }
        //    cam.transform.position += cam.transform.TransformDirection(Vector3.up * CamRotSpeed * Time.deltaTime);

        //}
        //else if(MouseYY < 0)
        //{
        //    if (cam.transform.position.y < 0.5f)
        //    {
        //        return;
        //    }
        //    cam.transform.position -= cam.transform.TransformDirection(Vector3.up * CamRotSpeed * Time.deltaTime);

        //}
        //if (MouseYY < 0)
        //{
        //   
        //    cam.transform.position += cam.transform.TransformDirection(Vector3.down * CamRotSpeed *  Time.deltaTime);
        //}
        //else if (MouseY < 0)
        // {
        //     Debug.Log("아래로");

        // }
        //else if(MouseY == 0)
        // {

        // }


    }



    private float InputHorizontal()
    {
        //return Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D))
        {
            return 1 * moveSpeed ;
        }
        else if(Input.GetKey(KeyCode.A)|| Input.GetKeyDown(KeyCode.A))
        {
            return -1 * moveSpeed ;
        }
        return 0;
    }

    private float InputVertical()
    {
        return Input.GetAxisRaw("Vertical") * moveSpeed;
    }
    private void MoveChar()
    {
        if (!PlayerMode) { return; }

        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.W))
        {
            Rb.velocity = transform.TransformDirection(Vector3.forward) * moveSpeed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKeyDown(KeyCode.S))
        {
            Rb.velocity = transform.TransformDirection(Vector3.back) * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles += Vector3.up * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles += Vector3.down * moveSpeed;
        }
        if (Input.GetKey(KeyCode.Q) || Input.GetKeyDown(KeyCode.Q))
        {
            Rb.velocity = transform.TransformDirection(Vector3.left) * moveSpeed;
        }
        if (Input.GetKey(KeyCode.E) || Input.GetKeyDown(KeyCode.E))
        {
            Rb.velocity = transform.TransformDirection(Vector3.right) * moveSpeed;
        }
    }

    private void ModeCheaker()
    {
        PlayerMode = gm.F_CamModeChaker(0);
    }

    public Vector3 F_GetPlayerPosAndRot(string _value)
    {
        switch (_value)
        {
            case "Pos":
                Vector3 PlayerPos = transform.localPosition;
                return PlayerPos;


            case "Rot":
                Vector3 PlayerRot = transform.eulerAngles;
                return PlayerRot;

        }

        return Vector3.zero;
    }
}
