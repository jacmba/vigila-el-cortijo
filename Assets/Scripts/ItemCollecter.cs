using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollecter : MonoBehaviour
{
  private bool collecting = false;

  public void StartCollect()
  {
    collecting = true;
  }

  public void StopCollect()
  {
    collecting = false;
  }

  public bool IsCollecting()
  {
    return collecting;
  }
}
