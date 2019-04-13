using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController CONTEXT;
    public CityController City {
        get {
            return this.city;
        }
    }
    public CityController city;

    private void Awake() {
        CONTEXT = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
