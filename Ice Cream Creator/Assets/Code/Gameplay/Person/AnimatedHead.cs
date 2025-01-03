using UnityEngine;

namespace Code.Gameplay.Person
{
    public class AnimatedHead : MonoBehaviour
    {
        private const float MoveVerticalDuration = 1f;
        private const float RotateHorizontalDuration = 1.2f;
        
        private const int MinMoveVerticalDistance = 2;
        private const int MaxMoveVerticalDistance = 12;
        private const int MinRotateHorizontal = 1;
        private const int MaxRotateHorizontal = 5;
        
        private void Awake()
        {
            MoveVertical();
            Rotate();
        }

        private void MoveVertical()
        {
            LeanTween.moveLocal(gameObject, transform.localPosition + new Vector3(0, Random.Range
                    (MinMoveVerticalDistance, MaxMoveVerticalDistance), 0), MoveVerticalDuration)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() =>
                {
                    LeanTween.moveLocal(gameObject, transform.localPosition - new Vector3(0, Random.Range
                                (MinMoveVerticalDistance, MaxMoveVerticalDistance), 0),
                            MoveVerticalDuration)
                        .setEase(LeanTweenType.linear)
                        .setOnComplete(MoveVertical);
                });
        }

        private void Rotate()
        {
            LeanTween.rotateLocal(gameObject, new Vector3(0, 0, Random.Range
                    (MinRotateHorizontal, MaxRotateHorizontal)), RotateHorizontalDuration)
                .setEase(LeanTweenType.linear)
                .setOnComplete(() =>
                {
                    LeanTween.rotateLocal(gameObject, new Vector3(0, 0, 0), RotateHorizontalDuration)
                        .setEase(LeanTweenType.linear)
                        .setOnComplete(() =>
                        {
                            LeanTween.rotateLocal(gameObject, new Vector3(0, 0, -Random.Range
                                    (MinRotateHorizontal, MaxRotateHorizontal)), RotateHorizontalDuration)
                                .setEase(LeanTweenType.linear)
                                .setDelay(0.75f)
                                .setOnComplete(Rotate);
                        });
                });
        }
    }
}