using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform m_followTarget;

    Queue<Vector3> m_delayPositionQueue = new Queue<Vector3>();
    Queue<Vector3> m_delayRotationQueue = new Queue<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        m_followTarget = SceneController.CONTEXT.city.FollowTarget;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_delayPositionQueue.Enqueue(m_followTarget.position);
        m_delayRotationQueue.Enqueue(m_followTarget.rotation.eulerAngles);

        if (m_delayPositionQueue.Count > 30) {
            Vector3 pos = m_delayPositionQueue.Dequeue();
            Vector3 rot = m_delayRotationQueue.Dequeue();

            this.transform.position = pos;
            this.transform.rotation = Quaternion.Euler(0.0f, rot.y, 0.0f);
        }
    }
}
