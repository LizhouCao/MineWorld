using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController CONTEXT;
    public CityController City {
        get {
            return this.city;
        }
    }
    public CityController city;

    public ItemGeneratorController ItemGeneratorCtrl {
        get { return m_itemGeneratorCtrl; }
    }

    private ItemGeneratorController m_itemGeneratorCtrl;
    private int[] m_timeScale = { 0, 1, 2, 4, 8};
    private int num = 1;

    private void Awake() {
        CONTEXT = this;
        m_itemGeneratorCtrl = this.GetComponentInChildren<ItemGeneratorController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            num = (num + 1) % 5;
            Time.timeScale = m_timeScale[num];
        }
    }
}
