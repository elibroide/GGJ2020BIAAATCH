using System;
using UnityEngine;

[ExecuteInEditMode]
public class WorldBounds : MonoBehaviour
{
    public float width;
    public float height;

    public Transform top;
    public Transform bot;
    public Transform left;
    public Transform right;

    public void Awake()
    {
        top.position = new Vector3(0, height/2);
        bot.position = new Vector3(0, -height/2);
        left.position = new Vector3(-width/2, 0);
        right.position = new Vector3(width/2, 0);
    }
}