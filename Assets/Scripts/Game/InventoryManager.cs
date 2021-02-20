using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Data type to manage main character's inventory
/// </summary>
public class InventoryManager
{
  /// <summary>
  /// Initial number of item slots. Will be synced to actual inventory window positions
  /// </summary>
  public int slots = 10;
  private Dictionary<ItemType, InventoryItem> itemMap = new Dictionary<ItemType, InventoryItem>();

  /// <summary>
  /// Get number of available slots
  /// </summary>
  /// <returns>Integer</returns>
  public int getAvailSlots()
  {
    return slots - itemMap.Count;
  }

  /// <summary>
  /// Insert an item in inventory if there is room for it
  /// </summary>
  /// <param name="item">Item data to insert</param>
  public void insert(InventoryItem item)
  {
    if (item.pickAmount <= 0)
    {
      return;
    }

    InventoryItem current;
    if (itemMap.TryGetValue(item.type, out current))
    {
      current.increase(item.pickAmount);
    }
    else
    {
      item.increase(item.pickAmount);
      itemMap.Add(item.type, item);
    }
  }

  /// <summary>
  /// Get number of occurencies for an item type
  /// </summary>
  /// <param name="item">Type of item</param>
  /// <returns>Integer</returns>
  public int count(ItemType item)
  {
    InventoryItem current;
    if (itemMap.TryGetValue(item, out current))
    {
      return current.count();
    }
    else
    {
      return 0;
    }
  }

  /// <summary>
  /// Take an item from inventory
  /// </summary>
  /// <param name="item">Type of item</param>
  /// <param name="amount">Number of items requested to use</param>
  /// <returns>Integer - Actual number of items used</returns>
  public int use(ItemType item, int amount)
  {
    if (amount <= 0)
    {
      return 0;
    }

    InventoryItem current;
    if (itemMap.TryGetValue(item, out current))
    {
      int remain = current.count() - amount;
      if (remain <= 0)
      {
        itemMap.Remove(item);
        return current.count();
      }
      else
      {
        current.increase(-amount);
        return amount;
      }
    }
    else
    {
      return 0;
    }
  }

  /// <summary>
  /// Get an item from inventory by position
  /// </summary>
  /// <param name="pos">Position of the item in dictionary</param>
  /// <returns>InventoryItem</returns>
  public InventoryItem get(int pos)
  {
    if (pos >= 0 && pos < itemMap.Count)
    {
      Dictionary<ItemType, InventoryItem>.ValueCollection values = itemMap.Values;
      InventoryItem[] valueArr = (new List<InventoryItem>(values)).ToArray();
      return valueArr[pos];
    }
    else
    {
      return null;
    }
  }
}
