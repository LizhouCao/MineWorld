using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneratorArea : ItemGenerator
{
    /*
    private Vector2Int m_start_position;
    private bool m_is_startPoint_down = false;
    private int m_limit = 400;

    private ItemPre[] m_itemPreArray;
    private Vector2Int m_last_position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isWorking == true) {
            if (Input.GetMouseButtonDown(1)) {
                ExitBuilding();

                if (m_itemPreArray != null) {
                    foreach (ItemPre item in m_itemPreArray)
                        item.gameObject.SetActive(false);
                }
            }
            else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000.0f, 1 << 10)) {

                    Vector2Int itemPosition = HitToMap(hit.point);

                    if (m_is_startPoint_down == true)                   
                       itemPosition = VectorLimit(itemPosition);

                    bool avaliable = CheckPlaneUpdate(itemPosition);

                    if (m_is_startPoint_down == false) {
                        if (avaliable) {
                            if (Input.GetMouseButtonDown(0)) {
                                m_start_position = itemPosition;
                                m_is_startPoint_down = true;
                            }
                        }
                    }
                    else {
                        if (Input.GetMouseButtonUp(0)) {
                            m_is_startPoint_down = false;
                            Generate(itemPosition);

                            if (m_itemPreArray != null) {
                                foreach (ItemPre item in m_itemPreArray)
                                    item.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public override void ExitBuilding() {
        base.ExitBuilding();

        if (m_itemPreArray != null) {
            foreach (ItemPre item in m_itemPreArray) {
                if (item != null)
                    Destroy(item.gameObject);
            }
        }
    }

    protected override bool CheckPlaneUpdate(Vector2Int _itemPosition) {
        if (m_is_startPoint_down == false)
            base.CheckPlaneUpdate(_itemPosition);
        else {
            if (m_last_position!= _itemPosition) {

                m_last_position = _itemPosition;

                Vector2Int v1 = m_start_position;
                Vector2Int v2 = _itemPosition;

                VectorStandardize(ref v1, ref v2);

                int count = 0;
                for (int i = v1.x; i <= v2.x; i++) {
                    for (int j = v1.y; j <= v2.y; j++) {

                        ItemPre item = m_itemPreArray[count];
                        item.gameObject.SetActive(true);
                        CheckPlaneUpdate(item, new Vector2Int(i, j));
                        count++;
                    }
                }

                for (; count < m_itemPreArray.Length; count++)
                    m_itemPreArray[count].gameObject.SetActive(false);
            }
        }
        return true;
    }

    public override void StartGenerating() {
        base.StartGenerating();
        m_itemPreArray = new ItemPre[m_limit];

        for (int i = 0; i < m_limit; i++) {
            m_itemPreArray[i] = Instantiate(itemPre_prefab);
            m_itemPreArray[i].gameObject.SetActive(false);
        }

        m_last_position = new Vector2Int(-1, -1);
    }

    Vector2Int VectorLimit(Vector2Int _position) {
        int x_offset = Mathf.Abs(_position.x - m_start_position.x) + 1;
        int y_offset = Mathf.Abs(_position.y - m_start_position.y) + 1;

        float k = Mathf.Sqrt(1.0f * x_offset * y_offset / m_limit);

        int x_sign = 1, y_sign = 1;
        if (_position.x < m_start_position.x)
            x_sign = -1;
        if (_position.y < m_start_position.y)
            y_sign = -1;

        if (k > 1) {
            _position.x = m_start_position.x + x_sign * ((int)(x_offset / k) - 1);
            _position.y = m_start_position.y + y_sign * ((int)(y_offset / k) - 1);
        }

        return _position;
    }

    void VectorStandardize(ref Vector2Int _v1, ref Vector2Int _v2) {
        if (_v1.x > _v2.x) {
            int temp = _v1.x;
            _v1.x = _v2.x;
            _v2.x = temp;
        }

        if (_v1.y > _v2.y) {
            int temp = _v1.y;
            _v1.y = _v2.y;
            _v2.y = temp;
        }
    }

    protected override void Generate(Vector2Int _itemPosition) {
        Vector2Int v1 = m_start_position;
        Vector2Int v2 = _itemPosition;

        VectorStandardize(ref v1, ref v2);

        for (int i = v1.x; i <= v2.x; i++) {
            for (int j = v1.y; j <= v2.y; j++) {
                if (item_prefab.CheckMapAvaliable(new Vector2Int(i, j)))
                    base.Generate(new Vector2Int(i, j));
            }
        }

        // for (int i = m_is_startPoint_down)
        
        // base.Generate(_itemPosition);
    }

    bool CheckPlaneUpdate(ItemPre _item, Vector2Int _itemPosition) {
        bool avaliableFlag = item_prefab.CheckMapAvaliable(_itemPosition);

        _item.SetState(avaliableFlag);

        _item.transform.position = new Vector3(_itemPosition.x + (item_prefab.size.x - 1) / 2.0f, 0.0f, _itemPosition.y + (item_prefab.size.y - 1) / 2.0f);

        return avaliableFlag;
    }
    */
}
