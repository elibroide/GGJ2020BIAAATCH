using UnityEngine;
using System.Collections;

public class ResourcesManage: MonoBehaviour
{
    static GameObject hand;
    // Use this for initialization
    void Start()
    {
        hand = Resources.Load("hand") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
