using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Process mobile virtual joystick input
/// </summary>
public class MobileJoystickController : MonoBehaviour, IPointerEnterHandler
{
  public void OnPointerEnter(PointerEventData data)
  {
    Debug.Log("Clicked!!");
  }
}