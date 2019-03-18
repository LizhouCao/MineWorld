using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuilder : MonoBehaviour
{
    enum ItemBuilder_State {
        IDLE = 0,
        BUILDING = 1
    }

    ItemBuilder_State m_state = ItemBuilder_State.IDLE;
    public GameObject checkPlane_prefab;

    GameObject[,] m_checkPlane_array;
    Vector2Int m_size;

    // Start is called before the first frame update
    void Start()
    {
        StartBuilding();
    }

    // Update is called once per frame
    void Update() {
        if (m_state == ItemBuilder_State.BUILDING) {
            if (Input.GetMouseButtonDown(1)) {
                ExitBuilding();
            }
            else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, 1 << 10)) {
                    CheckPlaneUpdate(HitToMap(hit.point));
                }
            }
        }
    }

    



    void StartBuilding() {
        m_state = ItemBuilder_State.BUILDING;

        m_size = new Vector2Int(3, 3);

        m_checkPlane_array = new GameObject[m_size.x, m_size.y];
        for (int i = 0; i < m_size.x; i++) {
            for (int j = 0; j < m_size.y; j++) {
                m_checkPlane_array[i, j] = Instantiate(checkPlane_prefab);
            }
        }
    }

    void ExitBuilding() {

    }

    Vector2Int HitToMap(Vector3 _point) {
        float t = 0.4f;
        int x = (int) Mathf.Round(_point.x - (m_size.x - 1) / 2.0f);
        int y = (int) Mathf.Round(_point.z - (m_size.y - 1) / 2.0f);
        return new Vector2Int(x, y);
    }

    void CheckPlaneUpdate(Vector2Int _point) {

        for (int i = 0; i < m_size.x; i++) {
            for (int j = 0; j < m_size.y; j++) {
                m_checkPlane_array[i, j].transform.position = new Vector3(_point.x + i, 0.0f, _point.y + j);
            }
        }
    }
}
