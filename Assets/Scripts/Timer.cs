using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI timerText;
    [SerializeField] TMPro.TextMeshProUGUI destroyText;
    [SerializeField] GameObject titlePage;
    float timer, tk = 0;
    bool gameFinished = false, gameStarted = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted) {
            if (Input.anyKey) {
                FindObjectOfType<PlayerController>().receiveInput = true;
                gameStarted = true;
                destroyText.SetText("Destroy All Food Stands!!");
                titlePage.SetActive(false);
            }

            return;
        }


        if (!gameFinished) {
            timer = Time.time;
            timerText.SetText(timer.ToString("F1"));

            if (Time.time > tk) {
                tk = Time.time + 0.5f;
                StallBehaviour[] stalls = FindObjectsOfType<StallBehaviour>();

                if (stalls.Length == 0) {
                    FindObjectOfType<PlayerController>().receiveInput = false;
                    gameFinished = true;
                    destroyText.SetText("");
                    timerText.SetText("Last food stand destroyed in " + timer.ToString("F1") + " seconds!");
                    tk = Time.time + 2.0f;
                }
            }
        }
        else {
            if (Time.time > tk) {
                if (Input.anyKey) {
                    Application.Quit();
                }
            }
        }
    }
}
