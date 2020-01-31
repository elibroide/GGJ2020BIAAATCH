using UnityEngine;

public class ItemOfLayers : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public bool isAuto = false;

    [ReadOnly] public OrderOfLayers parent;

    void Awake()
    {
        parent = FindObjectOfType<OrderOfLayers>();
        parent.list.Add(this);

        if (isAuto)
        {
            sprites = GetComponentsInChildren<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (isAuto)
        {
            sprites = GetComponentsInChildren<SpriteRenderer>();
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