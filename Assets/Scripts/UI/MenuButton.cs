using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Behavior of UI menu butytons
/// </summary>
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
  ISelectHandler, IDeselectHandler
{
  [SerializeField]
  private MenuButtonType type;
  private Vector3 size;
  private Vector3 overSize;
  private Vector3 clickSize;
  private Button button;

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
  }

  public void OnPointerEnter(PointerEventData data)
  {
    button.Select();
  }

  public void OnPointerExit(PointerEventData data)
  {
    OnDeselect(data as BaseEventData);
  }

  public void OnSelect(BaseEventData data)
  {
    transform.localScale = overSize;
  }

  public void OnDeselect(BaseEventData data)
  {
    transform.localScale = size;
  }
}
