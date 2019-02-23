using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int id;
    public GameObject model;
    public GameObject prepareModel;

    // Start is called before the first frame update
    void Start()
    {
        Button button = this.GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(SelectItem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItem() {
        ItemBuilder.CONTEXT.SelectItem(this);
    }
}
