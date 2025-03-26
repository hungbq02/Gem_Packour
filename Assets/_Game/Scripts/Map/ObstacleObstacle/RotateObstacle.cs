using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class RotateObstacle : MonoBehaviour
{
    public enum RotationMode
    {
        Infinite,
        Once
    }

    [SerializeField] private RotationMode rotationMode = RotationMode.Infinite;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float rotationSpeed = 45f;
    [SerializeField] private float rotationAngle = 90f;
    [SerializeField] private float duration = 1f;

    private void Update()
    {
        if (rotationMode == RotationMode.Infinite)
        {
            RotateInfinite();
        }
    }
    public void RotateInfinite()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

    public void RotateOnce()
    {
        transform.DORotate(transform.eulerAngles + rotationAxis * rotationAngle, duration)
            .SetEase(Ease.Linear);
    }
}
