using System;
using UnityEngine;

/// <summary>
/// Event bus class
/// </summary>
public class EventManager
{
  /// <summary>
  /// Subscribe to OnCarenter event
  /// </summary>
  public static event Action carEnter;

  /// <summary>
  /// Subscribe to OnCarExit event
  /// </summary>
  public static event Action<Transform> carExit;

  /// <summary>
  /// Subscribe to OnPickItem event
  /// </summary>
  public static event Action<InventoryItem> pickItem;

  /// <summary>
  /// Subscribe to OnToggleInventory event
  /// </summary>
  public static event Action toggleInventory;

  /// <summary>
  /// Subscribe to OnWellOperate event
  /// </summary>
  public static event Action wellOperate;

  /// <summary>
  /// Subscribe to OnBucketSpawn event
  /// </summary>
  public static event Action bucketSpawn;

  /// <summary>
  /// Subscribe to OnPauseAPressed
  /// </summary>
  public static event Action pauseAPressed;

  /// <summary>
  /// Subscribe to OnResume event
  /// </summary>
  public static event Action resume;

  /// <summary>
  /// Subscribe to OnExitGame event
  /// </summary>
  public static event Action exitGame;

  /// <summary>
  /// Event triggered when the Gañán jumps into the car
  /// </summary>
  public static void OnCarEnter()
  {
    carEnter?.Invoke();
  }

  /// <summary>
  /// Event triggered when the Gañán exits the car
  /// </summary>
  /// <param name="t">Exit position transform</param>
  public static void OnCarExit(Transform t)
  {
    carExit?.Invoke(t);
  }

  /// <summary>
  /// Event trigered when the Gañán picks an item
  /// </summary>
  /// <param name="item">Inventory item object</param>
  public static void OnPickItem(InventoryItem item)
  {
    pickItem?.Invoke(item);
  }

  /// <summary>
  /// Event trigered when select button is pressed to toggle inventory display
  /// </summary>
  public static void OnInventoryToggle()
  {
    toggleInventory?.Invoke();
  }

  /// <summary>
  /// Event triggerd when the Gañán is interacting with the well
  /// </summary>
  public static void OnWellOperate()
  {
    wellOperate?.Invoke();
  }

  /// <summary>
  /// Event triggered when water bucket is spawned after operating the well
  /// </summary>
  public static void OnBucketSpawn()
  {
    bucketSpawn?.Invoke();
  }

  /// <summary>
  /// Event triggered when A button is pressed during pause
  /// </summary>
  public static void OnPauseAPressed()
  {
    pauseAPressed?.Invoke();
  }

  /// <summary>
  /// Event triggered when pause exit requested
  /// </summary>
  public static void OnResume()
  {
    resume?.Invoke();
  }

  /// <summary>
  /// Event triggered when requesting exit the game
  /// </summary>
  public static void OnExitGame()
  {
    exitGame?.Invoke();
  }
}