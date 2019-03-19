using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCheckPlane : MonoBehaviour
{
    public Material materialTrue;
    public Material materialFalse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(bool _state) {
        if (_state == true)
            this.GetComponent<Renderer>().material = materialTrue;
        else
            this.GetComponent<Renderer>().material = materialFalse;
    }
}
