using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
  public class PauseTests
  {
    private EventManager em;
    private bool called;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
      em = EventManager.getInstance();
    }

    [SetUp]
    public void Setup()
    {
      called = false;
    }

    // A Test behaves as an ordinary method
    [Test]
    public void PauseTestsSimplePasses()
    {
      // Use the Assert class to test conditions
    }
  }
}
