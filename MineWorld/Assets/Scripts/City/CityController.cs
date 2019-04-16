using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class CityController : MonoBehaviour
{
    public CityData Data {
        get { return m_cityData; }
    }

    private CityData m_cityData;

    public Transform LevelPlane(int _num) {
        return this.transform.Find("Level" + _num);
    }

    private void Awake() {
        // m_cityData = new CityData();
        m_cityData = LoadCityData("Save/", "data.xml");
        m_cityData.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_cityData.itemGenerateInstructions.Count; i++) {
            CityData.ItemGenerateInstruction instruction = m_cityData.itemGenerateInstructions[i];
            Item item = SelfGenerate(instruction);
            instruction.item = item;
            m_cityData.AddItemInMap(instruction);
        }
    }

    Item SelfGenerate(CityData.ItemGenerateInstruction _instruction) {
        Item prefab = SceneController.CONTEXT.ItemGeneratorCtrl.GetItemPrefabById(_instruction.id);
        Item item = prefab.Generate(this.LevelPlane(1), _instruction.position, _instruction.rotation);
        return item;
    }

    public void Generate(Item _item, Vector3Int _position, int _rotation) {
        Item item = _item.Generate(this.LevelPlane(1), _position, _rotation);
        m_cityData.AddInstruction(item, _position, _rotation);
    }

    public void ItemDestroy(CityData.ItemGenerateInstruction _instruction) {
        m_cityData.RemoveInstruction(_instruction);
        Destroy(_instruction.item.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            this.SaveCityData(m_cityData, "Save/", "data.xml");
        }
    }

    CityData LoadCityData(string _dir, string _file) {
        XmlSerializer xml = new XmlSerializer(typeof(CityData));
        using (StreamReader sr = new StreamReader(_dir + _file)) {
            return (CityData)xml.Deserialize(sr);
        }
    }

    void SaveCityData(CityData _config, string _dir, string _file) {
        XmlSerializer xml = new XmlSerializer(typeof(CityData));
        using (StreamWriter sw = new StreamWriter(_dir + _file)) {
            xml.Serialize(sw, _config);
        }
    }

    public Transform FollowTarget{
        get {
            return this.transform.Find("FollowTarget");
        }
    }
}
