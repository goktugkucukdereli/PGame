using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

[Serializable]

public class BallAreaOperations
{
    public Animator BallAreaElevator;
    public TextMeshProUGUI NumberText;
    public int BallThrown;
    public GameObject[] Balls;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PickerObject;
    [SerializeField] private GameObject[] PickerPallets;
    [SerializeField] private GameObject[] BonusBalls;
    bool isPallets;
    [SerializeField] private GameObject BallCtrlObject;
    public bool PickerMoveStatus;
    public GameObject TryAgain;

    int BallNumberThrown;
    int TotalCheckPointNumber;
    int AvailableCheckPointIndex;
    float TouchPosX;

    [SerializeField] private List<BallAreaOperations> _BallAreaOperations = new List<BallAreaOperations>();
    void Start()
    {
        PickerMoveStatus = true;
        for (int i = 0; i < _BallAreaOperations.Count; i++)
        {
            _BallAreaOperations[i].NumberText.text = BallNumberThrown + "/" + _BallAreaOperations[i].BallThrown;
        }
        TotalCheckPointNumber = _BallAreaOperations.Count -1;
    }

    void Update()
    {
        if (PickerMoveStatus)
        {
            PickerObject.transform.position += 3.4f * Time.deltaTime * PickerObject.transform.forward;

            if(Time.timeScale != 0)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            TouchPosX = TouchPosition.x - PickerObject.transform.position.x;
                            break;
                        case TouchPhase.Moved:
                            if (TouchPosition.x - TouchPosX > -1.15 && TouchPosition.x - TouchPosX < -1.15)
                            {
                                PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position, new Vector3(TouchPosition.x - TouchPosX,
                                    PickerObject.transform.position.y, PickerObject.transform.position.z), 3f);
                            }
                            break;
                    }
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position, new Vector3(PickerObject.transform.position.x - 0.1f,
                    PickerObject.transform.position.y, PickerObject.transform.position.z), 0.05f);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    PickerObject.transform.position = Vector3.Lerp(PickerObject.transform.position, new Vector3(PickerObject.transform.position.x + 0.1f,
                    PickerObject.transform.position.y, PickerObject.transform.position.z), 0.05f);
                }
            }
        }
    }

    public void OnLimit()
    {
        if (isPallets)
        {
            PickerPallets[0].SetActive(false);
            PickerPallets[1].SetActive(false);
        }

        PickerMoveStatus = false;
        Invoke("StageCtrl", 2f);
        Collider[] HitColl = Physics.OverlapBox(BallCtrlObject.transform.position, BallCtrlObject.transform.localScale / 2, Quaternion.identity);

        int i = 0;
        while(i < HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 0.5f), ForceMode.Impulse);
            i++;
        }
    }

    public void CountBalls()
    {
        BallNumberThrown++;
        _BallAreaOperations[AvailableCheckPointIndex].NumberText.text = BallNumberThrown + "/" + _BallAreaOperations[AvailableCheckPointIndex].BallThrown;
    }

    void StageCtrl()
    {
        if(BallNumberThrown >= _BallAreaOperations[AvailableCheckPointIndex].BallThrown)
        {
            _BallAreaOperations[AvailableCheckPointIndex].BallAreaElevator.Play("Elevator");
            foreach (var item in _BallAreaOperations[AvailableCheckPointIndex].Balls)
            {
                item.SetActive(false);
            }

            if(AvailableCheckPointIndex == TotalCheckPointNumber)
            {
                Debug.Log("Game Over");
                Time.timeScale = 0;
            }
            else
            {
                AvailableCheckPointIndex++;
                BallNumberThrown = 0;

                if (isPallets)
                {
                    PickerPallets[0].SetActive(true);
                    PickerPallets[1].SetActive(true);
                }
            }
            BallNumberThrown = 0;
        }
        else
        {
            Debug.Log("lose");
            TryAgain.SetActive(true);
        }
    }

    public void ShowPallets()
    {
        isPallets = true;
        PickerPallets[0].SetActive(true);
        PickerPallets[1].SetActive(true);
    }

    public void AddBonusBalls(int BonusBallIndex)
    {
        BonusBalls[BonusBallIndex].SetActive(true);
    }
}
