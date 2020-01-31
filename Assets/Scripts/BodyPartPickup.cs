using UnityEngine;

public class BodyPartPickup : MonoBehaviour
{
    public BodyPartData data;

    private float _gracePeriod = 5f;
    private float _timePassed = 0;

    void Update()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed < _gracePeriod)
        {
            return;
        }

        data.Decompose(Time.deltaTime);
    }

    public void PickedUp()
    {
        Destroy(gameObject);
    }
}
