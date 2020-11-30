using UnityEngine;
using System;

public class EventManager
{
  public static event Action carEnter;
  public static event Action carExit;

  public static void OnCarEnter() {
    carEnter?.Invoke();
  }
}
