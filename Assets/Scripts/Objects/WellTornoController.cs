using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control the behavior of the well bar when interacting
/// </summary>
public class WellTornoController : MonoBehaviour
{
  private Transform bucket;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    EventManager.wellOperate += OnWellOperate;
    bucket = transform.parent.parent.Find("Bucket");
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventManager.wellOperate -= OnWellOperate;
  }

  /// <summary>
  /// Event triggered when the player is interacting with the well
  /// </summary>
  void OnWellOperate()
  {
    transform.Rotate(Vector3.right * 90 * Time.deltaTime, Space.Self);
    bucket.Translate(Vector3.down * .2f * Time.deltaTime);
  }
}
