using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;

public class InventoryUI : MonoBehaviour
{
    public HUDManager hud;
    public PlayerInventory inventory;
    public ClipInfo plantSFX;
    public ClipInfo craftSFX;
    public List<InventorySlotUI> Slots = new List<InventorySlotUI>();
    public Dictionary<GameIngredientInfo, InventorySlotUI> SlotsDict = new Dictionary<GameIngredientInfo, InventorySlotUI>();
    public Queue<InventorySlotUI> SlotsQueue = new Queue<InventorySlotUI>();

    private void Awake()
    {
        Slots.ForEach(slot =>
        {
            slot.inventory = this;
            SlotsQueue.Enqueue(slot);
            slot.gameObject.SetActive(false);
        });

        inventory.OnAddItem += (ingredient, amount) =>
        {
            if (SlotsDict.ContainsKey(ingredient))
            {
                SlotsDict[ingredient].UpdateAmount(inventory.Amount(ingredient));
            }
            else
            {
                var slot = SlotsQueue.Dequeue();
                slot.gameObject.SetActive(true);
                SlotsDict.Add(ingredient, slot);
                slot.Set(ingredient, inventory.Amount(ingredient));
            }
        };

        inventory.OnRemoveItem += (ingredient) =>
        {
            int amount = inventory.Amount(ingredient);

            if (amount == 0)
            {
                var slot = SlotsDict[ingredient];
                SlotsDict.Remove(ingredient);
                SlotsQueue.Enqueue(slot);
                slot.Clear();
                slot.gameObject.SetActive(false);
            }
            else
            {
                SlotsDict[ingredient].UpdateAmount(amount);
            }
        };
    }
}