using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitrogenAcceleration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.F)) {
            this.GetComponent<Rigidbody>().AddForce(this.transform.forward * 5000.0f);
        }
    }
}
