using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    private bool started = false;
    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(PlayerPrefs.GetInt("w0"), -4, PlayerPrefs.GetInt("h0"));
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
