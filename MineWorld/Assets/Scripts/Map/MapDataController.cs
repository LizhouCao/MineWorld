using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataController : MonoBehaviour
{
    public static MapDataController CONTEXT;
    int m_mapSize = 500;

    /*
     * 0 ground
     * 1 building
     * 2 road
     */
    int[,] m_mapTypeArray;

    MapData[,] m_mapDataArray;
    public MapData[,] MapDataArray {
        get { return m_mapDataArray; }
        set { m_mapDataArray = value; }
    }

    private void Awake() {
        CONTEXT = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetMap(500);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetMap(int _size) {
        m_mapTypeArray = new int[_size, _size];
    }

    public int CheckMapType(Vector2Int _point) {    
        return CheckMapType(_point.x, _point.y);
    }

    public int CheckMapType(int _x, int _y) {
        return m_mapTypeArray[_x, _y];
    }

    public bool BuildItem(Vector2Int _point, int _id, Vector2Int _size) {
        for (int i = 0; i < _size.x; i++) {
            for (int j = 0; j < _size.y; j++) {
                m_mapTypeArray[_point.x + i, _point.y + j] = 1;
            }
        }
        return true;
    }

    public bool DeleteItem(Vector3Int _position) {
        return true;
    }
}
