using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 rotateValue = Vector3.zero;
    private float gravity = 9.81f;
    private float mouseSensevity = 100f;
    private float verticalVelo = 0f;
    

    private CharacterController cCon;
    [SerializeField] bool isGround;
    [SerializeField] bool isJump;

    private void Awake()
    {
        cCon = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        cheakGround();
        CharJump();
        cheakGravity();
    }

    private void cheakGround()
    {
        //isGround = cCon.isGrounded;
        isGround = false;
        if (verticalVelo <= 0f)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, cCon.height * 0.6f, LayerMask.GetMask("Ground"));
        }
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

    private void CharJump()
    {
        if(!isGround) { return; }

        if(Input.GetKeyDown(KeyCode.Space))
        {
           isJump = true;
        }
    }

}
