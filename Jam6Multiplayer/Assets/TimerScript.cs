using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    DateTime start;
    public bool timerRunning = false;
    float timer;

    public float TotalTime
    {
        get { return timer; }
    }

    // Use this for initialization
    void Start()
    {
        timerRunning = true;
        timer = 0;
        start = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            timer += Time.deltaTime;
            TimeSpan timeSinceStart = DateTime.Now - start;
            gameObject.GetComponent<TextMesh>().text = "Time: " + timeSinceStart.Minutes + ":" + timeSinceStart.Seconds + "." + timeSinceStart.Milliseconds;
        }
    }

    public void RestartTimer()
    {
        timerRunning = true;
        timer = 0;
    }
}
