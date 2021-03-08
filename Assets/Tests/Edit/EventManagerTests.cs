using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
  public class EventManagerTests
  {
    // A Test behaves as an ordinary method
    [Test]
    public void EventManagerTestsCheckSingletonInstance()
    {
      EventManager em = EventManager.getInstance();
      Assert.IsNotNull(em, "EventManager singleton instance should not be null");
    }
  }
}
