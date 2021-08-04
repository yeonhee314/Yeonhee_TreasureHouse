using System;
using UnityEngine;

public class deleteGateTest : MonoBehaviour
{
    public Action<int> anLove;

    public delegate void myDream(int value);
    public myDream fallenDream;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void setDelegate()
    {
        anLove += addMyHeart;
        anLove += addYourHeart;

        fallenDream += addMyHeart;
        fallenDream += addYourHeart;

        anLove(5);

        fallenDream(10);
    }

    public void addMyHeart(int value)
    {
        Debug.Log("my heart : "+value);
    }

    public void addYourHeart(int value)
    {
        Debug.Log("your heart : "+value);
    }
}
