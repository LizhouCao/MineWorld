using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float m_scaleMin = 1.0f;
    float m_scaleMax = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (Input.GetMouseButton(1)) {
            float distanceX = -Input.GetAxis("Mouse X");
            float disntaceY = -Input.GetAxis("Mouse Y");

            this.transform.Rotate(new Vector3(0.0f, 3.0f * distanceX, 0.0f));
        }

        if (Input.GetMouseButton(2)) {
            float distanceX = -Input.GetAxis("Mouse X");
            float distanceY = -Input.GetAxis("Mouse Y");

            this.transform.Translate(new Vector3(distanceX, 0.0f, distanceY));
        }

        this.transform.localScale *= 1.0f - 0.1f * Input.mouseScrollDelta.y;
        if (this.transform.localScale.x < m_scaleMin)
            this.transform.localScale = m_scaleMin * Vector3.one;
        else if (this.transform.localScale.x > m_scaleMax)
            this.transform.localScale = m_scaleMax * Vector3.one;

    }
}
