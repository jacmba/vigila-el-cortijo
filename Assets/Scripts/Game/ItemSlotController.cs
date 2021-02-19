using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotController : MonoBehaviour
{
  public InventoryItem item;

  public GameObject infoNode;
  private Image icon;
  private Text count;

  // Start is called before the first frame update
  void Start()
  {
    infoNode = transform.Find("ItemSlotInfo").gameObject;
  }

  // Update is called once per frame
  void Update()
  {
  }
}
