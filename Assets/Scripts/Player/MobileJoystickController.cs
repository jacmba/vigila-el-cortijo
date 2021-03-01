using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Process mobile virtual joystick input
/// </summary>
public class MobileJoystickController : IInputManager, IDragHandler, IPointerDownHandler,
  IPointerUpHandler
{
  /// <summary>
  /// Maximum pivot distance from origin
  /// </summary>
  public float maxDist = 5f;

  /// <summary>
  /// Joystick drag sentitivity
  /// </summary>
  public float step = 10f;

  private RectTransform rectTransform;
  private Vector2 origin;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    rectTransform = GetComponent<RectTransform>();
    origin = rectTransform.pivot;
  }

  /// <summary>
  /// Event triggered when joystick is pressed
  /// </summary>
  /// <param name="data">Event info</param>
  public void OnPointerDown(PointerEventData data)
  {
    OnDrag(data);
  }

  /// <summary>
  /// Event triggered when joystick is released
  /// </summary>
  /// <param name="data">Event info</param>
  public void OnPointerUp(PointerEventData data)
  {
    rectTransform.pivot = origin;
  }

  /// <summary>
  /// Event triggered when dragging handle
  /// </summary>
  /// <param name="data">Dragging information</param>
  public void OnDrag(PointerEventData data)
  {
    Vector2 move = rectTransform.pivot + data.delta * step * Time.deltaTime;
    float dist = Mathf.Abs((move - origin).magnitude);
    if (dist <= maxDist)
    {
      rectTransform.pivot = move;
    }
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update()
  {
    h = (rectTransform.pivot.x - origin.x) / maxDist;
    v = (rectTransform.pivot.y - origin.y) / maxDist;
    steer = h;
    throttle = v;
  }
}