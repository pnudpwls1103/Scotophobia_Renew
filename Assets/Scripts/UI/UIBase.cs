using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UIBase : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        _objects[type] = new UnityEngine.Object[names.Length];
        for (int i = 0; i < names.Length; i++)
            foreach (T com in GetComponentsInChildren<T>())
                if (com.name == names[i])
                    _objects[type][i] = com;
    }

    protected void Bind(Type type) // GameObject
    {
        string[] strs = Enum.GetNames(type);
        _objects[type] = new UnityEngine.Object[strs.Length];
        for (int i = 0; i < strs.Length; i++)
        {
            GameObject go = GameObject.Find(strs[i]);
            if (go == null)
                Debug.Log($"{strs[i]} GameObject is not exist");
            _objects[type][i] = go;
        }
    }
}