using System;
using UnityEngine;

public class EventManager
{
  public static event Action carEnter;

  public static event Action<Transform> carExit;

  public static void OnCarEnter()
  {
    carEnter?.Invoke();
  }

  public static void OnCarExit(Transform t)
  {
    carExit?.Invoke(t);
  }
}
