using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<string, ItemInfo> homeworkItemDict { get; private set; } = new Dictionary<string, ItemInfo>();
    public Dictionary<string, ItemInfo> abuseItemDict { get; private set; } = new Dictionary<string, ItemInfo>();
    public Dictionary<string, ItemInfo> pictureItemDict { get; private set; } = new Dictionary<string, ItemInfo>();

    public void Init()
    {
        homeworkItemDict = LoadJson<ItemInfoData, string, ItemInfo>("homeworkItem").MakeDict();
        abuseItemDict = LoadJson<ItemInfoData, string, ItemInfo>("abuseItem").MakeDict();
        pictureItemDict = LoadJson<ItemInfoData, string, ItemInfo>("pictureItem").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}


