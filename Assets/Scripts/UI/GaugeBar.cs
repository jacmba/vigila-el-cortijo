using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeBar : MonoBehaviour
{
  public float Value = 100f;

  private RectTransform fore;

  // Start is called before the first frame update
  void Start()
  {
    fore = transform.GetChild(1).gameObject.GetComponent<RectTransform>();
  }

  // Update is called once per frame
  void Update()
  {
    //Clamp the value
    if (Value < 0f)
    {
      Value = 0f;
    }
    if (Value > 100f)
    {
      Value = 100f;
    }

    float size = Value / 100f;
    fore.localScale = new Vector3(size, 1, 1);
  }
}
