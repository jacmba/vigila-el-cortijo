using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArea : MonoBehaviour
{
  public GameController gameController;
  public AreaAction action;

  private IInputManager im;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    im = gameController.gameObject.GetComponent<IInputManager>();
  }

  /// <summary>
  /// Event called when an object enters a trigger collider
  /// </summary>
  /// <param name="other">Object that entered the trigger collider</param>
  void OnTriggerStay(Collider other)
  {
    if (other.gameObject.tag == "Player" && im.a)
    {
      switch (action)
      {
        case AreaAction.CAR_ENTER:
          EventManager.OnCarEnter();
          gameObject.SetActive(false);
          break;
        case AreaAction.WELL_OPERATE:
          EventManager.OnWellOperate();
          break;
        default:
          break;
      }
    }
  }
}
