﻿using System;
using UnityEngine;

public class EventManager
{
  public static event Action carEnter;

  public static event Action<Transform> carExit;

  public static event Action<InventoryItem> pickItem;

  public static event Action toggleInventory;

  public static event Action wellOperate;

  public static event Action bucketSpawn;

  public static void OnCarEnter()
  {
    carEnter?.Invoke();
  }

  public static void OnCarExit(Transform t)
  {
    carExit?.Invoke(t);
  }

  public static void OnPickItem(InventoryItem item)
  {
    pickItem?.Invoke(item);
  }

  public static void OnInventoryToggle()
  {
    toggleInventory?.Invoke();
  }

  public static void OnWellOperate()
  {
    wellOperate?.Invoke();
  }

  public static void OnBucketSpawn()
  {
    bucketSpawn?.Invoke();
  }
}