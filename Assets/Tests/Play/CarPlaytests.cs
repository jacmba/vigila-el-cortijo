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
    private Vector3 initPos;
    private Quaternion initRot;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      GameObject terraPref = Resources.Load<GameObject>("Prefabs/Terra");
      terra = GameObject.Instantiate(terraPref);
      body = terra.GetComponent<Rigidbody>();
      initPos = terra.transform.position;
      initRot = terra.transform.rotation;
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
      im.a = false;

      gameIm.a = false;

      terra.transform.position = initPos;
      terra.transform.rotation = initRot;
      body.velocity = Vector3.zero;
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

      yield return new WaitForSeconds(5f);

      Assert.GreaterOrEqual(body.velocity.magnitude, 1f, "Car speed should be at least 1");
      Assert.AreEqual(0, anyWheel.brakeTorque, "Brake force should be 0");

      im.a = true;

      yield return null;
    }

    [UnityTest]
    public IEnumerator CarPlayTestsShouldApplyBrakeForceWhenApplyOppsoiteThrottle()
    {
      WheelCollider anyWheel = terra.GetComponentInChildren<WheelCollider>();
      EventManager.OnCarEnter();

      yield return null;

      im.throttle = 1f;

      yield return new WaitForSeconds(2f);

      float preSpeed = controller.getSpeed();
      im.throttle = -1f;

      yield return new WaitForSeconds(.1f);

      float postSpeed = controller.getSpeed();
      Debug.Log(postSpeed);

      Assert.GreaterOrEqual(anyWheel.brakeTorque, 1f, "Braking force should be at least 1");
      Assert.Less(postSpeed, preSpeed, "Speed after braking should be smaller after applying brakes");
      Assert.AreEqual(Direction.FORWARD, controller.getDirection(), "Car direction should be forward");

      im.a = true;

      yield return null;
    }

    [UnityTest]
    public IEnumerator CarPlayTestsShouldContinueMovingForwardAfterCrashStop()
    {
      WheelCollider anyWheel = terra.GetComponentInChildren<WheelCollider>();
      EventManager.OnCarEnter();

      yield return null;

      im.throttle = 1f;

      yield return new WaitForSeconds(1f);

      body.velocity = Vector3.zero;

      yield return new WaitForSeconds(2f);

      Assert.GreaterOrEqual(controller.getSpeed(), 1, "Car speed should be at least 1");
      Assert.Less(anyWheel.brakeTorque, 0.001, "Break force should be zero");

      im.a = true;

      yield return null;
    }

    [UnityTest]
    public IEnumerator CarPlayTestsShouldThrowSmokeWhenRunning()
    {
      GameObject smoke = terra.transform.Find("Escape").gameObject;
      EventManager.OnCarEnter();

      yield return new WaitForSeconds(.1f);

      Assert.IsTrue(smoke.activeSelf, "Smoke should be active when Gañán on the car");

      im.a = true;

      yield return new WaitForSeconds(.1f);

      Assert.IsFalse(smoke.activeSelf, "Smoke should not be active when Gañán got off the car");
    }

    [UnityTest]
    public IEnumerator CarPlayTestsShouldDisplayBrakingLight()
    {
      Transform rootLights = terra.transform.Find("Lights");
      GameObject lights = rootLights.Find("BrakeLights").gameObject;
      EventManager.OnCarEnter();

      yield return null;

      im.throttle = 1f;

      yield return new WaitForSeconds(2f);

      im.throttle = -1f;

      yield return new WaitForSeconds(.1f);

      Assert.IsTrue(lights.activeSelf, "Lights should be on while braking");

      im.throttle = 0.00f;

      yield return new WaitForSeconds(.1f);

      Assert.IsFalse(lights.activeSelf, "Lights should be off when stopped braking");

      im.throttle = -1;

      yield return new WaitForSeconds(.1f);

      im.a = true;

      yield return new WaitForSeconds(.1f);

      Assert.IsFalse(lights.activeSelf, "Lights should be off when Gañán leaving the car");
    }
  }
}
