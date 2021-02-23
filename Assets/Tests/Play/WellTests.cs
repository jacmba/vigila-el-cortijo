using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
  public class WellTests
  {
    private GameObject pozo;
    private GameObject pozoPref;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      pozoPref = Resources.Load<GameObject>("Prefabs/pozo");

      GameObject camera = new GameObject();
      camera.AddComponent<Camera>();
      camera.transform.Translate(Vector3.back * 10 + Vector3.up * 3);
    }

    [SetUp]
    public void Setup()
    {
      pozo = GameObject.Instantiate(pozoPref);
      pozo.transform.position = Vector3.zero;
    }

    [TearDown]
    public void TearDown()
    {
      GameObject.Destroy(pozo);
    }

    [UnityTest]
    public IEnumerator WellTestsWithEnumeratorPasses()
    {
      yield return null;

      Assert.NotNull(pozo, "Pozo instance should not be null");
    }

    [UnityTest]
    public IEnumerator WellTestsRopeShouldFollowBucket()
    {
      Transform bucket = pozo.transform.Find("bucket");
      LineRenderer rope = pozo.GetComponentInChildren<LineRenderer>();

      Vector3 pos = new Vector3(10.000000f, 10.000000f, 10.000000f);
      bucket.position = pos;

      yield return null;

      Assert.IsTrue(pos == rope.GetPosition(1), "Rope bottom edge should be at {10, 10, 10}");
    }
  }
}
