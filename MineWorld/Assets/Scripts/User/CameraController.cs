using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (Input.GetMouseButton(2)) {
            float distanceX = -Input.GetAxis("Mouse X");
            float distanceY = -Input.GetAxis("Mouse Y");

            this.transform.position += new Vector3(distanceX, 0.0f, distanceY);
        }

        this.transform.Translate(0.0f, 0.0f, Input.mouseScrollDelta.y);
    }
}
