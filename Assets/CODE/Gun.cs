using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    private Rigidbody Rb;
    Camera cam;
    [SerializeField] float aimDis = 200;
    [SerializeField] GameObject obj_bullet;
    [SerializeField] Transform bullet_Hole;
    [SerializeField] Transform bullet_Tong;
    Vector3 TargetPos;
    bool isFire;
    [SerializeField] float shootingSpeed;
    bool isFireCannon;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        cam = Camera.main;  

    }

    // Update is called once per frame
    void Update()
    {
        GunTargetPoiter();
        isShooting();
    }
    
    private void GunTargetPoiter()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, aimDis, LayerMask.GetMask("Ground")))
        {
            transform.LookAt(hit.point);    
            TargetPos = hit.point;
        }
        else
        {
            transform.LookAt(cam.transform.position + cam.transform.forward * aimDis);
            TargetPos = cam.transform.position + cam.transform.forward * aimDis;
        }
        //transform.LookAt(cam.transform.position + cam.transform.forward * aimDis);
        //TargetPos = cam.transform.position + cam.transform.forward * aimDis;
    }

    float counter;
    private void isShooting()
    {
        counter += Time.deltaTime;
        isFire = counter > shootingSpeed;
        if (Input.GetMouseButton(0) && isFire)
        {
            counter = 0;
            //무기공속
            ShootBullet();
        }
    }

    private void ShootBullet()
    {
        GameObject obj = Instantiate(obj_bullet, bullet_Hole.position, bullet_Hole.rotation, bullet_Tong.transform);
        BulletController sc = obj.GetComponent<BulletController>();
        sc.F_SetBulletTarget(TargetPos, 50);
        

        //if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, aimDis, LayerMask.GetMask("Ground")))
        //{
        //    sc.F_SetBulletTarget(hit.point, 80);

        //}
        //else
        //{
        //    sc.F_SetBulletTarget(cam.transform.position + cam.transform.forward * aimDis, 80);
        //}
    }

    public void Addforce(float _force)
    {
        Rb.useGravity = true;
        Rb.AddForce(transform.TransformDirection(Vector3.forward * _force),ForceMode.Impulse );

    }
}
