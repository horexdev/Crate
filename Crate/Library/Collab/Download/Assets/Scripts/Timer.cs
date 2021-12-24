using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timeText;
    [SerializeField] private float timeToDisplay;
    [SerializeField] private int speed = 1;

    public static bool stopTimer = false;
    public static bool isComplete = false;

    private void Start() {
        timeText = GetComponentInChildren<Text>();

        isComplete = false;
        stopTimer = false;
    }
    
    private void Update() {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if (!stopTimer) {
            if (timeToDisplay > 0 && !isComplete) {
                timeToDisplay -= (1 * Time.deltaTime) * speed;

                timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            } else {
                minutes = 0;
                seconds = 0;

                isComplete = true;
            }
        }
    }
}