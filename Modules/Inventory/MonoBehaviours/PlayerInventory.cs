using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
	private List<int> keys = new List<int>();

    // TODO: Please remove this when debugging done.
    [System.Serializable]
    public class KeyInfo
    {
        public int Id;
    }
    public List<KeyInfo> currentKeys;

    private void Awake()
    {
        currentKeys = new List<KeyInfo>();
    }

    public bool HasKey(int keyID)
	{
		return keys.Contains(keyID);
	}

	public void AddKey(int keyID)
	{
		keys.Add(keyID);
        currentKeys.Add(new KeyInfo() { Id = keyID });
    }

	public void RemoveKey(int keyID)
	{
		keys.Remove(keyID);
        currentKeys.RemoveAll(x => x.Id == keyID);
    }
}
