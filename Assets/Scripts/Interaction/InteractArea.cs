using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArea : MonoBehaviour
{
  public GameController gameController;
  public AreaAction action;

  private IInputManager im;
  private EventManager eventManager;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    im = gameController.gameObject.GetComponent<IInputManager>();

    GameObject mobile = gameController.transform.Find("MobileControls").gameObject;
    if (mobile != null && mobile.activeSelf)
    {
      im = mobile.GetComponent<MobileInputManager>();
    }

    eventManager = EventManager.getInstance();
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
          eventManager.OnCarEnter();
          gameObject.SetActive(false);
          break;
        case AreaAction.WELL_OPERATE:
          eventManager.OnWellOperate();
          break;
        default:
          break;
      }
    }
  }
}
