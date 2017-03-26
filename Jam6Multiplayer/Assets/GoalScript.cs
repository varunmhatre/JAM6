using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetFloat("LevelTime", GameObject.Find("TimerText").GetComponent<TimerScript>().TotalTime);
        GameObject.Find("TimerText").GetComponent<TimerScript>().timerRunning = false;
        SceneManager.LoadScene("GameOverScene");
    }
}
