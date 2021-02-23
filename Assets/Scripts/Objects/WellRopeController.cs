using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Draws the rope that links bucket to well
/// </summary>
public class WellRopeController : MonoBehaviour
{
  /// <summary>
  /// Reference to bucket position
  /// </summary>
  public Transform bucket;

  private LineRenderer line;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    line = GetComponent<LineRenderer>();
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update()
  {
    line.SetPosition(0, transform.position);
    line.SetPosition(1, bucket.position);
  }
}
