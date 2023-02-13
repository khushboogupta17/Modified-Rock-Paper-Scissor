using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Image TimerFG;
    public int TimeLimit = 1;
    public TextMeshProUGUI TimerLabel;
    
    public delegate void OnTimeIsUp();

    public static event OnTimeIsUp OnTimeIsUpEvent ;

    private Coroutine _decreaseTimerCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        TimerLabel.text = $"Timer: {TimeLimit}s";
       Invoke(nameof(StartTimer),0.2f);
    }

    IEnumerator DecreaseTimer()
    {
        while (TimerFG.fillAmount>0)
        {
            TimerFG.fillAmount -= Time.deltaTime / (float)TimeLimit;
            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }
        
        OnTimeIsUpEvent?.Invoke();
        Debug.Log("Time's up!");
    }

    public void StartTimer()
    {
        TimerFG.fillAmount = 1f;
        _decreaseTimerCoroutine = StartCoroutine(DecreaseTimer());
    }

    public void StopTimer()
    {
        if (_decreaseTimerCoroutine != null)
        {
            StopCoroutine(_decreaseTimerCoroutine);
        }
        
    }
}
