using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

  public InventoryItem item;

  // Update is called once per frame
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
        EventManager.OnPickItem(item);
        Destroy(gameObject);
      }
    }
  }
}