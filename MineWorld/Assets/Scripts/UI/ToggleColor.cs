using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleColor : MonoBehaviour {
    private Toggle m_toggle;

    public ColorBlock on_colors;
    public ColorBlock off_color;

    // Start is called before the first frame update
    void Start() {
        m_toggle = this.GetComponent<Toggle>();
        m_toggle.onValueChanged.AddListener(ValueChange);
        ValueChange(m_toggle.isOn);
    }

    // Update is called once per frame
    void Update() {

    }

    void ValueChange(bool _isOn) {
        if (_isOn == true) {
            m_toggle.colors = on_colors;
        }
        else {
            m_toggle.colors = off_color;
        }
    }
}
