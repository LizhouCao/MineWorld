using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemGenerator : MonoBehaviour
{
    protected bool m_isWorking = false;

    public Item item_prefab;

    private ItemPre m_itemPre;

    protected int m_rotation_offset = 0;

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

                    Vector3 localVector = GLobalToLocal(hit.point);
                    Vector3Int itemPosition = HitToMap(localVector);                   
                    bool avaliable = CheckPlaneUpdate(itemPosition);

                    if (avaliable) {
                        if (Input.GetMouseButtonDown(0)) {
                            Generate(itemPosition);
                        }                                 
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.E)) {
                m_rotation_offset = (m_rotation_offset + 1) % 4;
                m_itemPre.SetRotation(m_rotation_offset);
            }
            else if (Input.GetKeyDown(KeyCode.Q)) {
                m_rotation_offset = (m_rotation_offset - 1 + 4) % 4;
                m_itemPre.SetRotation(m_rotation_offset);
            }
        }
    }

    protected Vector3 GLobalToLocal(Vector3 _vector) {
        return SceneController.CONTEXT.City.transform.InverseTransformPoint(_vector);
    }

    
    protected virtual void Generate(Vector3Int _itemPosition) {
        // MapDataController.CONTEXT.BuildItem(_itemPosition, item_prefab.id, item_prefab.size);
        // item_prefab.Build(SceneController.CONTEXT.city.LevelPlane(1), _itemPosition, m_rotation_offset);
        SceneController.CONTEXT.city.Generate(item_prefab, _itemPosition, m_rotation_offset);
    }


    public virtual void StartGenerating() {
        if (m_isWorking == true) {
            ExitBuilding();
        }
        m_isWorking = true;
        m_itemPre = Instantiate(item_prefab.itemPre_prefab);
        m_itemPre.transform.SetParent(SceneController.CONTEXT.city.transform, false);
        m_itemPre.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    public virtual void ExitBuilding() {
        if (m_itemPre != null)
            Destroy(m_itemPre.gameObject);

        //foreach (MapCheckPlane plane in m_checkPlane_array)
        //  Destroy(plane.gameObject);

        m_isWorking = false;
    }

    protected Vector3Int HitToMap(Vector3 _point) {
        int x = (int) Mathf.Round(_point.x + 0.5f);
        int y = (int)Mathf.Round(_point.y);
        int z = (int) Mathf.Round(_point.z + 0.5f);

        return new Vector3Int(x, y, z);
    }

    protected virtual bool CheckPlaneUpdate(Vector3Int _itemPosition) {
        bool avaliableFlag = item_prefab.CheckMapAvaliable(_itemPosition, m_rotation_offset);

        m_itemPre.SetState(avaliableFlag);

        m_itemPre.transform.localPosition = new Vector3(_itemPosition.x - 0.5f, _itemPosition.y, _itemPosition.z - 0.5f);
        // m_itemPre.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        return avaliableFlag;
    }
}
