using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Slot : MonoBehaviour
{
    [SerializeField] GameObject _gbjSlot = null;
    [SerializeField] GameObject _gbjClick = null;

    public void Init()
    {
        _gbjSlot.SetActive(false);
        _gbjClick.SetActive(false);
    }

    public void SetClick(bool isClick)
    {
        if (isClick == true)
        {
            _gbjClick.SetActive(true);
        }
        else
        {
            _gbjClick.SetActive(false);
        }
    }
}