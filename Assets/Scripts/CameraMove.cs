using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    internal static object main;
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Target_offset;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + Target_offset, 0.125f);
    }
}
