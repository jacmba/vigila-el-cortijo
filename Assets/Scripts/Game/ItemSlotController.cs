using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script class to render items on inventory window
/// </summary>
public class ItemSlotController : MonoBehaviour
{
  /// <summary>
  /// Item data
  /// </summary>
  public InventoryItem item;

  /// <summary>
  /// Scene node holding elements to display
  /// </summary>
  public GameObject infoNode;

  /// <summary>
  /// Item icon sprite
  /// </summary>
  public Image icon;

  /// <summary>
  /// Text of number of items occurences
  /// </summary>
  public Text count;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    infoNode = transform.Find("ItemSlotInfo").gameObject;
    icon = infoNode.transform.Find("ItemSlotImage").gameObject.GetComponent<Image>();
    count = infoNode.transform.Find("ItemSlotText").gameObject.GetComponent<Text>();
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update()
  {
    if (item?.count() > 0)
    {
      icon.sprite = item.icon;
      count.text = "x" + item.count();
      infoNode.SetActive(true);
    }
    else
    {
      infoNode.SetActive(false);
    }
  }
}
