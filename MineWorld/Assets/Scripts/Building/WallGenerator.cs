using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallGenerator : ItemGenerator {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, 1000.0f, 1 << 11)) {
            Vector3 localPoint = this.GlobalToLocal(hit.transform, hit.point);
            Vector3Int pointInt = this.HitPointToMap(localPoint);
            pointInt += SceneController.CONTEXT.arkBuildingController.buildingData.offset;
        }
    }

    protected Vector3 GlobalToLocal(Transform _transform, Vector3 _vector) {
        return _transform.InverseTransformPoint(_vector);
    }
}