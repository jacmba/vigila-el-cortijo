using System;
using UnityEngine;

/// <summary>
/// Event bus class
/// </summary>
public class EventManager
{
  /// <summary>
  /// Singleton instance
  /// </summary>
  private static EventManager instance = null;

  /// <summary>
  /// Private constructor to force singleton usage
  /// </summary>
  private EventManager() { }

  /// <summary>
  /// Get singleton instance
  /// </summary>
  /// <returns>EventManager singleton instance</returns>
  public static EventManager getInstance()
  {
    if (instance == null)
    {
      instance = new EventManager();
    }
    return instance;
  }

  /// <summary>
  /// Subscribe to OnCarenter event
  /// </summary>
  public event Action carEnter;

  /// <summary>
  /// Subscribe to OnCarExit event
  /// </summary>
  public event Action<Transform> carExit;

  /// <summary>
  /// Subscribe to OnPickItem event
  /// </summary>
  public event Action<InventoryItem> pickItem;

  /// <summary>
  /// Subscribe to OnToggleInventory event
  /// </summary>
  public event Action toggleInventory;

  /// <summary>
  /// Subscribe to OnWellOperate event
  /// </summary>
  public event Action wellOperate;

  /// <summary>
  /// Subscribe to OnBucketSpawn event
  /// </summary>
  public event Action bucketSpawn;

  /// <summary>
  /// Subscribe to OnPauseAPressed
  /// </summary>
  public event Action pauseAPressed;

  /// <summary>
  /// Subscribe to OnResume event
  /// </summary>
  public event Action resume;

  /// <summary>
  /// Subscribe to OnExitGame event
  /// </summary>
  public event Action exitGame;

  /// <summary>
  /// Event triggered when the Gañán jumps into the car
  /// </summary>
  public void OnCarEnter()
  {
    carEnter?.Invoke();
  }

  /// <summary>
  /// Event triggered when the Gañán exits the car
  /// </summary>
  /// <param name="t">Exit position transform</param>
  public void OnCarExit(Transform t)
  {
    carExit?.Invoke(t);
  }

  /// <summary>
  /// Event trigered when the Gañán picks an item
  /// </summary>
  /// <param name="item">Inventory item object</param>
  public void OnPickItem(InventoryItem item)
  {
    pickItem?.Invoke(item);
  }

  /// <summary>
  /// Event trigered when select button is pressed to toggle inventory display
  /// </summary>
  public void OnInventoryToggle()
  {
    toggleInventory?.Invoke();
  }

  /// <summary>
  /// Event triggerd when the Gañán is interacting with the well
  /// </summary>
  public void OnWellOperate()
  {
    wellOperate?.Invoke();
  }

  /// <summary>
  /// Event triggered when water bucket is spawned after operating the well
  /// </summary>
  public void OnBucketSpawn()
  {
    bucketSpawn?.Invoke();
  }

  /// <summary>
  /// Event triggered when A button is pressed during pause
  /// </summary>
  public void OnPauseAPressed()
  {
    pauseAPressed?.Invoke();
  }

  /// <summary>
  /// Event triggered when pause exit requested
  /// </summary>
  public void OnResume()
  {
    resume?.Invoke();
  }

  /// <summary>
  /// Event triggered when requesting exit the game
  /// </summary>
  public void OnExitGame()
  {
    exitGame?.Invoke();
  }
}