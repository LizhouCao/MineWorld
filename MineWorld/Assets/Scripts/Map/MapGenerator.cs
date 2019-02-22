using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject CubePrefab;
    public int mapSize = 200;
    
    class MapData {
        int type;
    };

    // Start is called before the first frame update
    void Start()
    {
        this.InitMap();
    }

    void InitMap() {
        for (int i = 0; i < mapSize; i++) {
            for (int j = 0; j < mapSize; j++) {
                GameObject obj = Instantiate(CubePrefab);
                obj.transform.SetParent(this.transform, false);
                obj.transform.localPosition = new Vector3(i, 0, j);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Load() {

    }

    void Save() {

    }
}
