using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSpeedButton : MonoBehaviour
{
    public int timeScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Toggle>().onValueChanged.AddListener(ValueChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ValueChange(bool _isOn) {
        Time.timeScale = timeScale;
    }
}
