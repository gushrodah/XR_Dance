using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosOffset : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] bool localOffset;
    [SerializeField] bool lockRot;
    
    void Update()
    {
        if (!localOffset) transform.position = target.position + offset;
        else
        {
            transform.position = target.TransformPoint(offset);
		}

		if (lockRot)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
