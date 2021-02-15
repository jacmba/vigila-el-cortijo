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
      mgr.insert(ItemType.CEPA, 5);
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
      mgr.insert(ItemType.CEPA, 2);
      mgr.insert(ItemType.WINE, 5);
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
      mgr.insert(ItemType.CEPA, 1);
      mgr.insert(ItemType.CEPA, 3);

      Assert.AreEqual(9, mgr.getAvailSlots());
      Assert.AreEqual(4, mgr.count(ItemType.CEPA));
    }

    [Test]
    public void InventoryTestShouldNotAddNegativeValues()
    {
      InventoryManager mgr = new InventoryManager();
      mgr.insert(ItemType.CEPA, -10);
      Assert.AreEqual(10, mgr.getAvailSlots());
      Assert.AreEqual(0, mgr.count(ItemType.CEPA));
    }

    [Test]
    public void InventoryTestShoudNotUseNegativeAmounts()
    {
      InventoryManager mgr = new InventoryManager();
      mgr.insert(ItemType.CEPA, 10);
      int used = mgr.use(ItemType.CEPA, -1);
      Assert.AreEqual(0, used);
      Assert.AreEqual(9, mgr.getAvailSlots());
      Assert.AreEqual(10, mgr.count(ItemType.CEPA));
    }
  }
}
