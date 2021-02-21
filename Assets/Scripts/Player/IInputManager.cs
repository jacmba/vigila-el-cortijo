using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface to process user input
/// </summary>
public abstract class IInputManager : MonoBehaviour
{
  /// <summary>
  /// Car input throttle axis
  /// </summary>
  /// <value></value>
  public float throttle { get; set; }

  /// <summary>
  /// Car input steering axis
  /// </summary>
  /// <value></value>
  public float steer { get; set; }

  /// <summary>
  /// Character input horizontal axis
  /// </summary>
  /// <value></value>
  public float h { get; set; }

  /// <summary>
  /// Vertical axis
  /// </summary>
  /// <value></value>
  public float v { get; set; }

  /// <summary>
  /// A (primary) button
  /// </summary>
  /// <value></value>
  public bool a { get; set; }

  /// <summary>
  /// B (secondary) button
  /// </summary>
  /// <value></value>
  public bool b { get; set; }

  /// <summary>
  /// SELECT action button
  /// </summary>
  /// <value></value>
  public bool select { get; set; }
}
