using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuilderOld: MonoBehaviour
{
    enum ItemBuilder_State {
        IDLE = 0,
        HOLD = 1,
        DESTROY = 2
    };
    ItemBuilder_State m_state = ItemBuilder_State.IDLE;
    Item m_item = null;
    GameObject m_itemPreparing = null;

    public static ItemBuilderOld CONTEXT;
    public List<Item> itemList;
    private Dictionary<int, Item> itemDictionary;

    private void Awake() {
        CONTEXT = this;
        itemDictionary = new Dictionary<int, Item>();
        foreach (Item item in itemList) {
            itemDictionary.Add(item.id, item);
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

    private void LateUpdate() {
        if (m_state == ItemBuilder_State.HOLD) {
            if (Input.GetMouseButtonDown(1)) {
                UnSelect();
            }
            else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, 1 << 9)) {
                    if (MapDataController.CONTEXT.CheckAddItem(m_item, Vector3Int.RoundToInt(hit.transform.position))) {
                        m_itemPreparing.SetActive(true);

                        Vector3 position = hit.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
                        m_itemPreparing.transform.position = position;

                        if (Input.GetMouseButtonDown(0)) {
                            PlaceItem(m_item, position);
                        }
                    }
                    else {
                        m_itemPreparing.SetActive(false);
                    }
                }
            }
        }
        else if (m_state == ItemBuilder_State.DESTROY) {
            if (Input.GetMouseButtonDown(1)) {
                ExitDestroyMode();
            }
            else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, 1 << 9)) {
                   
                    if (Input.GetMouseButtonDown(0)) {
                        DestroyItem(hit.transform.gameObject, hit.transform.position);
                    }
                }
            }
        }
    }

    public void SelectItem(Item _item) {
        m_state = ItemBuilder_State.HOLD;
        m_item = _item;
        m_itemPreparing = Instantiate(_item.prepareModel);
        m_itemPreparing.SetActive(false);
    }

    public void PlaceItem(int _id, Vector3 _position) {
        Item item = GetItemByID(_id);
        if (item != null)
            PlaceItem(item, _position);
    }

    public void DestroyItem(GameObject _obj, Vector3 _position) {
        bool flag = MapDataController.CONTEXT.DeleteItem(Vector3Int.RoundToInt(_position));
        if (flag)
            Destroy(_obj);
    }

    public void DestroyMode() {
        m_state = ItemBuilder_State.DESTROY;
    }

    void PlaceItem(Item _item, Vector3 _position) {
        GameObject model = Instantiate(_item.model);
        model.transform.SetParent(this.transform.Find("MapItems"), false);
        model.transform.localPosition = _position;
        MapDataController.CONTEXT.AddItem(_item.id, Vector3Int.RoundToInt(_position));
    }

    void UnSelect() {
        m_state = ItemBuilder_State.IDLE;
        Destroy(m_itemPreparing);
    }

    void ExitDestroyMode() {
        m_state = ItemBuilder_State.IDLE;
    }

    public Item GetItemByID(int _id) {
        return itemDictionary[_id];
    }
}
