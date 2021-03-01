using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle input for mobile devices
/// </summary>
public class MobileInputManager : IInputManager
{
  [SerializeField]
  private PlayerController player;

  [SerializeField]
  private CarController car;

  private MobileJoystickController joystickController;

  [SerializeField]
  private MobileButtonController btnAController;

  [SerializeField]
  private MobileButtonController btnBController;

  [SerializeField]
  private MobileButtonController btnSelectController;

  private IInputManager gcim;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    player.im = this;
    car.im = this;
    joystickController = GetComponentInChildren<MobileJoystickController>();

    if (transform.parent != null && transform.parent.name == "GameController")
    {
      GameObject gameController = transform.parent.gameObject;
      gcim = gameController.GetComponent<IInputManager>();
    }
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update()
  {
    h = joystickController.h;
    gcim.h = h;
    v = joystickController.v;
    gcim.v = v;
    steer = joystickController.steer;
    gcim.steer = steer;
    throttle = joystickController.throttle;
    gcim.throttle = throttle;

    a = btnAController.isPressed();
    gcim.a = a;
    b = btnBController.isPressed();
    gcim.b = b;
    select = btnSelectController.isPressed();
    gcim.select = select;
  }
}
