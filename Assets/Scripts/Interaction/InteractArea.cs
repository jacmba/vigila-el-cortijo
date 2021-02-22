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
        default:
          break;
      }
    }
  }
}
