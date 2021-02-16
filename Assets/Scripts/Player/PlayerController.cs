using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public InputManager im;
  public float speed = 1f;
  public float rotationSpeed = 15f;

  private Animator animator;
  private CameraHook camHook;
  private ItemCollecter collecter;
  private bool canToggleInv;
  // Start is called before the first frame update
  void Start()
  {
    animator = GetComponent<Animator>();
    camHook = GetComponentInChildren<CameraHook>();
    collecter = GetComponent<ItemCollecter>();

    EventManager.carEnter += OnCarEnter;

    canToggleInv = true;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventManager.carEnter -= OnCarEnter;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (im.v > 0.1f)
    {
      transform.Translate(Vector3.forward * speed * Time.deltaTime);
      animator.SetBool("running", true);
      camHook.follow = true;
    }
    else if (im.v < -0.1f)
    {
      transform.Translate(Vector3.forward * -speed * Time.deltaTime);
      animator.SetBool("running", true);
      camHook.follow = true;
    }
    else
    {
      animator.SetBool("running", false);
    }

    if (im.h > 0.1f || im.h < -0.1f)
    {
      transform.Rotate(Vector3.up * rotationSpeed * im.h * Time.deltaTime);
    }
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update()
  {
    if (im.b && !collecter.IsCollecting())
    {
      collecter.StartCollect();
      animator.SetTrigger("collect");
    }

    if (canToggleInv && im.select)
    {
      EventManager.OnInventoryToggle();
      canToggleInv = false;
    }
    else if (!im.select)
    {
      canToggleInv = true;
    }
  }

  void OnCarEnter()
  {
    gameObject.SetActive(false);
  }
}
