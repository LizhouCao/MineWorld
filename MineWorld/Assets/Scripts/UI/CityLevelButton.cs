using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class CityLevelButton : MonoBehaviour
{
    public int id;
    private Toggle m_toggle;
    // Start is called before the first frame update
    void Start()
    {
        m_toggle = this.GetComponent<Toggle>();
        m_toggle.onValueChanged.AddListener(ValueChange);
        ValueChange(m_toggle.isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ValueChange(bool _isOn) {
        if (_isOn == true) {
            this.transform.parent.GetComponent<CityLevelUI>().SelectLevel(id);
        }
    }
}
