using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderOfLayers : MonoBehaviour
{
    public List<ItemOfLayers> list;

    void Update()
    {
        list.Sort((t1, t2) => t2.transform.position.y < t1.transform.position.y ? -1 : 1);
        for (var i = 0; i < transform.childCount; i++)
        {
            foreach (var sprite in list[i].sprites)
            {
                sprite.sortingOrder = i;
            }
        }
    }
}