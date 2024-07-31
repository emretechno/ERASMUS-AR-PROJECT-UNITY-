using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{
    public Text timerText; // UI Text to display the timer
    private float startTime;
    private bool isRunning;

    private void Start()
    {
        startTime = Time.time;
        isRunning = true;
    }

    private void Update()
    {
        if (isRunning)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
