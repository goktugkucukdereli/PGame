using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallItem : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private string ItemType;
    [SerializeField] private int BonusBallIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickerLimitObject"))
        {
            if(ItemType == "Pallet")
            {
                _GameManager.ShowPallets();
                gameObject.SetActive(false);
            }
            else if(ItemType == "Pallet")
            {
                _GameManager.AddBonusBalls(BonusBallIndex);
                gameObject.SetActive(false);
            }
        }
    }
}
