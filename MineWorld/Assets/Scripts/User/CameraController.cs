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
        if (this.transform.localScale.x < 1.0f)
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else if (this.transform.localScale.x > 10.0f)
            this.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);

    }
}
