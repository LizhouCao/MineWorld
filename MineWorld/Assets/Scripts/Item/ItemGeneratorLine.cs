using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneratorLine : ItemGenerator
{
    private Vector3Int m_start_position;
    private bool m_is_startPoint_down = false;
    private int m_limit = 100;

    private ItemPre[] m_itemPreArray;
    private Vector3Int m_last_position;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
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
                if (Physics.Raycast(ray, out hit, 1000.0f, 1 << 11)) {

                    Vector3 localVector = this.GLobalToLocal(hit.point);
                    Vector3Int itemPosition = HitToMap(localVector);

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

    protected override bool CheckPlaneUpdate(Vector3Int _itemPosition) {
        if (m_is_startPoint_down == false)
            base.CheckPlaneUpdate(_itemPosition);
        else {
            if (m_last_position != _itemPosition) {

                m_last_position = _itemPosition;

                Vector3Int v1 = m_start_position;
                Vector3Int v2 = _itemPosition;

                VectorStandardize(ref v1, ref v2);

                int count = 0;
                for (int i = v1.x; i <= v2.x; i++) {
                    for (int j = v1.z; j <= v2.z; j++) {

                        ItemPre item = m_itemPreArray[count];
                        item.gameObject.SetActive(true);
                        CheckPlaneUpdate(item, new Vector3Int(i, v1.y, j));
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
            m_itemPreArray[i].transform.SetParent(SceneController.CONTEXT.city.transform);
            m_itemPreArray[i].gameObject.SetActive(false);
        }

        m_last_position = new Vector3Int(-1, -1, -1);
    }

    Vector3Int VectorLimit(Vector3Int _position) {
        int x_offset = _position.x - m_start_position.x;
        int z_offset = _position.z - m_start_position.z;

        if (Mathf.Abs(x_offset) > Mathf.Abs(z_offset)) {
            z_offset = 0;
            if (x_offset > m_limit)
                x_offset = m_limit;
            else if (x_offset < -m_limit)
                x_offset = -m_limit;
        }
        else {
            x_offset = 0;
            if (z_offset > m_limit)
                z_offset = m_limit;
            else if (x_offset < -m_limit)
                z_offset = -m_limit;
        }

        return m_start_position + new Vector3Int(x_offset, 0, z_offset);
    }

    void VectorStandardize(ref Vector3Int _v1, ref Vector3Int _v2) {
        if (_v1.x > _v2.x) {
            int temp = _v1.x;
            _v1.x = _v2.x;
            _v2.x = temp;
        }

        if (_v1.z > _v2.z) {
            int temp = _v1.z;
            _v1.z = _v2.z;
            _v2.z = temp;
        }
    }

    protected override void Generate(Vector3Int _itemPosition) {
        Vector3Int v1 = m_start_position;
        Vector3Int v2 = _itemPosition;

        VectorStandardize(ref v1, ref v2);

        for (int i = v1.x; i <= v2.x; i++) {
            for (int j = v1.z; j <= v2.z; j++) {
                if (item_prefab.CheckMapAvaliable(new Vector3Int(i, v1.y, j)))
                    base.Generate(new Vector3Int(i, v1.y, j));
            }
        }

        // for (int i = m_is_startPoint_down)

        // base.Generate(_itemPosition);
    }

    bool CheckPlaneUpdate(ItemPre _item, Vector3Int _itemPosition) {
        bool avaliableFlag = item_prefab.CheckMapAvaliable(_itemPosition);

        _item.SetState(avaliableFlag);

        _item.transform.localPosition = new Vector3(_itemPosition.x + (item_prefab.size.x - 1) / 2.0f, _itemPosition.y, _itemPosition.z + (item_prefab.size.z - 1) / 2.0f);
        _item.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        return avaliableFlag;
    }
}
