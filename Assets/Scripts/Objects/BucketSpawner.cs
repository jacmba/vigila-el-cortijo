using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the spawning of bucket item to pick water
/// </summary>
public class BucketSpawner : MonoBehaviour
{
  public GameObject bucketPrefab;
  private GameObject bucket;

  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start()
  {
    bucket = null;
    EventManager.bucketSpawn += OnBucketSpawn;
  }

  /// <summary>
  /// This function is called when the MonoBehaviour will be destroyed.
  /// </summary>
  void OnDestroy()
  {
    EventManager.bucketSpawn -= OnBucketSpawn;
  }

  /// <summary>
  /// Event triggerrd when bucket spawning is requested
  /// </summary>
  void OnBucketSpawn()
  {
    if (bucket == null)
    {
      bucket = GameObject.Instantiate(bucketPrefab, transform.position, Quaternion.identity);
      Debug.Log("Spawned water bucket");
    }
  }
}
