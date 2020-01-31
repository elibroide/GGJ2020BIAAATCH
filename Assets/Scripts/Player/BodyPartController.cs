using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class BodyPartController : MonoBehaviour
    {
        public BodyPartData data;

        public void Rot()
        {
            var fx = GetComponentInChildren<SpriteRenderer>().gameObject.AddComponent<_2dxFX_DesintegrationFX>();
            fx.Seed = Random.value;
            fx.Desintegration = 0;
            fx._Color = Color.green;
            DOVirtual.Float(0, 1, 2, number => fx.Desintegration = number);
        }
    }
}