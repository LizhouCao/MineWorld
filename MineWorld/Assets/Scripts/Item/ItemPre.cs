﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPre : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(bool _state) {
        this.transform.Find("true").gameObject.SetActive(_state);
        this.transform.Find("false").gameObject.SetActive(!_state);
    }

    public void SetRotation(int _num) {
        this.transform.localRotation = Quaternion.Euler(0.0f, 90.0f * _num, 0.0f);
    }
}
