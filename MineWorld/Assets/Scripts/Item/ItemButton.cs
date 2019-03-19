using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public ItemGenerator itemGenerator;

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
        ItemGeneratorController.CONTEXT.EnableGenerator(itemGenerator);
    }
}
