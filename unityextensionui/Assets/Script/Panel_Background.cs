using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Panel_Background : MonoBehaviour
{
    [SerializeField] Color_Slot _slot = null;
    [SerializeField] Transform _slotContainer = null;

    Color_Slot _dragSlot = null;
    bool _isDrag = false;

    RectTransform _generatePos = null;

    int _slotCount = 4;

    private void Awake()
    {
        _generatePos = GetComponent<RectTransform>();

        Init();
    }

    void Init()
    {
        ActivateSlot(_slotCount);
    }

    void ActivateSlot(int slotCount)
    {
        for (int i = 0; i < slotCount; ++i)
        {
            Vector2 generatePos = GetRandomPos();
            _slot.transform.localPosition = generatePos;

            Instantiate(_slot, _slotContainer);
        }
    }

    Vector2 GetRandomPos()
    {
        float width = Screen.width;
        float height = Screen.height;

        float posX = Random.Range(0, width);
        float posY = Random.Range(0, height);

        Vector2 randomPos = new Vector2(posX, posY);

        return randomPos;
    }

}