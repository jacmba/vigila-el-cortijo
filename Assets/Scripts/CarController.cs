using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
  public InputManager im;
  public List<Wheel> wheels;
  public float strengthCoefficient = 20f;
  public float maxTurn = 20f;
  public float maxTorque = 80f;
  public GameObject cm;

  private GameController gameController;
  private Rigidbody body;
  private Transform entry;
  private bool canExit;

  void Start()
  {
    body = GetComponent<Rigidbody>();
    body.centerOfMass = cm.transform.localPosition;
    gameController = im.gameObject.GetComponent<GameController>();
    entry = transform.Find("Entry");
    canExit = false;
  }

  // Update is called once per frame
  void FixedUpdate()
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

    if (im.a && canExit)
    {
      body.velocity = Vector3.zero;
      EventManager.OnCarExit();
      canExit = false;
      enabled = false;
    }
    else
    {
      if (!im.a)
      {
        canExit = true;
      }
    }
  }

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
}
