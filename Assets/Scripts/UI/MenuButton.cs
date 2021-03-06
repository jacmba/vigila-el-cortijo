using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Behavior of UI menu butytons
/// </summary>
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
  ISelectHandler, IDeselectHandler, IPointerClickHandler
{
  [SerializeField]
  private MenuButtonType type;
  private Vector3 size;
  private Vector3 overSize;
  private Vector3 clickSize;
  private Button button;
  private bool selected;

  /// <summary>
  /// Start is called on the frame when a script is enabled just before
  /// any of the Update methods is called the first time.
  /// </summary>
  void Start()
  {
    size = transform.localScale;
    overSize = size * 1.5f;
    clickSize = size * 2f;
    button = GetComponent<Button>();
    if (type == MenuButtonType.RESUME)
    {
      button.Select();
    }

    EventManager.pauseAPressed += OnPauseAPressed;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventManager.pauseAPressed -= OnPauseAPressed;
  }

  /// <summary>
  /// This function is called when the object becomes enabled and active.
  /// </summary>
  void OnEnable()
  {
    if (type == MenuButtonType.RESUME && button != null)
    {
      button.Select();
    }
  }

  /// <summary>
  /// Event triggered when pointer is over the button
  /// </summary>
  /// <param name="data">Event info</param>
  public void OnPointerEnter(PointerEventData data)
  {
    button.Select();
  }

  /// <summary>
  /// Event triggered when pointer is getting out of the button
  /// </summary>
  /// <param name="data">Event info</param>
  public void OnPointerExit(PointerEventData data)
  {
    OnDeselect(data as BaseEventData);
  }

  /// <summary>
  /// Event triggered when UI navigation selects the button
  /// </summary>
  /// <param name="data">Event info</param>
  public void OnSelect(BaseEventData data)
  {
    transform.localScale = overSize;
    selected = true;
  }

  /// <summary>
  /// Event triggered when UI navigation selects another element
  /// </summary>
  /// <param name="data">Event info</param>
  public void OnDeselect(BaseEventData data)
  {
    transform.localScale = size;
    selected = false;
  }

  /// <summary>
  /// Event tiggered when the button is clicked
  /// </summary>
  /// <param name="data">Event info</param>
  public void OnPointerClick(PointerEventData data)
  {
    switch (type)
    {
      case MenuButtonType.RESUME:
        EventManager.OnResume();
        break;
      case MenuButtonType.EXIT:
        EventManager.OnExitGame();
        break;
      default:
        Debug.Log("Unknown button pressed");
        break;
    }
  }

  /// <summary>
  /// Event triggered when A button is pressed during pause
  /// </summary>
  public void OnPauseAPressed()
  {
    if (selected)
    {
      OnPointerClick(null);
    }
  }
}
