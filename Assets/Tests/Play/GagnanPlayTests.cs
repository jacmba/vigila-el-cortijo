using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
  public class TestGagnanPlay
  {
    private GameObject gagnan;
    private GameObject gagnanPref;
    private GameObject camera;
    private IInputManager im;
    private PlayerController controller;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      gagnanPref = Resources.Load<GameObject>("Prefabs/Gañan");
    }

    [SetUp]
    public void Setup()
    {
      gagnan = GameObject.Instantiate(gagnanPref);
      im = gagnan.AddComponent<MockInput>();
      controller = gagnan.GetComponent<PlayerController>();
      controller.im = im;

      camera = new GameObject();
      Camera cam = camera.AddComponent<Camera>();
      CameraController cc = camera.AddComponent<CameraController>();
      cc.target = gagnan;
    }

    [TearDown]
    public void TearDown()
    {
      GameObject.Destroy(gagnan);
      GameObject.Destroy(camera);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestGagnanPlayWithEnumeratorPasses()
    {
      // Use the Assert class to test conditions.
      // Use yield to skip a frame.
      yield return null;

      Assert.IsNotNull(gagnan);
      Assert.AreEqual("Player", gagnan.tag);
    }

    [UnityTest]
    public IEnumerator TestGagnanPlayGagnanShouldWeigh90Kilos()
    {
      // Use the Assert class to test conditions.
      // Use yield to skip a frame.
      yield return null;
      Rigidbody body = gagnan.GetComponent<Rigidbody>();
      Assert.AreEqual(90, body.mass);
    }

    [UnityTest]
    public IEnumerator TestGagnanPlayGagnanShouldNotMoveWhileCrouching()
    {
      Animator animator = gagnan.GetComponent<Animator>();
      Vector3 initPos = gagnan.transform.position;
      im.b = true;

      yield return null;

      im.v = 1f;

      yield return new WaitForSeconds(.5f);

      Vector3 delta = initPos - gagnan.transform.position;
      delta.y = 0f;
      float dist = Mathf.Abs(delta.magnitude);

      Assert.IsTrue(animator.GetCurrentAnimatorStateInfo(0).IsName("vendimia"), "Current animation state should be 'vendimia'");
      Assert.Less(dist, 1f, "Position delta should be less than 1");
      Assert.IsFalse(animator.GetBool("running"), "Animator should not be running");
    }

    [UnityTest]
    public IEnumerator TestGagnanPlayGagnanShouldNotRotateWhileCrouching()
    {
      Quaternion initRot = gagnan.transform.rotation;
      im.b = true;

      yield return null;

      im.h = 1f;

      yield return new WaitForSeconds(.1f);

      Assert.AreEqual(initRot, gagnan.transform.rotation);
    }

    [UnityTest]
    public IEnumerator TestGagnanShouldNotStartVendimiaWhileMoving()
    {
      Animator animator = gagnan.GetComponent<Animator>();
      im.v = 1f;

      yield return new WaitForSeconds(.5f);

      im.b = true;

      yield return null;

      Assert.IsTrue(animator.GetCurrentAnimatorStateInfo(0).IsName("run"));
      Assert.IsTrue(animator.GetBool("running"));
    }

    [UnityTest]
    public IEnumerator TestGagnanShouldNotStartVendimiaWhileRotating()
    {
      Animator animator = gagnan.GetComponent<Animator>();
      Quaternion initRot = gagnan.transform.rotation;
      im.h = 1f;

      yield return new WaitForSeconds(.5f);

      im.b = true;

      yield return new WaitForSeconds(.5f);

      Assert.IsFalse(animator.GetCurrentAnimatorStateInfo(0).IsName("vendimia"));
      Assert.AreNotEqual(initRot, gagnan.transform.rotation);
    }
  }
}
