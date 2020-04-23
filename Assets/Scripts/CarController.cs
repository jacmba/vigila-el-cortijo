using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
  public InputManager im;
  public List <Wheel> wheels;
  public List <WheelCollider> throttleWheels;
  public List <WheelCollider> steerWheels;
  public float strengthCoefficient = 20f;
  public float maxTurn = 20f;
  public float maxTorque = 80f;
  public GameObject cm;

  private GameController gameController;
  private Rigidbody body;

  void Start() {
    body = GetComponent<Rigidbody>();
    body.centerOfMass = cm.transform.localPosition;
    gameController = im.gameObject.GetComponent<GameController>(); 
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    foreach(WheelCollider wheel in throttleWheels) {
      wheel.motorTorque = maxTorque * im.throttle;
    }

    foreach(WheelCollider wheel in steerWheels) {
      wheel.steerAngle = maxTurn * im.steer;
    }

    if(im.a)  {
      body.velocity = Vector3.zero;
      gameController.DoAction("EXIT_CAR");
    }
  }

  void Update() {
    foreach(WheelCollider wheel in throttleWheels) {
      Vector3 pos = Vector3.zero;
      Quaternion rot = Quaternion.identity;

      wheel.GetWorldPose(out pos, out rot);
      wheel.transform.rotation = rot;
    }
  }
}
