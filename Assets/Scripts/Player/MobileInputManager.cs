using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle input for mobile devices
/// </summary>
public class MobileInputManager : IInputManager
{
  /// <summary>
  /// Reference to player controller script
  /// </summary>
  public PlayerController player;

  private MobileJoystickController joystickController;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    player.im = this;
    joystickController = GetComponentInChildren<MobileJoystickController>();
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update()
  {
    h = joystickController.h;
    v = joystickController.v;
  }
}
