using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
  public class EventManagerTests
  {
    private bool called;

    [SetUp]
    public void Setup()
    {
      called = false;
    }

    // A Test behaves as an ordinary method
    [Test]
    public void EventManagerTestsCheckSingletonInstance()
    {
      EventManager em = EventManager.getInstance();
      Assert.IsNotNull(em, "EventManager singleton instance should not be null");
    }

    [Test]
    public void EventManagerTestsSimpleEvent()
    {
      EventManager em = EventManager.getInstance();
      em.carEnter += OnCall;

      Assert.IsFalse(called, "Event should have not been called yet");

      em.OnCarEnter();
      Assert.IsTrue(called, "Event should have been called now");
    }

    private void OnCall()
    {
      called = true;
    }
  }
}
