using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorStatus : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private Animator BarrierArea;

    public void BarrierRemove()
    {
        BarrierArea.Play("BarrierRemove");
    }

    public void Finish()
    {
        _GameManager.PickerMoveStatus = true;
    }
}
