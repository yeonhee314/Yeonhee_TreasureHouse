using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncAwaitTest : MonoBehaviour
{
    public float _TargetTime = 0f;
    Coroutine _coroutine = null;
    float _deltaTime = 0f;
    bool _chkUpdate = false;
    
    /// <summary>
    /// Async 로직
    /// </summary>
    public void OnAsyncBtn()
    {
        FirstTask();
    }

    async void FirstTask() 
    {
        Debug.Log("go first"); 
        await Task.Delay(3000); 
        Debug.Log("go after"); 
        await SecondTask();
        Debug.Log("all done");
    }

    async Task SecondTask()
    {
        Debug.Log("second job start");
        await Task.Delay(2000);
        Debug.Log("job's done");
    }

    /// <summary>
    /// Update 로직
    /// </summary>

    public void OnUpdateBtn()
    {
        _deltaTime = _TargetTime;
        _chkUpdate = true;
        Debug.Log("start of update");
    }

    private void Update()
    {
        if(_chkUpdate)
        {
            if (_deltaTime > 0)
            {
                _deltaTime -= Time.deltaTime;
            }
            if (_deltaTime <= 0)
            {
                _chkUpdate = false;
                Debug.Log("end of update");
            }
        }
    }

    /// <summary>
    /// Coroutine 로직
    /// </summary>

    public void OnCoroutineBtn()
    {
        //if(_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(CoDelayTest(2));
    }

    IEnumerator CoDelayTest(float delay)
    {
        Debug.Log("start coroutine");
        yield return new WaitForSeconds(delay);
        Debug.Log("end of coroutine");
    }
}
