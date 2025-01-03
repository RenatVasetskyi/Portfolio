using UnityEngine;

namespace Code.Gaming
{
    public class Fish : MonoBehaviour
    {
        public int Prize;
        public int Weight;

        public bool OnHook { get; private set; }

        public void Move(Transform to)
        {
            LeanTween.move(gameObject, to.position, Random.Range(3f, 5f))
                .setEase(LeanTweenType.linear)
                .setOnComplete(() => Destroy(gameObject));
        }

        public void TakeOnHook(Transform hook)
        {
            OnHook = true;
            LeanTween.cancel(gameObject);
            transform.SetParent(hook);
            LeanTween.rotate(gameObject, new Vector3(0, 0, -90), 0.15f);
            LeanTween.moveLocal(gameObject, Vector3.zero, 0.15f);
        }
    }
}