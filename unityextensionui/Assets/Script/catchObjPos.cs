using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catchObjPos : MonoBehaviour
{
    [SerializeField] GameObject _obj = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtn()
    {
        if (_obj != null) Debug.Log("world pos : "+_obj.transform.position+", local pos : "+_obj.transform.localPosition);
    }

    void blabla()
    {
        
    }
}
