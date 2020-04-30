﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArea : MonoBehaviour
{
  public GameController gameController;
  public string action;

  private InputManager im;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
      im = gameController.gameObject.GetComponent<InputManager>();
  }

  void OnTriggerStay(Collider other) {
    if(other.gameObject.tag == "Player" && im.a) {
      gameController.DoAction(action);
    }
  }
}