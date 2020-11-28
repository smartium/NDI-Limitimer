using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using OscJack;

public class Timer : MonoBehaviour
{
    public Text txtCounter;
    public int time;
    
    public GameObject inputTime;
    public Button btnConfirm;
    public Button btnStart;

    private int counter;
    private bool isPlaying = false;
    private Coroutine counterRoutine;
    
    // Start is called before the first frame update
    void Start()
    {
       counter = time;

       StartCoroutine(OscListener());
        
        //inputTime.GetComponentInChildren<Text>().text = "test";
        time *= 60;
        
        btnStart.onClick.AddListener(delegate
        {
            if (isPlaying)
            {
                stopTimer();
            }
            else
            {
                if (counter < 1)
                {
                    stopTimer("reset");
                }
                else
                {
                    startTimer(time);    
                }
            }

            isPlaying = !isPlaying;
        });
        
        btnConfirm.onClick.AddListener(delegate
        {
            time = int.Parse(inputTime.GetComponentInChildren<Text>().text)*60;
            //txtCounter.text = "";
            counter = time;
            setCounterDisplay();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            setCounterDisplay();
        }

        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            if (isPlaying)
            {
                stopTimer();
            }
            else
            {
                if (counter < 1)
                {
                    stopTimer("reset");
                }
                else
                {
                    startTimer(time);
                }
            }

            isPlaying = !isPlaying;
        }
    }
    
    IEnumerator Countdown (int seconds) {
        counter = seconds;
        while (counter > -1)
        {
            yield return new WaitForSeconds (1);
            //print(counter);
            if (counter <= Math.Round(time * 0.2f) + 20 && counter > Math.Round(time * 0.1f))
            {
               txtCounter.color = Color.yellow;
            }
            
            else if (counter <= Math.Round(time*0.1f)+1)
            {
                txtCounter.color = Color.red;
            }

            counter -= 1;
        }
        isPlaying = !isPlaying;
        btnStart.GetComponentInChildren<Text>().text = "READY";
        btnStart.GetComponentInChildren<Text>().color = Color.black;
        btnStart.image.color = Color.yellow;
        txtCounter.text = "OVER";

    }
    
    IEnumerator OscListener()
    {
        var server = new OscServer(8000); // Port number

        server.MessageDispatcher.AddCallback(
            "/addminute", // OSC address
            (string address, OscDataHandle data) =>
            {
                AddMinute();
            }
        );
        
        server.MessageDispatcher.AddCallback(
            "/subminute", // OSC address
            (string address, OscDataHandle data) => {
               RemoveMinute();
            }
        );
        yield break;
    }

    void AddMinute()
    {
        counter++;
    }
    
    void RemoveMinute()
    {
        counter--;
    }

    void startTimer(int _time)
    {
        txtCounter.color = Color.green;
        counterRoutine = StartCoroutine(Countdown(_time));
        btnStart.GetComponentInChildren<Text>().text = "STOP";
        btnStart.GetComponentInChildren<Text>().color = Color.white;
        inputTime.SetActive(false);
        btnConfirm.gameObject.SetActive(false);
        btnStart.image.color = Color.red;
    }

    void stopTimer(string _reset = "no")
    {
        if (_reset != "reset")
        {
            StopCoroutine(counterRoutine);
            btnStart.image.color = Color.green;
        }
        btnStart.GetComponentInChildren<Text>().text = "START";
        btnStart.GetComponentInChildren<Text>().color = Color.black;
        
        counter = time;
        inputTime.SetActive(true);
        btnConfirm.gameObject.SetActive(true);
    }

    void setCounterDisplay()
    {
        int hours = counter / 60 / 60;
        int minutes = counter / 60 % 60;
        int seconds = counter % 60;
        string strMinutes = null;
        string strSeconds = null;
 
        if(minutes < 10) {
            strMinutes = "0" + minutes;
        }
        else
        {
            strMinutes = minutes.ToString();
        }
        if(seconds < 10) {
            strSeconds = "0" + seconds;
        }
        else
        {
            strSeconds = seconds.ToString();
        }
        txtCounter.text = hours + ":" + strMinutes + ":" + strSeconds;
    }
}
