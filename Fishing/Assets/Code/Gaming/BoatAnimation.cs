using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Gaming
{
    public class BoatAnimation : MonoBehaviour
    {
        private const float MinMoveUp = 0.05f;
        private const float MaxMoveUp = 0.2f;
        
        private const float MinMoveDown = 0.2f;
        private const float MaxMoveDown = 0.4f;

        private void Awake()
        {
            MoveUp();
            RotateZ();
        }

        private void MoveUp()
        {
            Vector3 currentPosition = transform.position;
            Vector3 pointToMove = currentPosition + new Vector3(0, Random.Range(MinMoveUp, MaxMoveUp));
            
            LeanTween.move(gameObject, pointToMove, Random.Range(0.7f, 1.5f))
                .setEaseLinear()
                .setOnComplete(() =>
                {
                    LeanTween.move(gameObject, currentPosition, Random.Range(0.7f, 1.5f))
                        .setEaseLinear()
                        .setOnComplete(MoveDown);
                });
        }

        private void MoveDown()
        {
            Vector3 currentPosition = transform.position;
            Vector3 pointToMove = currentPosition - new Vector3(0, Random.Range(MinMoveDown, MaxMoveDown));
            
            LeanTween.move(gameObject, pointToMove, Random.Range(0.7f, 1.5f))
                .setEaseLinear()
                .setOnComplete(() =>
                {
                    LeanTween.move(gameObject, currentPosition, Random.Range(0.7f, 1.5f))
                        .setEaseLinear()
                        .setOnComplete(MoveUp);
                });
        }

        private void RotateZ()
        {
            float randomRotation = Random.Range(0f, 5f);
            
            LeanTween.rotateZ(gameObject, randomRotation, Random.Range(1.5f, 4f))
                .setEaseLinear()
                .setOnComplete(() =>
                {
                    float randomRotation = Random.Range(0f, 5f);
                    
                    LeanTween.rotateZ(gameObject, -randomRotation, Random.Range(1.5f, 4f))
                        .setEaseLinear()
                        .setOnComplete(RotateZ);
                });
        }
    }
}