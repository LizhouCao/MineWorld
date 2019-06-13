using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityLevelUI : MonoBehaviour
{
    CityLevelButton[] m_toggles;

    public ColorBlock onColors;
    public ColorBlock offColors;
    public ColorBlock selectedColors;

    private void Awake() {
        m_toggles = this.GetComponentsInChildren<CityLevelButton>();

        System.Array.Reverse(m_toggles);

        for (int i = 0; i < m_toggles.Length; i++) {
            m_toggles[i].id = i;
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

    public void SelectLevel(int _level) {
        for (int i = 0; i < _level; i++) {
            m_toggles[i].GetComponent<Toggle>().colors = onColors;
        }
        m_toggles[_level].GetComponent<Toggle>().colors = selectedColors;
        for (int i = _level + 1; i < m_toggles.Length; i++) {
            m_toggles[i].GetComponent<Toggle>().colors = offColors;
        }

        SceneController.CONTEXT.city.LevelButtonSelected(_level);
    }
}
