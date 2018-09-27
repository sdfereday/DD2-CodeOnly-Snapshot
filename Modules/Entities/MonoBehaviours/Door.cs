using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Door : MonoBehaviour, IInteractible
{
    public bool IsLocked { get; private set; }
    public int RequiresKeyId;

    private List<Door> Partners;

    private void Start()
    {
        IsLocked = true;
    }

    private void OnEnable()
    {
        Mapper.OnMapGenerated += FindPartners;
    }

    private void OnDisable()
    {
        Mapper.OnMapGenerated -= FindPartners;
    }

    private void FindPartners(Mapper.MapResult result)
    {
        Partners = result.InstanceCache
            .Select(x => x.GetComponent<Door>())
            .Where(x => x != null && x.RequiresKeyId == RequiresKeyId)
            .ToList();
    }

    public void Use(Collider other)
    {
        var inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null && inventory.HasKey(RequiresKeyId))
        {
            inventory.RemoveKey(RequiresKeyId);           

            IsLocked = false;
            gameObject.SetActive(false);

            Partners.ForEach(x => x.IsLocked = false);
            Partners.ForEach(x => x.gameObject.SetActive(false));
        }
    }
}