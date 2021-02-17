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
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
      SceneManager.LoadScene("World");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestGagnanPlayWithEnumeratorPasses()
    {
      // Use the Assert class to test conditions.
      // Use yield to skip a frame.
      yield return new WaitForSeconds(0.1f);

      GameObject g = GameObject.Find("Gañan");

      Assert.IsNotNull(g);
      Assert.AreEqual("Player", g.tag);

      Rigidbody body = g.GetComponent<Rigidbody>();
      Assert.AreEqual(90, body.mass);
    }

    [UnityTest]
    public IEnumerator TestGagnanPlayGagnanShouldWeigh90Kilos()
    {
      // Use the Assert class to test conditions.
      // Use yield to skip a frame.
      yield return new WaitForSeconds(0.1f);

      GameObject g = GameObject.Find("Gañan");

      Rigidbody body = g.GetComponent<Rigidbody>();
      Assert.AreEqual(90, body.mass);
    }
  }
}
