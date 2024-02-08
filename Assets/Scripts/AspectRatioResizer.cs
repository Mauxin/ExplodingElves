using Extensions;
using UnityEngine;

public class AspectRatioResizer : MonoBehaviour
{
    [SerializeField] private Vector3 _minScale;
    [SerializeField] private Vector3 _maxScale;

    private void Start()
    {
        transform.localScale = Camera.main.AspectRatioPercentage(_minScale, _maxScale);
    }
}
