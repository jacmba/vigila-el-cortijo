using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
  public float throttle;
  public float steer;
  public float h;
  public float v;
  public bool a;
  public bool b;

  // Update is called once per frame
  void Update()
  {
    throttle = Input.GetAxis("Vertical");
    steer = Input.GetAxis("Horizontal");

    h = Input.GetAxis("Horizontal");
    v = Input.GetAxis("Vertical");

    a = Input.GetButton("Fire1");
    b = Input.GetButton("Fire2");
  }
}
