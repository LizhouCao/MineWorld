using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataController : MonoBehaviour
{
    public static MapDataController CONTEXT;
    int m_mapSize = 200;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetMap(int _size) {
        m_mapDataArray = new MapData[_size, _size];
        GameObject obj = this.transform.Find("MapItems").gameObject;
        obj.name = "destroy";
        Destroy(obj);
        GameObject emptyObj = new GameObject("MapItems");
        emptyObj.transform.SetParent(this.transform, false);
    }



    public bool CheckAddItem(Item _item, Vector3Int _position) {
        int x = _position.x;
        int y = _position.y;
        int z = _position.z;

        int high = -1;
        for (MapData data = m_mapDataArray[x, z]; data != null; data = data.up)
            high++;
        if (y < high)
            return false;

        return true;
    }

    public void AddItem(int _id, Vector3Int _position) {
        int x = _position.x;
        int y = _position.y;
        int z = _position.z;

        MapData data = new MapData();
        data.id = _id;

        if (m_mapDataArray[x, z] == null)
            m_mapDataArray[x, z] = data;
        else {
            for (MapData d = m_mapDataArray[x, z]; ; d = d.up) {
                if (d.up == null) {
                    d.up = data;
                    data.down = d;
                    break;
                }
            }
        }
    }

    public bool DeleteItem(Vector3Int _position) {
        MapData data = m_mapDataArray[_position.x, _position.z];
        for (int i = 0; ; i++) {
            if (data == null) {
                return false;
            }
            if (i == _position.y) {
                if (data.up == null) {
                    if (data.down != null) {
                        data.down.up = null;
                    }
                    else
                        m_mapDataArray[_position.x, _position.z] = null;
                    
                    return true;
                }
                else {
                    return false;
                }
            }
            data = data.up;
        } 
    }
}
