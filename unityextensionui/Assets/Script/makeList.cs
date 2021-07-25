using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class makeList : MonoBehaviour
{
    [SerializeField] RectTransform _rtParent = null;
    [SerializeField] GameObject _gbjSlot = null;
    [SerializeField] ScrollRect _scroll = null;


    public int _slotCount = 9;
    public int _makeCount = 10;
    
    int _index = 0;
    RectTransform _slotRect = null;
    GameObject[] _slots = null;


    private void Awake()
    {
        _scroll.onValueChanged.AddListener(OnScrollChange);
    }

    private void Start()
    {
        _slotRect = _gbjSlot.GetComponent<RectTransform>();

        _rtParent.sizeDelta = new Vector2(_rtParent.sizeDelta.x, _slotRect.sizeDelta.y * _slotCount);
        _slots = new GameObject[_slotCount];

        for(var i = 0; i < _makeCount; ++i)
        {
            GameObject gbj = Instantiate(_gbjSlot, _rtParent);
            /*gbj.name = string.Format("item {0}",i);
            gbj.transform.localPosition = new Vector2( gbj.transform.localPosition.x,-_slotRect.sizeDelta.y * i);*/
            Refresh(gbj,i);
            _slots[i] = gbj;
        }
    }

    void OnScrollChange(Vector2 vec)
    {
        //index 가져오기
        //위 아래 옮겨주기
        int index = getCurrentIndex();
        GameObject objIndex = _slots[index];
        if(objIndex == null)
        {
            int next = index + _makeCount;
            Debug.Log("index : " + index+", next : "+next);
            if (next > _slotCount - 1)
                return;
            else
            {
                GameObject objNow = _slots[next];
                if(objNow != null)
                {
                    _slots[next] = objIndex;
                    _slots[index] = objNow;
                    Refresh(_slots[index], index);
                }
            }
        }
        else
        {
            if(index > 0)
            {
                GameObject obj = _slots[index - 1];
                if (obj == null)
                    return;
                int next = index - 1 + _makeCount;
                if (next > _slotCount - 1)
                    return;
                else
                {
                    GameObject objNow = _slots[next];
                    if(objNow == null)
                    {
                        _slots[next] = obj;
                        _slots[index - 1] = objNow;
                        Refresh(_slots[next], next);
                    }
                }
            }
        }
    }

    void Refresh(GameObject obj, int indexRefresh)
    {
        obj.transform.name = string.Format("item {0}",indexRefresh);
        Vector3 vec = getLocationAppear(obj.transform.localPosition, indexRefresh);
        obj.transform.localPosition = vec;
    }

    Vector3 getLocationAppear(Vector2 initVec, int locaiton)
    {
        Vector3 vec = initVec;
        vec = new Vector3(vec.x,-( _slotRect.sizeDelta.y * locaiton), 0);
        return vec;
    }

    int getCurrentIndex()
    {
        int index = (int)(_rtParent.anchoredPosition.y/ _slotRect.sizeDelta.y);
        if (index < 0)
            index = 0;
        if (index > _slotCount - 1)
            index = _slotCount - 1;
        return index;
    }


    //빠른 이동으로 인한 버그 픽스가 필요함
}
