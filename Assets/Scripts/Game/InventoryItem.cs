using System;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
  public ItemType type;

  public string name;

  public int pickAmount;

  public int maxAmount;

  private int amount = 0;

  public Sprite icon;

  public void increase(int a)
  {
    amount += a;
    if (amount > maxAmount)
    {
      amount = maxAmount;
    }
  }

  public int count()
  {
    return amount;
  }
}