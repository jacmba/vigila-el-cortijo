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

  private GameController gc;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    gc = GetComponent<GameController>();
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update()
  {
    for (int i = 0; i < slots.Length; i++)
    {
      slots[i].item = gc.inventory.get(i);
    }
  }
}
