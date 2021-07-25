using UnityEngine;
using UnityEngine.UI;

public class setListComponent : MonoBehaviour
{
    [SerializeField] Text _textNum;

    public void setText(int num)
    {
        _textNum.text = "next : "+num;
    }
}
