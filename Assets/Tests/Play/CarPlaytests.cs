using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
  public class CarPlaytests
  {
    private GameObject terra;
    private Rigidbody body;
    private CarController controller;
    private MockInput im;
    private MockInput gameIm;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      GameObject terraPref = Resources.Load<GameObject>("Prefabs/Terra");
      terra = GameObject.Instantiate(terraPref);
      body = terra.GetComponent<Rigidbody>();
      controller = terra.GetComponent<CarController>();
      im = terra.AddComponent<MockInput>();
      controller.im = im;

      GameObject gcPref = Resources.Load<GameObject>("Prefabs/GameController");
      GameObject gc = GameObject.Instantiate(gcPref);
      GameController gameController = gc.GetComponent<GameController>();
      gameIm = gc.AddComponent<MockInput>();

      InteractArea ia = terra.GetComponentInChildren<InteractArea>();
      ia.gameController = gameController;

      GameObject cortiPref = Resources.Load<GameObject>("Prefabs/cortijo");
      GameObject cortijo = GameObject.Instantiate(cortiPref);
    }

    [SetUp]
    public void Setup()
    {
      im.throttle = 0f;
      im.steer = 0f;

      gameIm.a = false;
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CarPlaytestsWithEnumeratorPasses()
    {
      yield return null;

      Assert.IsNotNull(terra, "Terra object should not be null");
      Assert.IsNotNull(body, "Rigidbody should not be null");
      Assert.IsNotNull(controller, "Car controller should not be null");

      Assert.AreEqual(700, body.mass, "Seat Terra should weight 700 kgs");
      Assert.IsTrue(controller.enabled, "Car controller should be enabled");

      im.throttle = 1f;

      yield return new WaitForSeconds(1f);

      Assert.LessOrEqual(body.velocity.magnitude, 0.01f, "Car speed should be 0");
    }
  }
}
