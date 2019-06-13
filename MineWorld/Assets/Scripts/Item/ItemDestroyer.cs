using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDestroyer : ItemGenerator
{
    CityData.ItemGenerateInstruction m_instruction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (m_isWorking == true) {
            if (Input.GetMouseButtonDown(1)) {
                ExitBuilding();
            }
            else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, 1000.0f, 1 << 11)) {

                    Vector3 localVector = GLobalToLocal(hit.point);
                    Vector3Int itemPosition = HitToMap(localVector);

                    Vector3Int offsetPosition = SceneController.CONTEXT.city.Data.GetOffsetPosition(itemPosition);                   
                    
                    CityData.ItemGenerateInstruction instruction = SceneController.CONTEXT.city.Data.GetInstructionByPosition(offsetPosition);
                    
                    
                    if (instruction != m_instruction) {
                        if (m_instruction != null)
                            m_instruction.item.UnSelected();

                        m_instruction = instruction;

                        if (instruction != null)
                            instruction.item.Selected();
                    }

                    if (m_instruction != null) {
                        if (Input.GetMouseButtonDown(0)) {
                            SceneController.CONTEXT.city.ItemDestroy(m_instruction);
                            m_instruction = null;
                        }
                    }
                }
            }
        }
    }

    public override void StartGenerating() {
        m_isWorking = true;
    }

    public override void ExitBuilding() {
        m_isWorking = false;
        if (m_instruction != null)
            m_instruction.item.UnSelected();
    }
}
