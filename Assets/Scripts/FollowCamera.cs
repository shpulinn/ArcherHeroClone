using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, followTarget.position.z + offset.z);
    }
}
