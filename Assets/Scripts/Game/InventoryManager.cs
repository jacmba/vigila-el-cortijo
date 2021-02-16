using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryManager
{
  public int slots = 10;
  private Dictionary<ItemType, InventoryItem> itemMap = new Dictionary<ItemType, InventoryItem>();

  public int getAvailSlots()
  {
    return slots - itemMap.Count;
  }

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
}
