using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior of area that consumes an item and triggers an action
/// </summary>
public class ConsumingArea : MonoBehaviour
{
  [SerializeField]
  private GameController gc;

  [SerializeField]
  private ItemType type;

  [SerializeField]
  private SerializeField amount;

  private IInputManager im;
  private InventoryManager inventory;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    im = gc.gameObject.GetComponent<IInputManager>();
    inventory = InventoryManager.getInstance();
  }
}
