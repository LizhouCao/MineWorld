using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    public Vector2Int size;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool CheckMapAvaliable(Vector2Int _position) {
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                if (MapDataController.CONTEXT.CheckMapType(_position.x + i, _position.y + j) != 0)
                    return false;
            }
        }
        return true;
    }

    public virtual bool CheckAndBuild(Vector2Int _position) {
        bool check = CheckMapAvaliable(_position);
        return check;
    }

    public virtual void Build(Vector2Int _position) {
        MapDataController.CONTEXT.BuildItem(_position, id, size);
        Item item = Instantiate(this);
        item.transform.position = new Vector3(_position.x + (size.x - 1) / 2.0f, 0.0f, _position.y + (size.y - 1) / 2.0f);
    }
}
