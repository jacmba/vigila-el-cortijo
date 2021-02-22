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
  /// Maximum braking force
  /// </summary>
  public float maxBrake = 500f;

  /// <summary>
  /// Center of mass reference object
  /// </summary>
  public GameObject cm;

  private Rigidbody body;
  private Transform entry;
  private bool canExit;
  private bool parked;
  private Direction direction;
  private float speed;

  private static readonly float ZERO = 0.000000000000f;

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
    direction = Direction.NEUTRAL;

    EventManager.carEnter += OnCarEnter;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventManager.carEnter -= OnCarEnter;
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void FixedUpdate()
  {
    Vector2 speedV = new Vector2(body.velocity.x, body.velocity.z);
    speed = speedV.magnitude;

    if (!parked)
    {
      if (direction == Direction.NEUTRAL)
      {
        if (im.throttle > .1f)
        {
          direction = Direction.FORWARD;
        }
        else if (im.throttle < .1f)
        {
          direction = Direction.REAR;
        }
      }
      else if (speed < 0.1f)
      {
        direction = Direction.NEUTRAL;
      }
      foreach (Wheel wheel in wheels)
      {
        WheelCollider w = wheel.Object.GetComponent<WheelCollider>();

        if (Mathf.Abs(im.throttle) < .1f)
        {
          w.brakeTorque = ZERO;
        }
        else if (direction == Direction.FORWARD && im.throttle < ZERO)
        {
          w.brakeTorque += -im.throttle;
        }
        else if (direction == Direction.REAR && im.throttle > ZERO)
        {
          w.brakeTorque += im.throttle;
        }

        if (w.brakeTorque > maxBrake)
        {
          w.brakeTorque = maxBrake;
        }

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
        parked = true;
        direction = Direction.NEUTRAL;
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
    else // Apply parking brake force when character is out of the car
    {
      foreach (Wheel wheel in wheels)
      {
        WheelCollider w = wheel.Object.GetComponent<WheelCollider>();
        w.brakeTorque = 1000f;
      }
    }
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update()
  {
    if (!parked)
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
  }

  /// <summary>
  /// Request activation of enter area after 1 second
  /// </summary>
  /// <returns></returns>
  IEnumerator deferEnable()
  {
    yield return new WaitForSeconds(1);
    entry.gameObject.SetActive(true);
  }

  /// <summary>
  /// Event triggered when the character jumps into the car
  /// </summary>
  public void OnCarEnter()
  {
    parked = false;
  }

  /// <summary>
  /// Get movement direction
  /// </summary>
  /// <returns>Direction</returns>
  public Direction getDirection()
  {
    return direction;
  }
}
