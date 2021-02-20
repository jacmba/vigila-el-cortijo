using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
  public class Simpletest
  {
    // A Test behaves as an ordinary method
    [Test]
    public void SimpletestSimplePasses()
    {
      // Use the Assert class to test conditions
      Assert.IsTrue(true);
    }
  }
}
