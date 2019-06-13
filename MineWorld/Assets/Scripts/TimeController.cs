using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public int timeScale = 1;
    int day = 1;
    int hour = 9;
    float minute = 0;

    public Text timeText;
    public Light globalLight;

    int m_sunraise_start = 4;
    int m_sunraise_end = 7;
    int m_sunset_start = 18;
    int m_sunset_end = 21;

    float m_day_light = 0.9f;
    float m_night_light = 0.1f;

    float m_light_intensity = 0.5f;

    public float LightIntensity {
        get {
            return m_light_intensity;
        }
    }
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        minute += Time.deltaTime;
        if (minute > 60.0f) {
            minute -= 60.0f;
            hour++;
            if (hour >= 24) {
                hour -= 24;
                day++;
            }
        }

        int showHour = hour % 12;
        if (hour == 12)
            showHour = 12;
        int showMinute = (int)minute;

        string ampm = "am";
        if (hour >= 12)
            ampm = "pm";

        timeText.text = "" + showHour.ToString("00") + ":" + showMinute.ToString("00") + " " + ampm + "\r\n" + "Day " + day;

        if (hour > m_sunset_end || hour < m_sunraise_start)
            m_light_intensity = m_night_light;
        else if (hour > m_sunraise_end && hour < m_sunset_start)
            m_light_intensity = m_day_light;
        else {
            float a;
            if (hour < 12)
                a = (hour - m_sunraise_start + minute / 60.0f) / (m_sunraise_end - m_sunraise_start);
            else
                a = (m_sunset_end - hour - minute / 60.0f) / (m_sunset_end - m_sunset_start);
            m_light_intensity = Mathf.Lerp(m_night_light, m_day_light, a);
        }
        globalLight.intensity = m_light_intensity;
    }
}
