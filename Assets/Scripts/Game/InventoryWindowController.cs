using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to control display of inventory grid
/// </summary>
public class InventoryWindowController : MonoBehaviour
{
  /// <summary>
  /// Items grid slots
  /// </summary>
  public ItemSlotController[] slots;

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update()
  {
    InventoryManager inventory = GetComponent<GameController>().inventory;
    for (int i = 0; i < slots.Length; i++)
    {
      slots[i].item = inventory.get(i);
    }
  }
}
