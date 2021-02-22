using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to handle vehicle control
/// </summary>
public class CarController : MonoBehaviour
{
  /// <summary>
  /// User input reference
  /// </summary>
  public IInputManager im;

  /// <summary>
  /// List of physic wheels
  /// </summary>
  public List<Wheel> wheels;

  /// <summary>
  /// Currently not used
  /// </summary>
  public float strengthCoefficient = 20f;

  /// <summary>
  /// Maximum steering
  /// </summary>
  /// 
  public float maxTurn = 20f;

  /// <summary>
  /// Maximum engine power
  /// </summary>
  public float maxTorque = 80f;

  /// <summary>
  /// Center of mass reference object
  /// </summary>
  public GameObject cm;

  private Rigidbody body;
  private Transform entry;
  private bool canExit;
  private bool parked;

  private bool canToggle;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    body = GetComponent<Rigidbody>();
    body.centerOfMass = cm.transform.localPosition;
    entry = transform.Find("Entry");
    canExit = false;
    parked = true;
    canToggle = true;
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void FixedUpdate()
  {
    if (!parked)
    {
      foreach (Wheel wheel in wheels)
      {
        WheelCollider w = wheel.Object.GetComponent<WheelCollider>();
        if (wheel.Transmision)
        {
          w.motorTorque = maxTorque * im.throttle;
        }
        if (wheel.Direction)
        {
          w.steerAngle = maxTurn * im.steer;
        }
      }

      // Exit the car
      if (im.a && canExit)
      {
        body.velocity = Vector3.zero;
        EventManager.OnCarExit(entry);
        StartCoroutine(deferEnable());
        canExit = false;
        enabled = false;
      }
      else if (!im.a)
      {
        canExit = true;
      }

      if (im.select && canToggle)
      {
        EventManager.OnInventoryToggle();
        canToggle = false;
      }
      else if (!im.select)
      {
        canToggle = true;
      }
    }
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update()
  {
    foreach (Wheel wheel in wheels)
    {
      Vector3 pos = Vector3.zero;
      Quaternion rot = Quaternion.identity;

      WheelCollider col = wheel.Object.GetComponent<WheelCollider>();
      Transform vis = col.transform.GetChild(0);

      col.GetWorldPose(out pos, out rot);
      vis.rotation = rot;
    }
  }

  IEnumerator deferEnable()
  {
    yield return new WaitForSeconds(1);
    entry.gameObject.SetActive(true);
  }
}
