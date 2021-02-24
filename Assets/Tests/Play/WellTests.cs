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
    private GameObject gc;
    private GameObject gcPref;
    private MockInput im;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      pozoPref = Resources.Load<GameObject>("Prefabs/pozo");
      gcPref = Resources.Load<GameObject>("Prefabs/GameController");

      GameObject camera = new GameObject();
      camera.AddComponent<Camera>();
      camera.transform.Translate(Vector3.back * 10 + Vector3.up * 3);
    }

    [SetUp]
    public void Setup()
    {
      pozo = GameObject.Instantiate(pozoPref);
      pozo.transform.position = Vector3.zero;

      gc = GameObject.Instantiate(gcPref);
      GameController gameController = gc.GetComponent<GameController>();
      im = gc.AddComponent<MockInput>();

      InteractArea ia = pozo.GetComponentInChildren<InteractArea>();
      ia.gameController = gameController;
    }

    [TearDown]
    public void TearDown()
    {
      GameObject.Destroy(pozo);
      GameObject.Destroy(gc);
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
      Transform bucket = pozo.transform.Find("Bucket");
      LineRenderer rope = pozo.GetComponentInChildren<LineRenderer>();

      Vector3 pos = new Vector3(10.000000f, 10.000000f, 10.000000f);
      bucket.position = pos;

      yield return null;

      Assert.IsTrue(pos == rope.GetPosition(1), "Rope bottom edge should be at {10, 10, 10}");
    }

    [UnityTest]
    public IEnumerator WellTestsInteractionShouldRotateTheTorno()
    {
      Transform well = pozo.transform.Find("Well");
      Transform torno = well.Find("Torno");
      Vector3 initRot = torno.transform.rotation.eulerAngles;

      EventManager.OnWellOperate();

      yield return new WaitForSeconds(.5f);

      Assert.Greater(torno.transform.rotation.eulerAngles.x, initRot.x, "Torno should rotate in X axis");
    }

    [UnityTest]
    public IEnumerator WellTestsInteractionShouldLowerBucket()
    {
      Transform bucket = pozo.transform.Find("Bucket");
      Vector3 initPos = bucket.position;

      EventManager.OnWellOperate();

      yield return new WaitForSeconds(.5f);

      Assert.Less(bucket.position.y, initPos.y, "Bucket height should lower when operating well");
    }
  }
}
