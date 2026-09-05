using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RobotVisualAnimator : MonoBehaviour
{
    [SerializeField] private float wheelRotationSpeed = 180f;
    [SerializeField] private float bobFrequency = 12f;
    [SerializeField] private float bobAmount = 0.035f;
    [SerializeField] private float armSwing = 8f;

    private Rigidbody2D body;
    private Transform visual;
    private Transform head;
    private Transform chassis;
    private Transform arm;
    private Transform wheelRear;
    private Transform wheelFront;
    private SpriteRenderer eye;

    private Vector3 headStart;
    private Vector3 chassisStart;
    private Quaternion armStart;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        visual = transform.Find("Visual");
        head = visual.Find("Head");
        chassis = visual.Find("Chassis");
        arm = visual.Find("Arm");
        wheelRear = visual.Find("WheelRear");
        wheelFront = visual.Find("WheelFront");
        eye = visual.Find("Eye").GetComponent<SpriteRenderer>();

        headStart = head.localPosition;
        chassisStart = chassis.localPosition;
        armStart = arm.localRotation;
    }

    private void Update()
    {
        float speed = Mathf.Abs(body.linearVelocity.x);
        bool moving = speed > 0.1f;

        if (moving)
        {
            float direction = Mathf.Sign(body.linearVelocity.x) * Mathf.Sign(transform.localScale.x);
            float rotation = -direction * speed * wheelRotationSpeed * Time.deltaTime;
            wheelRear.Rotate(0f, 0f, rotation);
            wheelFront.Rotate(0f, 0f, rotation);

            float cycle = Time.time * bobFrequency;
            float bob = Mathf.Sin(cycle) * bobAmount;
            head.localPosition = headStart + Vector3.up * bob;
            chassis.localPosition = chassisStart + Vector3.up * bob * 0.5f;
            arm.localRotation = armStart * Quaternion.Euler(0f, 0f, Mathf.Sin(cycle) * armSwing);
        }
        else
        {
            head.localPosition = Vector3.Lerp(head.localPosition, headStart, Time.deltaTime * 10f);
            chassis.localPosition = Vector3.Lerp(chassis.localPosition, chassisStart, Time.deltaTime * 10f);
            arm.localRotation = Quaternion.Slerp(arm.localRotation, armStart, Time.deltaTime * 10f);
        }

        float airborneTilt = Mathf.Clamp(-body.linearVelocity.y * 1.2f, -8f, 8f);
        visual.localRotation = Quaternion.Lerp(
            visual.localRotation,
            Quaternion.Euler(0f, 0f, airborneTilt),
            Time.deltaTime * 6f
        );

        float glow = 0.8f + Mathf.Sin(Time.time * 3f) * 0.2f;
        eye.color = new Color(0f, 1f, 0.65f) * glow;
    }
}
