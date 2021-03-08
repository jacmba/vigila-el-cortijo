using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player main script class
/// </summary>
public class PlayerController : MonoBehaviour
{
  public IInputManager im;
  public float speed = 1f;
  public float rotationSpeed = 15f;

  private Animator animator;
  private CameraHook camHook;
  private ItemCollecter collecter;
  private EventManager eventManager;
  private bool canToggleInv;
  private bool rotating;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    animator = GetComponent<Animator>();
    camHook = GetComponentInChildren<CameraHook>();
    collecter = GetComponent<ItemCollecter>();

    eventManager = EventManager.getInstance();
    eventManager.carEnter += OnCarEnter;

    canToggleInv = true;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    eventManager.carEnter -= OnCarEnter;
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void FixedUpdate()
  {
    if (im.v > 0.1f)
    {
      if (!collecter.IsCollecting())
      {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        animator.SetBool("running", true);
        camHook.follow = true;
      }
    }
    else if (im.v < -0.1f)
    {
      if (!collecter.IsCollecting())
      {
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        animator.SetBool("running", true);
        camHook.follow = true;
      }
    }
    else
    {
      animator.SetBool("running", false);
    }

    if ((im.h > 0.1f || im.h < -0.1f) && !collecter.IsCollecting())
    {
      transform.Rotate(Vector3.up * rotationSpeed * im.h * Time.deltaTime);
      rotating = true;
    }
    else
    {
      rotating = false;
    }
  }

  /// <summary>
  /// Update is called every frame, if the MonoBehaviour is enabled.
  /// </summary>
  void Update()
  {
    if (im.b && !collecter.IsCollecting() && !animator.GetBool("running") && !rotating)
    {
      collecter.StartCollect();
      animator.SetBool("running", false);
      animator.SetTrigger("collect");
    }

    if (canToggleInv && im.select)
    {
      eventManager.OnInventoryToggle();
      canToggleInv = false;
    }
    else if (!im.select)
    {
      canToggleInv = true;
    }
  }

  /// <summary>
  /// Event triggered when the character enters in vehicle
  /// </summary>
  void OnCarEnter()
  {
    gameObject.SetActive(false);
  }
}
