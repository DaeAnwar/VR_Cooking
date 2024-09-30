using UnityEngine;
using DG.Tweening;

public class MainArrow : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public float EndValue; 
    void OnEnable()
    {
        // Store original position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // Call the animation function
        AnimateArrow();
    }

    void AnimateArrow()
    {
        Sequence arrowSequence = DOTween.Sequence();

        // Move the arrow up
        arrowSequence.Append(transform.DOLocalMoveY(EndValue, 4f).SetEase(Ease.Linear));

        // Rotate the arrow around the Y-axis
        arrowSequence.Join(transform.DOLocalRotate(new Vector3(0f, 360f, 0f), 3f, RotateMode.WorldAxisAdd));

        // Set the loop type to Yoyo to make the animation repeat back and forth
        arrowSequence.SetLoops(-1, LoopType.Yoyo);

       Destroy(gameObject, 11f);

    }

   
}
