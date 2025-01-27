using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    private GameManager gameManager;

    public string itemName;
    public int itemID;
    
    public virtual void Use()
    {
        // Общая логика использования предмета
        Debug.Log($"Используется предмет: {itemName}");
    }
}
