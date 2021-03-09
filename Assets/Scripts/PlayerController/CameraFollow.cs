using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float interpVelocity;
    public float followSpeed;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //checks if there is a target set
        if (target)
        {
            Vector3 posOffset = transform.position + offset;
            Vector3 targetDirection = (target.transform.position - posOffset);
            float interpVelocity = targetDirection.magnitude * followSpeed;
            targetPos = (transform.position) + (targetDirection.normalized * interpVelocity * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.25f);
        }
    }
}
