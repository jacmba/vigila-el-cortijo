using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public GameObject player;

  public GameObject car;

  public CameraController mainCamera;

  public float MaxTimer = 3f;

  public InventoryManager inventory;

  private float timer;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    timer = 0f;

    // Setup event observers
    EventManager.carEnter += OnCarEnter;
    EventManager.carExit += OnCarExit;
    EventManager.pickItem += OnPickItem;

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

  void OnCarEnter()
  {
    car.GetComponent<CarController>().enabled = true;
    mainCamera.target = car;
  }

  void OnCarExit(Transform t)
  {
    player.SetActive(true);
    player.transform.position = t.position;
    mainCamera.target = player;
  }

  void OnPickItem(InventoryItem item)
  {
    Debug.Log("Picked " + item.pickAmount + " units of " + item.type);
    inventory.insert(item);
  }
}
