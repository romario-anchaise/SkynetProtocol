using UnityEngine;

public class EnvironmentPulse : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float amount = 0.08f;
    [SerializeField] private bool rotate;

    private Vector3 originalScale;
    private float phase;

    private void Awake()
    {
        originalScale = transform.localScale;
        phase = Mathf.Abs(GetInstanceID() % 100) * 0.1f;
    }

    private void Update()
    {
        float pulse = 1f + Mathf.Sin(Time.time * speed + phase) * amount;
        transform.localScale = originalScale * pulse;

        if (rotate)
            transform.Rotate(0f, 0f, 45f * Time.deltaTime);
    }
}
