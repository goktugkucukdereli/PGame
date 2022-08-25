using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwivelArm : MonoBehaviour
{
    bool Spin;
    [SerializeField] private float SpinValue;
    public void SpinStart()
    {
        Spin = true;
    }


    void Update()
    {
        if(Spin)
        transform.Rotate(0, 0, SpinValue, Space.Self);
    }
}
