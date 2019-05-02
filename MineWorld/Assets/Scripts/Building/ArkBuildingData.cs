using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkBuildingData : MonoBehaviour
{
    public Vector3Int size = new Vector3Int(20, 2, 30);

    public Vector3Int offset {
        get {
            return new Vector3Int(size.x / 2, 0, size.z / 2);
        }
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
