using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delegate2 : MonoBehaviour
{
    public deleteGateTest degate = null;

    // Start is called before the first frame update
    void Start()
    {
        degate.anLove += sadHeart;

        degate.setDelegate();

        degate.anLove(3000);
    }

    void sadHeart(int value)
    {
        Debug.Log("i am nobody without you : "+value);
    }
}
