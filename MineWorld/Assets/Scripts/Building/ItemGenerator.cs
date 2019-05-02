using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual Vector3Int HitPointToMap(Vector3 _point) {
        Vector3Int point = Vector3Int.zero;
        point.x = Mathf.RoundToInt(_point.x + 0.5f);
        point.y = Mathf.RoundToInt(_point.y);
        point.z = Mathf.RoundToInt(_point.z + 0.5f);
        return point;
    } 
}
