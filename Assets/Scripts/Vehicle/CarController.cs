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
  private GameObject escape;
  private GameObject brakeLights;

  private EventManager eventManager;

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
    escape = transform.Find("Escape").gameObject;
    Transform lights = transform.Find("Lights");
    brakeLights = lights.Find("BrakeLights").gameObject;
    canExit = false;
    parked = true;
    canToggle = true;
    direction = Direction.NEUTRAL;

    eventManager = EventManager.getInstance();
    eventManager.carEnter += OnCarEnter;
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
    Vector3 relV = transform.InverseTransformDirection(body.velocity);
    speed = relV.z;

    if (!parked)
    {
      bool braking = false;

      if (speed > 0.001)
      {
        direction = Direction.FORWARD;
      }
      else if (speed < -0.001)
      {
        direction = Direction.REAR;
      }
      else
      {
        direction = Direction.NEUTRAL;
      }

      if ((direction == Direction.FORWARD && im.throttle < -.1f) ||
      (direction == Direction.REAR && im.throttle > .1f))
      {
        braking = true;
      }

      brakeLights.SetActive(braking);

      foreach (Wheel wheel in wheels)
      {
        WheelCollider w = wheel.Object.GetComponent<WheelCollider>();

        if (braking)
        {
          w.brakeTorque += Mathf.Abs(im.throttle) * 10;
        }
        else
        {
          w.brakeTorque = ZERO;
        }

        if (w.brakeTorque > maxBrake)
        {
          w.brakeTorque = maxBrake;
        }

        if (wheel.Transmision && direction != Direction.NEUTRAL)
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
        eventManager.OnCarExit(entry);
        StartCoroutine(deferEnable());
        canExit = false;
        parked = true;
        direction = Direction.NEUTRAL;
        escape.SetActive(false);
        brakeLights.SetActive(false);
      }
      else if (!im.a)
      {
        canExit = true;
      }

      if (im.select && canToggle)
      {
        eventManager.OnInventoryToggle();
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
        vis.position = pos;
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
    escape.SetActive(true);
  }

  /// <summary>
  /// Get movement direction
  /// </summary>
  /// <returns>Direction</returns>
  public Direction getDirection()
  {
    return direction;
  }

  /// <summary>
  /// Get car speed
  /// </summary>
  /// <returns>float</returns>
  public float getSpeed()
  {
    return speed;
  }
}
