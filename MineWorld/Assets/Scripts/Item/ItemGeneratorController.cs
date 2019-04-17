using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneratorController : MonoBehaviour
{
    ItemGenerator m_itemGenerator;

    public GameObject menu;

    public List<Item> item_prefabs;
    private Dictionary<int, Item> m_itemDictionary;

    private void Awake() {
        m_itemDictionary = new Dictionary<int, Item>();
        foreach (Item item in item_prefabs) {
            m_itemDictionary.Add(item.id, item);
        }
    }

    /*public enum State {
        IDLE = 0,
        GENERATING = 1,
        DESTROY = 2
    }

    State m_state = State.IDLE;
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Item GetItemPrefabById(int _id) {
        return m_itemDictionary[_id];
    }

    public void SetMenuActive(bool _isOn) {
        if (menu != null) {
            menu.SetActive(_isOn);
        }
    }

    public void EnableGenerator(ItemGenerator _generator) {
        if (m_itemGenerator != null) {
            m_itemGenerator.ExitBuilding();
            Destroy(m_itemGenerator.gameObject);
        }

        m_itemGenerator = Instantiate(_generator);
        m_itemGenerator.StartGenerating();
    }
}
