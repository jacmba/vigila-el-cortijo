using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
  public class ItemSlotTests
  {
    private ItemSlotController slot;

    [SetUp]
    public void Setup()
    {
      GameObject s = GameObject.Instantiate(Resources.Load("Prefabs/ItemSlot") as GameObject);
      slot = s.GetComponent<ItemSlotController>();
    }

    [UnityTest]
    public IEnumerator ItemSlotTestsObjectBasicCheck()
    {
      yield return null;

      Assert.IsNotNull(slot);
      Assert.IsFalse(slot.infoNode.activeSelf);
    }
  }
}
