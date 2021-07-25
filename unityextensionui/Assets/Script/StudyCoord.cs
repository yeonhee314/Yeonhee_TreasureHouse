using UnityEngine;
using System;

public class StudyCoord : MonoBehaviour
{
    [SerializeField] GameObject _gbjCube = null;

    Action _onBtn = null;

    // 버튼을 누르면 Cube의 로컬 좌표와 월드 좌표를 출력
    void Start()
    {
        CoordinateCubeLog(OnBtn);
    }

    void CoordinateCubeLog(Action onBtn)
    {
        //
        _onBtn = onBtn;
/*
        //
        Debug.Log("Local Postion  : " + _gbjCube.transform.localPosition);
        Debug.Log("World Position : " + _gbjCube.transform.position);
*/
    }

    public void OnBtn()
    {
        _onBtn?.Invoke();
    }
}