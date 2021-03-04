using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Behavior of UI menu butytons
/// </summary>
public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
  }

  /// <summary>
  /// This function is called when the object becomes enabled and active.
  /// </summary>
  void OnEnable()
  {
    button.Select();
  }

  public void OnPointerEnter(PointerEventData data)
  {
    transform.localScale = overSize;
  }

  public void OnPointerExit(PointerEventData data)
  {
    transform.localScale = size;
  }
}
