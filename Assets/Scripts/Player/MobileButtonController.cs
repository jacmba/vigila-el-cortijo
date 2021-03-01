using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Script to handle mobile device buttons
/// </summary>
public class MobileButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  private bool pressed = false;

  /// <summary>
  /// Button pressed event
  /// </summary>
  /// <param name="data">Event info</param>
  public void OnPointerDown(PointerEventData data)
  {
    pressed = true;
  }

  /// <summary>
  /// Button released
  /// </summary>
  /// <param name="data">Event info</param>
  public void OnPointerUp(PointerEventData data)
  {
    pressed = false;
  }

  /// <summary>
  /// Check button status
  /// </summary>
  /// <returns>true when button is pressed</returns>
  public bool isPressed()
  {
    return pressed;
  }
}
