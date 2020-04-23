using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public GameObject target;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    Transform hook = target.transform.Find("CamHook");
    CameraHook h = hook.gameObject.GetComponent<CameraHook>();
    float dist = (hook.position - transform.position).magnitude;
    if(h.follow && dist > 0f) {
      Vector3 pos = Vector3.Lerp(transform.position, hook.position, Time.deltaTime);
      transform.position = pos;
    } else {
      h.follow = false;
    }

    transform.LookAt(target.transform.position + (Vector3.up * 1.5f));
  }
}
