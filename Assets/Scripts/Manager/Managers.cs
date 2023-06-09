using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instace;  
    public static Managers Instance { get { Init(); return s_instace; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instace == null)
        {
            GameObject obj = GameObject.Find("@Managers");
            if (obj == null)
            {
                obj = new GameObject { name = "@Managers" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);
            s_instace = obj.GetComponent<Managers>();
            s_instace._data.Init();
        }
    }
}
