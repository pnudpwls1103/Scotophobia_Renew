using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Item
[System.Serializable]
public class ItemInfo
{
    public string itemName;
    public bool check;
    public string imagePath;
    public string infoName;
    public string infoDescription;
}

[System.Serializable]
public class ItemInfoData : ILoader<string, ItemInfo>
{
    public List<ItemInfo> items = new List<ItemInfo>();

    public Dictionary<string, ItemInfo> MakeDict() 
	{
		Dictionary<string, ItemInfo> dict = new Dictionary<string, ItemInfo>();
		foreach (ItemInfo item in items)
			dict.Add(item.itemName, item);
		return dict;
	} 
}

#endregion