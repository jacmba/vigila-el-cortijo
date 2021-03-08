using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

  public InventoryItem item;
  private EventManager eventManager;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    eventManager = EventManager.getInstance();
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update()
  {
    transform.Rotate(0, 1, 0);
  }

  /// <summary>
  /// OnTriggerStay is called when the Collider other enters the trigger.
  /// </summary>
  /// <param name="other">The other Collider involved in this collision.</param>
  void OnTriggerStay(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      ItemCollecter collecter = other.gameObject.GetComponent<ItemCollecter>();
      if (collecter.IsCollecting())
      {
        eventManager.OnPickItem(item);
        Destroy(gameObject);
      }
    }
  }
}