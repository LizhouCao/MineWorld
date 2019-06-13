using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingToggle : MonoBehaviour
{
    Toggle m_toggle;
    float m_rotation = 0.0f;
    float m_rotateSpeedMin = 15.0f;
    float m_rotatespeedMax = 480.0f;
    float m_rotateSpeedAcc = 360.0f;

    float m_rotateSpeed = 1.0f;

    bool m_isOn = false;
    RectTransform m_transform;

    // Start is called before the first frame update
    void Start()
    {
        m_toggle = this.GetComponent<Toggle>();
        m_transform = this.transform.Find("Background").GetComponent<RectTransform>();
        m_toggle.onValueChanged.AddListener(ValueChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isOn) {
            m_rotation += Time.deltaTime * m_rotateSpeed;
            m_rotateSpeed -= Time.deltaTime * m_rotateSpeedAcc;
            if (m_rotateSpeed < m_rotateSpeedMin)
                m_rotateSpeed = m_rotateSpeedMin;
            m_transform.localEulerAngles = new Vector3(0.0f, 0.0f, m_rotation);
        }   
    }

    void ValueChange(bool _isOn) {
        if (_isOn == true) {
            m_isOn = true;
            m_rotateSpeed = m_rotatespeedMax;
        }
        else {
            m_isOn = false;
            m_transform.localEulerAngles = Vector3.zero;
        }
        SceneController.CONTEXT.ItemGeneratorCtrl.SetMenuActive(_isOn);
    }
}
