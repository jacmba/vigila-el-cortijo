using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
  public class InventoryWindowTests
  {
    private GameObject controller;
    private GameController gameController;
    private InventoryWindowController inventoryWindowController;

    private InventoryManager inventory;

    [SetUp]
    public void Setup()
    {
      controller = GameObject.Instantiate(Resources.Load("Prefabs/GameController") as GameObject);
      gameController = controller.GetComponent<GameController>();
      inventoryWindowController = controller.GetComponent<InventoryWindowController>();

      inventory = InventoryManager.getInstance();
      inventory.slots = 1;
    }

    [UnityTest]
    public IEnumerator InventoryWindowTestsWithEnumeratorPasses()
    {
      yield return null;
      Assert.IsNotNull(controller);
      Assert.IsNotNull(gameController);
      Assert.IsNotNull(inventoryWindowController);
      Assert.AreEqual(inventoryWindowController.slots.Length, inventory.slots);
    }

    [UnityTest]
    public IEnumerator InventoryWindowTestsFirstElementShouldBeACepa()
    {
      InventoryItem item = new InventoryItem();
      item.type = ItemType.CEPA;
      item.maxAmount = 100;
      item.pickAmount = 1;
      item.icon = Resources.Load<Sprite>("Sprites/cepa");

      inventory.insert(item);

      yield return null;

      GameObject inventoryWindow = controller.transform.Find("InventoryWindow").gameObject;
      inventoryWindow.SetActive(true);

      yield return null;

      ItemSlotController slot1 = inventoryWindowController.slots[0];
      Assert.AreEqual(ItemType.CEPA, slot1.item.type);
      Assert.AreEqual(1, slot1.item.count());
      Assert.IsTrue(slot1.infoNode.activeSelf);
      Assert.AreEqual(item.icon, slot1.icon.sprite);
      Assert.AreEqual("x1", slot1.count.text);
    }
  }
}
