using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] Transform TargetTR;

    private void Update()
    {
        LookAt();
    }


    private void LookAt()
    {
        if (TargetTR == null) { return; }
        transform.LookAt(TargetTR);
    }
}
