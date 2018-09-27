using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public KeyItemType ItemType;
        public Sprite InventorySprite;
    }

    public List<InventoryItem> ItemTypes;

    private Sprite GetPrefab(KeyItemType Type)
    {
        return ItemTypes.Find(x => x.ItemType == Type)
            .InventorySprite;
    }

    // Note: Items will probably best in a more persistent place, however that will require
    // saving data to scriptable objects or other means. For now however, this works accordingly.
    // It's a quick and simple way to get an inventory up and running. Have a look at Corgi Engines
    // inventory system in the future, it probably does it better.
    public Image[] itemImages = new Image[numItemSlots];
    public UIInventoryItem[] items = new UIInventoryItem[numItemSlots];
    //public KeyItem[] items = new KeyItem[numItemSlots];

    public const int numItemSlots = 1;

    public bool HasItemType(KeyItemType type)
    {
        return items != null && items
            .Any(x => x != null && x.ItemType == type);
    }

    public bool HasItem(int Id)
    {
        return items != null && items
            .Any(x => x != null && x.Id == Id);
    }

    public void AddItem(int itemId, KeyItemType itemType)
    {
        /* To avoid passing lots of pointless banana, an id and a type will be passed.
         * Depending on what that type is will depend on the prefab to use for that item
         * in the actual inventory. So somewhere there should be a sort of DB that'll map
         * the type of the item to the assigned inventory prefab. For now I'll just stick
         * it in here. Naturally.
         * The pickup in the meantime is long gone having transfered its id to the inventory.
         */
        if(HasItem(itemId))
        {
            return;
        }

        // So we create a new instance of a key item type to hold on to.
        var itemToAdd = new UIInventoryItem()
        {
            ItemType = itemType,
            Id = itemId,
            ItemSprite = GetPrefab(itemType)
        };

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.ItemSprite;
                itemImages[i].enabled = true;
                return;
            }
        }
    }

    public void RemoveItem (int itemId)
    {
        if (!HasItem(itemId))
        {
            return;
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Id == itemId)
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
                return;
            }
        }
    }
}
