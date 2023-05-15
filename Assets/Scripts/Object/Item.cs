using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IInteraction
{
    public void Interact(GameObject gameObject)
    {
        Inventory.Instance.Insert(gameObject.name);
    }
}
