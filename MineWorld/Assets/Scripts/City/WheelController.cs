using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public bool is_rotate;
    public bool is_driving;

    public float m_force = 18500.0f;
    public float m_brake = 1000000.0f;

    Vector3 pos;

    Vector3 m_rotation = Vector3.zero;
    float m_max_y_rotation = 30.0f;
    float m_x_rotate_speed = 10.0f;
    float m_y_rotate_speed = 2.0f;
    float m_y_return_speed = 1.0f;

    Transform m_model;
    WheelCollider m_wheel;

    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.position;
        m_model = this.transform.Find("model");
        m_wheel = this.transform.GetComponentInChildren<WheelCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vector = Vector3.Project(this.transform.position - pos, this.transform.forward);
        float distance = vector.magnitude;

        if (Vector3.Dot(vector, this.transform.forward) < 0.0f)
            distance = -distance;

        m_rotation.x += distance * m_x_rotate_speed;

        if (is_rotate) {
            float horizontal = Input.GetAxis("Horizontal");
            m_rotation.y += m_y_rotate_speed * horizontal;

            if (m_rotation.y > 0.0f) {
                m_rotation.y -= m_y_return_speed;
                if (m_rotation.y < m_y_return_speed)
                    m_rotation.y = 0.0f;
            }
            else if (m_rotation.y < 0.0f) {
                m_rotation.y += m_y_return_speed;
                if (m_rotation.y > -m_y_return_speed)
                    m_rotation.y = 0.0f;
            }

                m_rotation.y = Mathf.Min(m_rotation.y, m_max_y_rotation);
            m_rotation.y = Mathf.Max(m_rotation.y, -m_max_y_rotation);

            
        }

        this.transform.Find("model").localRotation = Quaternion.Euler(m_rotation);

        pos = this.transform.position;


        m_wheel.steerAngle = m_rotation.y;

        if (is_driving) {
            float vertical = Input.GetAxis("Vertical");
            m_wheel.motorTorque = m_force * vertical;

            if (vertical != 0.0f) {
                m_wheel.brakeTorque = 0.0f;
            }
            else {
                m_wheel.brakeTorque = m_brake;
            }
        }

        if (Input.GetKey(KeyCode.Space)) {
            m_wheel.brakeTorque = 2.0f * m_brake;
        }

        if (particle != null) {
            Vector3 velocity = SceneController.CONTEXT.city.GetComponent<Rigidbody>().velocity;
            // velocity = this.transform.InverseTransformPoint(velocity);

            // print(velocity);
            float z = Vector3.Dot(velocity, this.transform.forward) / this.transform.forward.magnitude;

            if (z != 0.0f) {
                float sign = z / Mathf.Abs(z);

                particle.transform.localRotation = Quaternion.Euler(-90.0f - sign * 30.0f, 0.0f, 0.0f);
                particle.Emit((int)(Mathf.Abs(z) * 0.25f));
            }
        }
    }
}
