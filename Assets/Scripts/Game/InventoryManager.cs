using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{
  public int slots = 10;

  private Dictionary<ItemType, int> itemMap = new Dictionary<ItemType, int>();

  public int getAvailSlots()
  {
    return slots - itemMap.Count;
  }

  public void insert(ItemType item, int amount)
  {
    if (amount <= 0)
    {
      return;
    }

    int items;
    if (itemMap.TryGetValue(item, out items))
    {
      itemMap.Remove(item);
    }
    else
    {
      items = 0;
    }
    itemMap.Add(item, items + amount);
  }

  public int count(ItemType item)
  {
    int items;
    if (itemMap.TryGetValue(item, out items))
    {
      return items;
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

    int items;
    int remain;
    if (itemMap.TryGetValue(item, out items))
    {
      remain = items - amount;
      if (remain <= 0)
      {
        itemMap.Remove(item);
        return items;
      }
      else
      {
        itemMap.Remove(item);
        itemMap.Add(item, remain);
        return amount;
      }
    }
    else
    {
      return 0;
    }
  }
}
