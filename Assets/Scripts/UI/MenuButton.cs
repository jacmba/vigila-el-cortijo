using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior of UI menu butytons
/// </summary>
public class MenuButton : MonoBehaviour
{
  [SerializeField]
  private MenuButtonType type;
  private Vector3 size;
  private Vector3 overSize;
  private Vector3 clickSize;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    size = transform.localScale;
    overSize = size * 1.5f;
    clickSize = size * 2f;
  }
}
