using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator CONTEXT;
    public int mapSize = 200;

    string m_dir = "Data/";
    string m_file = "save1.mw";

    private void Awake() {
        CONTEXT = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bool tryLoad = Load(m_dir + m_file);

        if (!tryLoad)
            this.InitMap();
    }

    void InitMap() {
        MapDataController.CONTEXT.ResetMap(mapSize);
        for (int i = 0; i < mapSize; i++) {
            for (int j = 0; j < mapSize; j++) {
                // ItemBuilderOld.CONTEXT.PlaceItem(1, new Vector3(i, 0, j));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            Save(m_dir, m_file);
        }
    }

    public void Load() {
        Load(m_dir + m_file);
    }

    public void Save() {
        Save(m_dir, m_file);
    }

    bool Load(string _file) {
        if (!File.Exists(_file)) {
            return false;
        }
        string mapString = File.ReadAllText(_file);

        int count = 0;
        int y = 0;
        MapDataController.CONTEXT.ResetMap(mapSize);

        for (int i = 0; i < mapString.Length; i++) {
            if (mapString[i] == '\0') {
                count++;
                y = 0;
                continue;
            }
            // ItemBuilderOld.CONTEXT.PlaceItem(mapString[i], new Vector3(count / mapSize, y, count % mapSize));
            y++;
        }

        return true;
    }

    void Save(string _dir, string _file) {
        if (!Directory.Exists(_dir)) {
            Directory.CreateDirectory(_dir);
        }

        if (!File.Exists(_dir + _file)) {
            File.Create(_dir + _file).Dispose();
        }

        string mapString = "";
        
        File.WriteAllText(_dir + _file, mapString);
    }
}
