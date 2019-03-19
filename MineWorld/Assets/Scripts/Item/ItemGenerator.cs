using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    enum ItemBuilder_State {
        IDLE = 0,
        BUILDING = 1,
        BUILDING_HOLD = 2
    }

    ItemBuilder_State m_state = ItemBuilder_State.IDLE;

    public Item item_prefab;
    public ItemPre itemPre_prefab;

    private ItemPre m_itemPre;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        if (m_state >= ItemBuilder_State.BUILDING) {
            if (Input.GetMouseButtonDown(1)) {
                ExitBuilding();
            }
            else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, 1 << 10)) {

                    Vector2Int itemPosition = HitToMap(hit.point);                   
                    bool avaliable = CheckPlaneUpdate(itemPosition);

                    if (avaliable) {
                        if (Input.GetMouseButtonDown(0)) {
                            // Build(itemPosition);
                        }                                 
                    }
                }
            }
        }
    }

    
    void Generate(Vector2Int _itemPosition) {
        Item item = Instantiate(item_prefab);
        item.transform.position = new Vector3(_itemPosition.x + (item_prefab.size.x - 1) / 2.0f, 0.0f, _itemPosition.y + (item_prefab.size.y - 1) / 2.0f);
    }


    public void StartGenerating() {
        if (m_state == ItemBuilder_State.BUILDING) {
            ExitBuilding();
        }
        m_state = ItemBuilder_State.BUILDING;
    }

    void ExitBuilding() {
        Destroy(m_itemPre.gameObject);

        //foreach (MapCheckPlane plane in m_checkPlane_array)
          //  Destroy(plane.gameObject);

        m_state = ItemBuilder_State.IDLE;
    }

    Vector2Int HitToMap(Vector3 _point) {
        int x = (int) Mathf.Round(_point.x - (item_prefab.size.x - 1) / 2.0f);
        int y = (int) Mathf.Round(_point.z - (item_prefab.size.y - 1) / 2.0f);
        return new Vector2Int(x, y);
    }

    bool CheckPlaneUpdate(Vector2Int _itemPosition) {
        bool avaliableFlag = item_prefab.CheckMapAvaliable(_itemPosition);

        m_itemPre.SetState(avaliableFlag);

        m_itemPre.transform.position = new Vector3(_itemPosition.x + (item_prefab.size.x - 1) / 2.0f, 0.0f, _itemPosition.y + (item_prefab.size.y - 1) / 2.0f);

        return avaliableFlag;
    }
}
