using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 bulletTarget;
    [SerializeField] float bulletForce = 0;
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, bulletTarget, bulletForce * Time.deltaTime);
    }

    public void F_SetBulletTarget(Vector3 Pos, float _Force)
    {
        bulletTarget = Pos;
        bulletForce = _Force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
