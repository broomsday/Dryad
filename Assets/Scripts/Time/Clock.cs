using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] private float gameTimeScale;
    [SerializeField] private TMP_Text clockText;

    private float second;
    private int minute;
    private int hour;
    private int day;
    private int year;
    private Vector3 time;
    private string timeOfDay;

    private Dictionary<int, string> TIME_OF_DAY = new Dictionary<int, string>();

    void Awake()
    {
        TIME_OF_DAY.Add(0, "Night");
        TIME_OF_DAY.Add(6, "Morning");
        TIME_OF_DAY.Add(12, "Noon");
        TIME_OF_DAY.Add(18, "Evening");
    }

    void Update()
    {
        TickTime();
        HandleTimeOfDay();
        UpdateDisplay();
    }

    private void TickTime() 
    {
        second += Time.deltaTime * gameTimeScale;

        if (second >= 60f) {
            minute += 1;
            second -= 60f;
        }
        if (minute >= 60) {
            hour += 1;
            minute -= 60;
        }
        if (hour >= 24) {
            day += 1;
            hour -= 24;
        }
        if (day >= 365) {
            year += 1;
            day -= 365;
        }

        time.x = second;
        time.y = minute;
        time.z = hour;
    }


    private void HandleTimeOfDay()
    {
        if (TIME_OF_DAY.ContainsKey(hour))
        {
            timeOfDay = TIME_OF_DAY[hour];
        }
    }

    private void UpdateDisplay()
    {
        clockText.text = $"{timeOfDay}, {hour:D2}:{minute:D2}, Day: {day}, Year: {year}";
    }

    public Vector3 GetTime()
    {
        return time;
    }

    public string GetTimeOfDay()
    {
        return timeOfDay;
    }
}
