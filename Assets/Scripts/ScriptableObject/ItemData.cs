using System;
using UnityEngine;

// 사용 방법에 따른 타입 구분
public enum ItemType
{
    Equipable,
    Consumable,
    Resource
}

// 소비했을 때 효과에 따른 타입 구분
public enum ConsumableType
{
    Health,
    SpeedUp
}

// 아이템이 가진 능력치 값
[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;
}
