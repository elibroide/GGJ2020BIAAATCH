using DG.Tweening;
using UnityEngine;

public class BodyPartPickup : MonoBehaviour
{
    public BodyPartData data;

    public float hoverUp = 0.25f;
    public float hoverUpDuration = 0.5f;
    
    private float _gracePeriod = 5f;
    private float _timePassed = 0;

    void Start()
    {
        // transform.DOLocalMove(Vector3.up * hoverUp, hoverUpDuration).SetLoops(-1, LoopType.Yoyo);
    }

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

    public void Init(BodyPartData createData)
    {
        data = createData;
        // sprite.sprite = createData.parent.bodyPartPickup;
    }
}
