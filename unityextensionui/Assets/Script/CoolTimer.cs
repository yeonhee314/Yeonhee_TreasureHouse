using UnityEngine.UI;
using UnityEngine;

public class CoolTimer : MonoBehaviour
{
    public Text text_CoolTime;
    public Image image_fill;
    public float time_coolTime = 2;
    private float time_current;
    private bool isEnded = true;


    private void Update()
    {
        if (isEnded)
            return;
        Check_CoolTime();
    }

    void Check_CoolTime()
    {
        time_current += Time.deltaTime;
        if(time_current < time_coolTime)
        {
            Set_FillAmount(time_current);
        }
        else if(!isEnded)
        {
            End_CoolTime();
        }
    }

    void End_CoolTime()
    {
        Set_FillAmount(time_coolTime);
        isEnded = true;
        text_CoolTime.gameObject.SetActive(false);
    }

    void Trigger_Skill()
    {
        if (!isEnded) return;

        Reset_CoolTime();
    }

    void Reset_CoolTime()
    {
        text_CoolTime.gameObject.SetActive(true);
        time_current = 0;
        Set_FillAmount(0);
        isEnded = false;
    }

    void Set_FillAmount(float value)
    {
        image_fill.fillAmount = value / time_coolTime;
        text_CoolTime.text = string.Format("Rest : {0}",value.ToString("0.0"));
    }

    public void on_Btn()
    {
        Trigger_Skill();
    }
}
