using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuilder : MonoBehaviour
{
    enum ItemBuilder_State {
        IDLE = 0,
        HOLD = 1
    };
    ItemBuilder_State m_state = ItemBuilder_State.IDLE;
    Item m_item = null;
    GameObject m_itemPreparing = null;

    public static ItemBuilder CONTEXT;

    private void Awake() {
        CONTEXT = this;
    }

    public List<Item> itemList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_state == ItemBuilder_State.HOLD) {
            if (Input.GetMouseButtonDown(1)) {
                UnSelect();
            }
            else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 200.0f, 1 << 9)) {
                    m_itemPreparing.SetActive(true);

                    Vector3 position = hit.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
                    m_itemPreparing.transform.position = position;

                    if (Input.GetMouseButtonDown(0)) {
                        PlaceItem(position);
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

    void PlaceItem(Vector3 _position) {
        GameObject model = Instantiate(m_item.model);
        model.transform.position = _position;
    }

    void UnSelect() {
        m_state = ItemBuilder_State.IDLE;
        Destroy(m_itemPreparing);
    }
}
