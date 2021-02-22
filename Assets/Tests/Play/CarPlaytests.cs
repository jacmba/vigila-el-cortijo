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

      GameObject playerPref = Resources.Load<GameObject>("Prefabs/Gañan");
      GameObject player = GameObject.Instantiate(playerPref);
      PlayerController pc = player.GetComponent<PlayerController>();
      pc.im = player.AddComponent<MockInput>();

      GameObject camObj = new GameObject();
      Camera cam = camObj.AddComponent<Camera>();

      GameObject gcPref = Resources.Load<GameObject>("Prefabs/GameController");
      GameObject gc = GameObject.Instantiate(gcPref);
      GameController gameController = gc.GetComponent<GameController>();

      InteractArea ia = terra.GetComponentInChildren<InteractArea>();
      ia.gameController = gameController;

      gameIm = gc.AddComponent<MockInput>();
      gameController.mainCamera = camObj.AddComponent<CameraController>();
      gameController.car = terra;
      gameController.player = player;

      gameController.mainCamera.target = player;

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
    public IEnumerator CarPlayTestsWithEnumeratorPasses()
    {
      WheelCollider anyWheel = terra.GetComponentInChildren<WheelCollider>();

      yield return null;

      Assert.IsNotNull(terra, "Terra object should not be null");
      Assert.IsNotNull(body, "Rigidbody should not be null");
      Assert.IsNotNull(controller, "Car controller should not be null");
      Assert.IsNotNull(anyWheel, "Wheel should not be null");

      Assert.AreEqual(700, body.mass, "Seat Terra should weight 700 kgs");
      Assert.IsTrue(controller.enabled, "Car controller should be enabled");

      im.throttle = 1f;

      yield return new WaitForSeconds(1f);

      Assert.LessOrEqual(body.velocity.magnitude, 0.1f, "Car speed should be 0");
      Assert.AreEqual(1000f, anyWheel.brakeTorque, "Max braking force should be applied");
    }

    [UnityTest]
    public IEnumerator CarPlayTestsShouldMoveWhenCharacterEnters()
    {
      WheelCollider anyWheel = terra.GetComponentInChildren<WheelCollider>();
      EventManager.OnCarEnter();

      yield return null;

      im.throttle = 1f;

      yield return new WaitForSeconds(1f);

      Assert.GreaterOrEqual(body.velocity.magnitude, 1f, "Car speed should be at least 1");
      Assert.AreEqual(0, anyWheel.brakeTorque, "Brake force should be 0");
    }
  }
}
