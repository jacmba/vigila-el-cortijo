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
      Assert.AreEqual(0, slot.item.count());
      Assert.IsFalse(slot.infoNode.activeSelf);
    }

    [UnityTest]
    public IEnumerator ItemSlotTestsCheckHasCepaAndIsEnabled()
    {
      InventoryItem item = new InventoryItem();
      item.type = ItemType.CEPA;
      item.maxAmount = 100;

      Sprite sprite = Resources.Load<Sprite>("Sprites/cepa");
      item.icon = sprite;

      item.increase(4);

      slot.item = item;

      yield return null;

      Assert.AreEqual(sprite, slot.icon.sprite);
      Assert.AreEqual("x4", slot.count.text);
      Assert.IsTrue(slot.infoNode.activeSelf);
    }

    [UnityTest]
    public IEnumerator ItemSlotTestsInfoNodeShouldDisabledWhenItemsAreZero()
    {
      InventoryItem item = new InventoryItem();
      item.maxAmount = 100;

      Sprite sprite = Resources.Load<Sprite>("Sprites/cepa");
      item.icon = sprite;

      item.increase(1);

      slot.item = item;

      yield return null;

      Assert.IsTrue(slot.infoNode.activeSelf);

      item.clear();

      yield return null;

      Assert.IsFalse(slot.infoNode.activeSelf);
    }
  }
}
