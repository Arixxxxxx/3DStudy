using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] Image Dot;
    [SerializeField] Image Aim;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float keyboardRotSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] CinemachineVirtualCamera cam;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 rotateValue = Vector3.zero;
    private Vector3 slopeVelocity = Vector3.zero;

    private float gravity = 9.81f;
    private float mouseSensevity = 100f;
    private float RotMouseSensevity = 300f;
    private float verticalVelo = 0f;
    Camera maincam;

    private CharacterController cCon;
    [SerializeField] bool isGround;
    [SerializeField] bool isJump;
    bool mouseRightClick;
    bool zoomin;
    private void OnDrawGizmos()
    {
        if(cCon == null)
        {
            cCon = GetComponent<CharacterController>();
        }
        Debug.DrawLine(transform.position, transform.position - new Vector3(0, cCon.height * 0.55f,0),Color.red);
    }
    private void Awake()
    {
        if(instance == null)
        {
              instance = this;
        }
        else
        {
            Destroy(instance);
        }

        cCon = GetComponent<CharacterController>();
        
    }
    private void Start()
    {
        maincam = Camera.main;
    }
    private void Update()
    {
        cheakingInput();
        camZoom();
        cheakGround();
        Char_Moving();
        CharJump();
        cheakGravity();
        Char_Rotation();
        CheakSlope();
        MouseCusorHide();
    }

    private void cheakingInput()
    {
        mouseRightClick = Input.GetMouseButtonDown(1);
    }
    private void camZoom()
    {
        if (mouseRightClick)
        {
            zoomin = !zoomin;
        }

        if (zoomin)
        {

            Vector3 offest = new Vector3(2f, 0.5f, 2.5f);
            Aim.enabled = true;
            Dot.enabled = false;
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = offest;
            cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 150;
            
        }

        else
        {
            Vector3 offest = new Vector3(0.5f,1.5f,0);
            Dot.enabled = true;
            Aim.enabled = false;
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = offest;
            cam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 250;
        }
    }
    private void cheakGround()
    {
        //isGround = cCon.isGrounded;
        isGround = false;
        if (verticalVelo <= 0f)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, cCon.height * 0.65f, LayerMask.GetMask("Ground"));
        }
    }

   
    private void Char_Moving()
    {
       
        moveDir = new Vector3(/*Input.GetAxisRaw("Horizontal")*/0f, 0f, Input.GetAxisRaw("Vertical")) * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x = -1 * moveSpeed * Time.deltaTime; 
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x = 1 * moveSpeed * Time.deltaTime; 
        }

        if (isSlope)
        {
            cCon.Move(-slopeVelocity);
        }
        else
        {
            cCon.Move(transform.TransformDirection(moveDir));
        }

        
        
    }

    private void Char_Rotation()
    {
        //if(Input.GetMouseButton(1) == false)
        //{
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        rotateValue.y -= 1 * keyboardRotSpeed * Time.deltaTime; ;
        //    }
        //    if (Input.GetKey(KeyCode.D))
        //    {
        //        rotateValue.y += 1 * keyboardRotSpeed * Time.deltaTime; ;
        //    }
        //}
        //else if(Input.GetMouseButton(1) == true)
        //{
        //    float MouseX = Input.GetAxisRaw("Mouse X") * RotMouseSensevity * Time.deltaTime;
        //    float MouseY = Input.GetAxisRaw("Mouse Y") * RotMouseSensevity * Time.deltaTime;
        //    rotateValue.x += -MouseY;
        //    rotateValue.y += MouseX;
        //}


        transform.rotation = Quaternion.Euler(0, maincam.transform.eulerAngles.y, 0);
        //Camera.main.transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.x , transform.rotation.y, 0);
    }
    private void cheakGravity()
    {
        if (isGround)
        {
            verticalVelo = 0f;
        }
        else
        {
            verticalVelo -= gravity * Time.deltaTime;
        }

        if (isJump)
        {
            isJump = false;
            verticalVelo = jumpForce;
        }

        //캐릭터컨트롤러 무브에는 방향만 집어넣어주면되는듯
        cCon.Move(new Vector3(0,verticalVelo,0) * Time.deltaTime ) ;
       
    }
    [SerializeField] bool isSlope;
    private void CheakSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, cCon.height * 0.7f, LayerMask.GetMask("Ground")))
        {
            Debug.Log("진입");
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            
            if(angle >= cCon.slopeLimit) // 슬롯리밋보다 크면 밀려남
            {
                isSlope = true;
                slopeVelocity = Vector3.ProjectOnPlane(new Vector3(0, gravity, 0), hit.normal) * Time.deltaTime; //캐릭터의 이동직선방향으로부터 
            }
            else
            {
                isSlope = false;
            }
            Debug.Log(angle);
        }
     
    }
    private void CharJump()
    {
        if(!isGround || isSlope) { return; }

        if(Input.GetKeyDown(KeyCode.Space))
        {
           isJump = true;
        }
    }

    private void MouseCusorHide()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked ;
            }
        }
    }
}
