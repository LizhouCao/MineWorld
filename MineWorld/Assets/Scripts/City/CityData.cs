using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class CityData
{
    public int energy;
    public int money;
    public int food;
    public int water;

    public class ItemGenerateInstruction {
        public int id;
        public Vector3Int position;
        public int rotation;

        [XmlIgnore]
        public Item item;
    }

    
    public List<ItemGenerateInstruction> itemGenerateInstructions = new List<ItemGenerateInstruction>();
    public Vector3Int citySize = new Vector3Int(20, 1, 30);

    [XmlIgnore]
    private ItemGenerateInstruction[,,] m_instructions;

    public CityData() {

    }

    public void Init() {
        m_instructions = new ItemGenerateInstruction[citySize.x + 10, citySize.y + 2, citySize.z + 10];
    }

    public void AddInstruction(Item _item, Vector3Int _position, int _rotation) {
        ItemGenerateInstruction instruction = new ItemGenerateInstruction();
        instruction.id = _item.id;
        instruction.item = _item;
        instruction.position = _position;
        instruction.rotation = _rotation;

        itemGenerateInstructions.Add(instruction);

        this.AddItemInMap(instruction);
    }

    public void AddItemInMap(ItemGenerateInstruction _instruction) {
        Vector3Int pos1 = new Vector3Int(), pos2 = new Vector3Int();

        GetStartAndEnd(_instruction, ref pos1, ref pos2);

        for (int i = pos1.x; i <= pos2.x; i++) {
            for (int j = pos1.z; j <= pos2.z; j++) {
                m_instructions[i, 1, j] = _instruction;
            }
        }
    }

    void Standardize(ref Vector3Int _v1, ref Vector3Int _v2) {
        if (_v1.x > _v2.x) {
            int temp = _v1.x;
            _v1.x = _v2.x;
            _v2.x = temp;
        }

        if (_v1.z > _v2.z) {
            int temp = _v1.z;
            _v1.z = _v2.z;
            _v2.z = temp;
        }
    }

    public void GetStartAndEnd(Vector3Int _position, int _rotation, Vector3Int _size, ref Vector3Int _pos1, ref Vector3Int _pos2) {
        Vector3Int pos = _position + new Vector3Int(citySize.x / 2, 0, citySize.z / 2);

        
        _pos1 = new Vector3Int(-(_size.x - 1) / 2, 0, -(_size.z - 1) / 2);
        _pos2 = new Vector3Int(_size.x / 2, 0, _size.z / 2);

        if (_size.x > 1 || _size.z > 1) {
            switch (_rotation) {
                case 0: break;
                case 1: { int temp = _pos1.x; _pos1.x = _pos1.z; _pos1.z = -temp; temp = _pos2.x; _pos2.x = _pos2.z; _pos2.z = -temp; break; }
                case 2: { _pos1.x = -_pos1.x; _pos1.z = -_pos1.z; _pos2.x = -_pos2.x; _pos2.z = -_pos2.z; break; }
                case 3: { int temp = _pos1.x; _pos1.x = -_pos1.z; _pos1.z = temp; temp = _pos2.x; _pos2.x = -_pos2.z; _pos2.z = temp; break; }
            }
        }

        _pos1 += pos;
        _pos2 += pos;

        this.Standardize(ref _pos1, ref _pos2);
    }

    public void GetStartAndEnd(ItemGenerateInstruction _instruction, ref Vector3Int _pos1, ref Vector3Int _pos2) {
        GetStartAndEnd(_instruction.position, _instruction.rotation, _instruction.item.size, ref _pos1, ref _pos2);
    }

    public Vector3Int GetOffsetPosition(Vector3Int _position) {
        return _position + new Vector3Int(citySize.x / 2, 0, citySize.z / 2);
    }

    public ItemGenerateInstruction GetInstructionByPosition(Vector3Int _position) {
        return m_instructions[_position.x, 1, _position.z];
    }

    public void RemoveInstruction(ItemGenerateInstruction _instruction) {
        itemGenerateInstructions.Remove(_instruction);

        Vector3Int pos1 = new Vector3Int(), pos2 = new Vector3Int();
         
        GetStartAndEnd(_instruction, ref pos1, ref pos2);

        for (int i = pos1.x; i <= pos2.x; i++) {
            for (int j = pos1.z; j <= pos2.z; j++) {
                m_instructions[i, 1, j] = null;
            }
        }
    }
}
