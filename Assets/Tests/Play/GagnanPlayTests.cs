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
    private IInputManager im;
    private PlayerController controller;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      gagnan = GameObject.Instantiate(Resources.Load("Prefabs/Gañan") as GameObject);
      im = gagnan.AddComponent<MockInput>();
      controller = gagnan.GetComponent<PlayerController>();
      controller.im = im;
    }

    [SetUp]
    public void Setup()
    {
      im.v = 0f;
      im.h = 0f;
      im.a = false;
      im.b = false;
      im.select = false;
      im.steer = 0f;
      im.throttle = 0f;
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

      Assert.IsTrue(animator.GetCurrentAnimatorStateInfo(0).IsName("vendimia"));
      Assert.AreEqual(initPos.x, gagnan.transform.position.x);
      Assert.AreEqual(initPos.z, gagnan.transform.position.z);
      Assert.IsFalse(animator.GetBool("running"));
    }

    [UnityTest]
    public IEnumerator TestGagnanPlayGagnanShouldNotRotateWhileCrhouching()
    {
      Quaternion initRot = gagnan.transform.rotation;
      im.b = true;

      yield return null;

      im.h = 1f;

      yield return new WaitForSeconds(.5f);

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
  }
}
