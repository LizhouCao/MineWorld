using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemGenerator : MonoBehaviour
{
    protected bool m_isWorking = false;

    public Item item_prefab;
    public ItemPre itemPre_prefab;

    private ItemPre m_itemPre;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        if (m_isWorking == true) {
            if (Input.GetMouseButtonDown(1)) {
                ExitBuilding();
            }
            else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, 1000.0f, 1 << 11)) {

                    Vector3Int itemPosition = HitToMap(hit.point);                   
                    bool avaliable = CheckPlaneUpdate(itemPosition);

                    if (avaliable) {
                        if (Input.GetMouseButtonDown(0)) {
                            Generate(itemPosition);
                        }                                 
                    }
                }
            }
        }
    }

    
    protected virtual void Generate(Vector3Int _itemPosition) {
        // MapDataController.CONTEXT.BuildItem(_itemPosition, item_prefab.id, item_prefab.size);
        Item item = Instantiate(item_prefab);
        item.transform.position = new Vector3(_itemPosition.x + (item_prefab.size.x - 1) / 2.0f, _itemPosition.y, _itemPosition.z + (item_prefab.size.z - 1) / 2.0f);
    }


    public virtual void StartGenerating() {
        if (m_isWorking == true) {
            ExitBuilding();
        }
        m_isWorking = true;
        m_itemPre = Instantiate(itemPre_prefab);
    }

    public virtual void ExitBuilding() {
        if (m_itemPre != null)
            Destroy(m_itemPre.gameObject);

        //foreach (MapCheckPlane plane in m_checkPlane_array)
        //  Destroy(plane.gameObject);

        m_isWorking = false;
    }

    protected Vector3Int HitToMap(Vector3 _point) {
        int x = (int) Mathf.Round(_point.x - (item_prefab.size.x - 1) / 2.0f);
        int y = (int)Mathf.Round(_point.y);
        int z = (int) Mathf.Round(_point.z - (item_prefab.size.y - 1) / 2.0f);

        return new Vector3Int(x, y, z);
    }

    protected virtual bool CheckPlaneUpdate(Vector3Int _itemPosition) {
        bool avaliableFlag = true;// item_prefab.CheckMapAvaliable(_itemPosition);

        m_itemPre.SetState(avaliableFlag);

        m_itemPre.transform.position = new Vector3(_itemPosition.x + (item_prefab.size.x - 1) / 2.0f, _itemPosition.y, _itemPosition.z + (item_prefab.size.z - 1) / 2.0f);

        return avaliableFlag;
    }
}
