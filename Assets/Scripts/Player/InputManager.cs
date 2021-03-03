using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Imput handling implementation class
/// </summary>
public class InputManager : IInputManager
{
  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update()
  {
    throttle = Input.GetAxis("Vertical");
    steer = Input.GetAxis("Horizontal");

    h = Input.GetAxis("Horizontal");
    v = Input.GetAxis("Vertical");

    a = Input.GetButton("Action1");
    b = Input.GetButton("Action2");

    select = Input.GetButton("Select");
    escape = Input.GetButton("Cancel");
  }
}
