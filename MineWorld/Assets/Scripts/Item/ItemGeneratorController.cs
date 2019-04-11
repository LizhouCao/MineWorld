using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneratorController : MonoBehaviour
{
    public static ItemGeneratorController CONTEXT;
    ItemGenerator m_itemGenerator;

    private void Awake() {
        CONTEXT = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
