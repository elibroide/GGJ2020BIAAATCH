using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemOfLayers : MonoBehaviour
{
    public List<SpriteRenderer> sprites;
    public bool isAuto = false;

    [ReadOnly] public OrderOfLayers parent;

    void Awake()
    {
        parent = FindObjectOfType<OrderOfLayers>();
        parent.list.Add(this);

        if (isAuto)
        {
            sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
        }
        else
        {
            if (sprites.Count > 0 && sprites[0] != null)
            {
                var sprite = sprites[0];
                if (sprite.transform.parent == transform.parent || sprite.transform == transform.parent)
                {
                    return;
                }
            }
            
            sprites.Clear();
            sprites.Add(GetComponentInParent<SpriteRenderer>());
        }
    }

    void Update()
    {
        if (isAuto)
        {
            sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
        }
    }

    private void OnDestroy()
    {
        if (parent != null)
        {
            parent.list.Remove(this);
        }
    }
}