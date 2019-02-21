using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public GameObject model;
    public GameObject prepareModel;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(SelectItem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItem() {
        ItemBuilder.CONTEXT.SelectItem(this);
    }


}
