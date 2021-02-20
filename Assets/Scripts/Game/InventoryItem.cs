using System;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Data type to hold information of items stored in inventory
/// </summary>
public class InventoryItem
{
  /// <summary>
  /// Type of item
  /// </summary>
  public ItemType type;

  /// <summary>
  /// Name of item
  /// </summary>
  public string name;

  /// <summary>
  /// Amount to be increased when item is picked
  /// </summary>
  public int pickAmount;

  /// <summary>
  /// Maximum number of item occurrences
  /// </summary>
  public int maxAmount;

  /// <summary>
  /// Icon sprite to be drawn on inventory
  /// </summary>
  public Sprite icon;

  private int amount = 0;

  /// <summary>
  /// Increase amount of items up to max amount
  /// </summary>
  /// <param name="a">Number to increase</param>
  public void increase(int a)
  {
    amount += a;
    if (amount > maxAmount)
    {
      amount = maxAmount;
    }
  }

  /// <summary>
  /// Count item ocurrences
  /// </summary>
  /// <returns>Amount of items</returns>
  public int count()
  {
    return amount;
  }

  /// <summary>
  /// Set amount of items to zero
  /// </summary>
  public void clear()
  {
    amount = 0;
  }
}