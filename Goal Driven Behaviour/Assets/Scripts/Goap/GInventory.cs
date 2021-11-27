using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GInventory
{
    private List<GameObject> _items = new List<GameObject>();
    public void AddItem(GameObject i)
    {
        _items.Add(i);
    }
    public GameObject FindItemWithTag(string tag)
    {
        foreach (GameObject i in _items)
        {
            if(i?.tag == tag)
                return i;
        }
        return null;
    }
    public void DeleteItem(GameObject i)
    {
        _items.Remove(i);
    }
}
