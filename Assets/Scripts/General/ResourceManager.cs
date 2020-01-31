using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static Dictionary<string, GameObject> resources = new Dictionary<string, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        resources["grave"] = Resources.Load<GameObject>("grave");
    }

    // Update is called once per frame
    public static GameObject GetGameObject(string key)
    {
        return resources[key];
    }
}
