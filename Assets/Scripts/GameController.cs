using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public GameObject player;

  public GameObject car;

  public CameraController mainCamera;

  public float MaxTimer = 3f;

  private float timer;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    timer = 0f;
    EventManager.carEnter += OnCarEnter;
    EventManager.carExit += OnCarExit;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventManager.carEnter -= OnCarEnter;
    EventManager.carExit -= OnCarExit;
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
}
