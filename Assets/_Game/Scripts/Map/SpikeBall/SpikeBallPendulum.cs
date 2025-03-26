using DG.Tweening;
using UnityEngine;

public class SpikeBallPendulum : MonoBehaviour
{
    [SerializeField] private float swingAngle = 45f; 
    [SerializeField] private float duration = 2f;
    [SerializeField] private Vector3 rotationAxis = Vector3.forward;

    private void Start()
    {
        StartPendulum();
    }

    private void StartPendulum()
    {
        Vector3 startRotation = transform.eulerAngles - rotationAxis * swingAngle;
        Vector3 endRotation = transform.eulerAngles + rotationAxis * swingAngle;

        transform.DORotate(endRotation, duration / 2, RotateMode.Fast)
            .SetEase(Ease.InOutSine) 
            .SetLoops(-1, LoopType.Yoyo) 
            .From(startRotation); 
    }
}
