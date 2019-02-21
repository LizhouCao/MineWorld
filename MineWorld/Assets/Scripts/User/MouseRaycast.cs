using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
    public GameObject GreenPlanePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200.0f, 1 << 9)) {
            GameObject plane = Instantiate(GreenPlanePrefab);
            plane.transform.position = hit.transform.position + new Vector3(0.0f, 0.55f, 0.0f);
            // hit.transform.Find("Plane").gameObject.SetActive(true);
        }
    }
}
