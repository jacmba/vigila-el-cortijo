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

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      gagnan = GameObject.Instantiate(Resources.Load("Prefabs/Gañan") as GameObject);
      var im = gagnan.AddComponent<InputManager>();
      var controller = gagnan.GetComponent<PlayerController>();
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
  }
}
