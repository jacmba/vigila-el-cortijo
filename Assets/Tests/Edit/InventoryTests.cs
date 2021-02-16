using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
  public class InventoryTests
  {
    // A Test behaves as an ordinary method
    [Test]
    public void InventoryTestsCheckObjectBuild()
    {
      InventoryManager mgr = new InventoryManager();
      Assert.IsNotNull(mgr);
      Assert.AreEqual(mgr.slots, 10);
    }

    [Test]
    public void InventoryTestsCheckSimpleInventoryItemType()
    {
      InventoryItem item = new InventoryItem();
      item.maxAmount = 100;
      Assert.AreEqual(0, item.count());
      item.increase(5);
      Assert.AreEqual(5, item.count());
    }

    [Test]
    public void InventoryTestsItemCountShouldNotOvershootMaxAmount()
    {
      InventoryItem item = new InventoryItem();
      item.maxAmount = 1;
      item.increase(10);
      Assert.AreEqual(1, item.count());
    }

    [Test]
    public void InventoryTestsCheckAvailableSlots()
    {
      InventoryManager mgr = new InventoryManager();
      mgr.slots = 3;
      int avail = mgr.getAvailSlots();
      Assert.AreEqual(3, avail);
    }

    [Test]
    public void InventoryTestsCheckSlotInsert()
    {
      InventoryManager mgr = new InventoryManager();
      InventoryItem i = new InventoryItem();
      i.type = ItemType.CEPA;
      i.pickAmount = 5;
      i.maxAmount = 100;
      mgr.insert(i);
      int avail = mgr.getAvailSlots();
      int cepas = mgr.count(ItemType.CEPA);
      Assert.AreEqual(9, avail);
      Assert.AreEqual(5, cepas);
    }

    [Test]
    public void InventoryTestsCheckNonExistingItemShouldHaveZero()
    {
      InventoryManager mgr = new InventoryManager();
      int waters = mgr.count(ItemType.WATER);
      Assert.AreEqual(0, waters);
    }

    [Test]
    public void InventoryTestsCheckUseItem()
    {
      InventoryManager mgr = new InventoryManager();
      mgr.slots = 2;

      InventoryItem ci = new InventoryItem();
      ci.maxAmount = 100;
      ci.pickAmount = 2;
      ci.type = ItemType.CEPA;
      InventoryItem wi = new InventoryItem();
      wi.maxAmount = 100;
      wi.pickAmount = 5;
      wi.type = ItemType.WINE;
      mgr.insert(ci);
      mgr.insert(wi);
      Assert.AreEqual(0, mgr.getAvailSlots());

      int used = mgr.use(ItemType.CEPA, 1);
      Assert.AreEqual(1, used);
      Assert.AreEqual(0, mgr.getAvailSlots());
      Assert.AreEqual(1, mgr.count(ItemType.CEPA));

      used = mgr.use(ItemType.CEPA, 3);
      Assert.AreEqual(1, used);
      Assert.AreEqual(1, mgr.getAvailSlots());
      Assert.AreEqual(0, mgr.count(ItemType.CEPA));

      used = mgr.use(ItemType.CEPA, 1);
      Assert.AreEqual(0, used);
    }

    [Test]
    public void InventoryTestInsertExistingShouldSum()
    {
      InventoryManager mgr = new InventoryManager();
      InventoryItem item = new InventoryItem();
      item.maxAmount = 100;
      item.pickAmount = 1;
      item.type = ItemType.CEPA;
      mgr.insert(item);
      item.pickAmount = 3;
      mgr.insert(item);

      Assert.AreEqual(9, mgr.getAvailSlots());
      Assert.AreEqual(4, mgr.count(ItemType.CEPA));
    }

    [Test]
    public void InventoryTestShouldNotAddNegativeValues()
    {
      InventoryManager mgr = new InventoryManager();
      InventoryItem item = new InventoryItem();
      item.maxAmount = 100;
      item.pickAmount = -10;
      item.type = ItemType.CEPA;
      mgr.insert(item);
      Assert.AreEqual(10, mgr.getAvailSlots());
      Assert.AreEqual(0, mgr.count(ItemType.CEPA));
    }

    [Test]
    public void InventoryTestShoudNotUseNegativeAmounts()
    {
      InventoryManager mgr = new InventoryManager();
      InventoryItem item = new InventoryItem();
      item.maxAmount = 100;
      item.pickAmount = 10;
      item.type = ItemType.CEPA;
      mgr.insert(item);
      int used = mgr.use(ItemType.CEPA, -1);
      Assert.AreEqual(0, used);
      Assert.AreEqual(9, mgr.getAvailSlots());
      Assert.AreEqual(10, mgr.count(ItemType.CEPA));
    }
  }
}
