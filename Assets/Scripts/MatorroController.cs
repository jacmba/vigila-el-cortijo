using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatorroController : MonoBehaviour
{
  public GameObject cepaPrefab;

  private Transform spawn;

  // Start is called before the first frame update
  void Start()
  {
      spawn = transform.GetChild(0);
      Instantiate(cepaPrefab, spawn);
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
