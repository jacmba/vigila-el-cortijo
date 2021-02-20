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

  private InventoryManager inventory;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    inventory = GetComponent<GameController>().inventory;
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update()
  {
    for (int i = 0; i < slots.Length; i++)
    {
      slots[i].item = inventory.get(i);
    }
  }
}
