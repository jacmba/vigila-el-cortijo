using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatorroController : MonoBehaviour
{
  public GameObject cepaPrefab;
  public GameObject cepa;
  public GaugeBar Riego;
  public GaugeBar Health;
  public GameObject Gauges;
  public float DryTimer = 0f;
  public float DryRatio = 5f;
  public float DeathTimer = 0f;
  public float DeathRatio = 2f;
  public float CepaTimer = 0f;
  public float CepaRatio = 30f;

  private Transform spawn;
  private bool hasCepa;

  // Start is called before the first frame update
  void Start()
  {
      spawn = transform.GetChild(0);
      hasCepa = false;
  }

  // Update is called once per frame
  void Update()
  {
    if(Riego.Value > 0) {
      DryTimer += Time.deltaTime;
      if(DryTimer >= DryRatio) {
        DryTimer = 0f;
        Riego.Value--;
      }
      if(Riego.Value >= 75 && !hasCepa) {
        CepaTimer += Time.deltaTime;
        if(CepaTimer >= CepaRatio) {
          spawnCepa();
        }
      }
    } else {
      DeathTimer += Time.deltaTime;
      if(DeathTimer >= DeathRatio) {
        DeathTimer = 0f;
        Health.Value--;
      }
      if(Health.Value <= 0) {
        Destroy(gameObject);
      }
    }

    if(hasCepa && cepa == null) {
      vendimiar();
    }
  }

  private void spawnCepa() {
    if(hasCepa) {
      return;
    }
    cepa = Instantiate(cepaPrefab, spawn);
    Gauges.SetActive(false);
    hasCepa = true;
  }

  /// <summary>
  /// OnTriggerEnter is called when the Collider other enters the trigger.
  /// </summary>
  /// <param name="other">The other Collider involved in this collision.</param>
  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player" && !hasCepa) {
      Gauges.SetActive(true);
    }
  }

  /// <summary>
  /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
  /// </summary>
  /// <param name="other">The other Collider involved in this collision.</param>
  void OnTriggerExit(Collider other)
  {
    if(other.gameObject.tag == "Player") {
      Gauges.SetActive(false);
    }
  }

  private void vendimiar() {
    CepaTimer = 0f;
    hasCepa = false;
  }
}
