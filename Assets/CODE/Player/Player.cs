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
    }
    private void FixedUpdate()
    {
        //MoveChar();
        movePlayer();
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
    Vector3 RotatVec;
    [SerializeField] float mouseSen;
    private void rotating()
    {
        if(!PlayerMode) { return; }
        if (Input.GetKey(KeyCode.Mouse1) == false) { return; }

        MouseX += Input.GetAxisRaw("Mouse X") * mouseSen * Time.deltaTime;
        MouseY += Input.GetAxisRaw("Mouse Y") * mouseSen * Time.deltaTime;

        RotatVec.x = MouseY * -1;
        RotatVec.y = MouseX;
        RotatVec.x = Mathf.Clamp(RotatVec.x, -45, 45);


        transform.rotation = Quaternion.Euler(RotatVec);


        Debug.Log($"{MouseX}, {MouseY}");
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
