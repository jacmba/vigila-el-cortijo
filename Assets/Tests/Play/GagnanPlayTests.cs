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
      im = gagnan.AddComponent<InputManager>();
      controller = gagnan.GetComponent<PlayerController>();
      controller.im = im;
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
  }
}
