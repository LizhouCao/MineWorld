using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityDriver : MonoBehaviour
{
    float m_force = 100000.0f;
    float m_rotate_speed = 1000.0f;

    public Rigidbody[] wheels;

    public Transform[] rotateWheels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z_offset = m_force * Time.deltaTime * Input.GetAxis("Vertical");
        float x_offset = m_rotate_speed * Time.deltaTime * Input.GetAxis("Horizontal");

        print(x_offset);

        foreach (Transform rotateWheel in rotateWheels) {
            rotateWheel.Rotate(0.0f, x_offset, 0.0f);
        }

        foreach (Rigidbody wheel in wheels) {
            wheel.AddForce(new Vector3(0.0f, 0.0f, z_offset));
        }

        
        // this.transform.Translate(new Vector3(0.0f, 0.0f, z_offset));
    }
}
