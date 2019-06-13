using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemPre itemPre_prefab;
    public int id;
    public Vector3Int size;

    private ItemPre m_itemPre;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual bool CheckMapAvaliable(Vector3Int _position, int _rotation) {
        Vector3Int pos1 = new Vector3Int();
        Vector3Int pos2 = new Vector3Int();

        SceneController.CONTEXT.city.Data.GetStartAndEnd(_position, _rotation, size, ref pos1, ref pos2);

        for (int i = pos1.x; i <= pos2.x; i++) {
            for (int j = pos1.z; j <= pos2.z; j++) {
                CityData.ItemGenerateInstruction instruction = SceneController.CONTEXT.city.Data.GetInstructionByPosition(new Vector3Int(i, pos1.y, j));

                if (instruction != null)
                    return false;
            }
        }

        return true;
    }

    public virtual bool CheckAndBuild(Vector3Int _position, int _rotation) {
        bool check = CheckMapAvaliable(_position, _rotation);
        return check;
    }

    public virtual Item Generate(Transform _floor, Vector3Int _position, int _rotation) {
        Item item = Instantiate(this);

        item.transform.SetParent(_floor);
        item.transform.localPosition = new Vector3(_position.x - 0.5f, 0.0f, _position.z - 0.5f);
        item.transform.localRotation = Quaternion.Euler(0.0f, 90.0f * _rotation, 0.0f);

        return item;
    }

    public void Selected() {
        this.transform.Find("model").gameObject.SetActive(false);
        m_itemPre = Instantiate(itemPre_prefab);
        m_itemPre.transform.SetParent(this.transform, false);
        m_itemPre.transform.localPosition = Vector3.zero;
        m_itemPre.transform.localRotation = Quaternion.Euler(Vector3.zero);
        m_itemPre.SetState(true);
    }

    public void UnSelected() {
        this.transform.Find("model").gameObject.SetActive(true);
        Destroy(m_itemPre.gameObject);
    }
}
