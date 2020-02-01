using System;
using UnityEngine;

[ExecuteInEditMode]
public class WorldBounds : MonoBehaviour
{
    public float topY;
    public float botY;
    public float leftX;
    public float rightX;

    public Transform top;
    public Transform bot;
    public Transform left;
    public Transform right;

    public void Awake()
    {
        top.position = new Vector3(0, topY);
        bot.position = new Vector3(0, botY);
        left.position = new Vector3(leftX, 0);
        right.position = new Vector3(rightX, 0);
    }
}