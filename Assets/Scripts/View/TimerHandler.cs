using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerHandler : MonoBehaviour
{
    private float totalTimeInMinutes; // Total time in minutes
    private float totalTimeInSeconds;
    [SerializeField] Text countdownText;
    private Transform playerCamera;
    [SerializeField] VFXTrigger OutcomeVfx;
    
    [SerializeField] GameObject LoadingVfx;

    [SerializeField] public GameObject TimerPanel;

    [SerializeField] GameObject tool;
    private ToolInteractionDetector Tool;

    private float currentTickInterval = 1.0f; // Current interval between countdown ticks
    private float initialTickInterval = 1.0f; // Initial tick interval before any speed adjustments
    private Coroutine countdownCoroutine; // Reference to the countdown coroutine

    void Awake()
    {
        playerCamera = Camera.main.transform;
    }

    public void SetTimerAndVFX(float timer)
    {
        SetTimerTime(timer);
       
        TimerPanel.SetActive(true);
        LoadingVfx.SetActive(true);
        Countdown();
    }

    public void Countdown()
    {
        totalTimeInSeconds = totalTimeInMinutes * 60.0f;
        Tool = tool.GetComponent<ToolInteractionDetector>();
        countdownCoroutine = StartCoroutine(StartCountdown());
    }
    
    public void SpeedUpCountdown()
    {
        // Reduce the interval between countdown ticks to speed up the countdown
        currentTickInterval *= 0.1f; // Adjust as needed for desired speed
    }
    
    public void SetTimerTime(float timer)
    {
        totalTimeInMinutes = timer;
    }

    void Update()
    {
        if (TimerPanel.activeSelf)
        {
            TimerPanel.transform.rotation = playerCamera.rotation;
        }
    }

    IEnumerator StartCountdown()
    {
        float timeRemaining = totalTimeInSeconds;

        while (timeRemaining > 0)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            countdownText.text = string.Format("{0}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(currentTickInterval);

            timeRemaining -= initialTickInterval; // Decrease by initial interval to maintain original countdown time
        }

        // Handle countdown finished event here
        Debug.Log("Countdown Finished!");
        TimerPanel.SetActive(false);
        OutcomeVfx.gameObject.SetActive(true);
        
        LoadingVfx.SetActive(false);

        Tool.CountdownCompleted?.Invoke();
    }
}
