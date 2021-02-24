using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control the behavior of the well bar when interacting
/// </summary>
public class WellTornoController : MonoBehaviour
{
  private Transform bucket;
  private GameObject water;
  private float initHeight;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    EventManager.wellOperate += OnWellOperate;
    bucket = transform.parent.parent.Find("Bucket");
    water = bucket.Find("Water").gameObject;

    initHeight = bucket.position.y;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventManager.wellOperate -= OnWellOperate;
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update()
  {
    if (bucket.position.y < -.1f)
    {
      water.SetActive(true);
    }
  }

  /// <summary>
  /// Event triggered when the player is interacting with the well
  /// </summary>
  void OnWellOperate()
  {
    if (bucket.position.y >= initHeight && water.activeSelf)
    {
      return;
    }
    Vector3 eulerRotation = water.activeSelf ? Vector3.left : Vector3.right;
    Vector3 translation = water.activeSelf ? Vector3.up : Vector3.down;
    transform.Rotate(eulerRotation * 90 * Time.deltaTime, Space.Self);
    bucket.Translate(translation * .2f * Time.deltaTime);
  }
}
