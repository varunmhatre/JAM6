using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    private bool started = false;
    public GameObject Map_Goal, Map_LM1, Map_LM2;
    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(PlayerPrefs.GetInt("w0"), -4, PlayerPrefs.GetInt("h0"));
        Map_Goal.transform.position = new Vector3(PlayerPrefs.GetInt("w1"), -5, PlayerPrefs.GetInt("h1"));
        Map_LM1.transform.position = new Vector3(PlayerPrefs.GetInt("w2"), -4, PlayerPrefs.GetInt("h2"));
        Map_LM2.transform.position = new Vector3(PlayerPrefs.GetInt("w3"), -4, PlayerPrefs.GetInt("h3"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            GameObject.Find("WaitText").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("TimerText").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("TimerText").GetComponent<TimerScript>().RestartTimer();
        }
    }
}
