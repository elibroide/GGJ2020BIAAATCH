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
        var myorder = 0;
        for (var i = 0; i < list.Count; i++)
        {
            var group = list[i].sprites.GroupBy(sprite => sprite.sortingOrder);
            foreach (var spriteGroup in group)
            {
                foreach (var sprite in spriteGroup)
                {
                    sprite.sortingOrder = myorder;
                }

                myorder++;
            }
        }
    }
}