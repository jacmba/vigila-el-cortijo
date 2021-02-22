﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main game logic
/// </summary>
public class GameController : MonoBehaviour
{
  /// <summary>
  /// Player character instance
  /// </summary>
  public GameObject player;

  /// <summary>
  /// Player car instance
  /// </summary>
  public GameObject car;

  /// <summary>
  /// Main camera handle
  /// </summary>
  public CameraController mainCamera;

  /// <summary>
  /// Not used currently
  /// </summary>
  public float MaxTimer = 3f;

  /// <summary>
  /// Inventory reference
  /// </summary>
  public InventoryManager inventory;

  private float timer;
  private GameObject inventoryWindow;
  private bool showInventory;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    timer = 0f;

    inventoryWindow = transform.Find("InventoryWindow").gameObject;
    InventoryWindowController inventoryWindowController = GetComponent<InventoryWindowController>();
    inventory.slots = inventoryWindowController.slots.Length;

    showInventory = false;

    // Setup event observers
    EventManager.carEnter += OnCarEnter;
    EventManager.carExit += OnCarExit;
    EventManager.pickItem += OnPickItem;
    EventManager.toggleInventory += OnInventoryToggle;

    inventory = new InventoryManager();
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventManager.carEnter -= OnCarEnter;
    EventManager.carExit -= OnCarExit;
    EventManager.pickItem -= OnPickItem;
    EventManager.toggleInventory -= OnInventoryToggle;
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update()
  {
    if (timer > 0f)
    {
      timer -= Time.deltaTime;
    }
  }

  /// <summary>
  /// Event called when player enters in the car
  /// </summary>
  void OnCarEnter()
  {
    mainCamera.target = car;
  }

  /// <summary>
  /// Event called when player gets off the car
  /// </summary>
  /// <param name="t"></param>
  void OnCarExit(Transform t)
  {
    player.SetActive(true);
    player.transform.position = t.position;
    mainCamera.target = player;
  }

  /// <summary>
  /// Event called when player picks an item
  /// </summary>
  /// <param name="item"></param>
  void OnPickItem(InventoryItem item)
  {
    Debug.Log("Picked " + item.pickAmount + " units of " + item.name);
    inventory.insert(item);
  }

  /// <summary>
  /// Event called when player requests to toggle inventory window
  /// </summary>
  void OnInventoryToggle()
  {
    showInventory = !showInventory;
    inventoryWindow.SetActive(showInventory);
  }
}
