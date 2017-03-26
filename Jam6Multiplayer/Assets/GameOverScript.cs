using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        try
        {
            TimeSpan time = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("LevelTime"));
            GameObject.Find("TotalTimeScore").GetComponent<TextMesh>().text = "Total Time: " + time.Minutes + ":" + time.Seconds + "." + time.Milliseconds;
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
