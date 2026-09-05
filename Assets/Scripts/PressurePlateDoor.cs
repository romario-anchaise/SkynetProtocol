using UnityEngine;

public class PressurePlateDoor : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private Collider2D doorCollider;
    [SerializeField] private SpriteRenderer plateRenderer;
    [SerializeField] private float openHeight = 3.5f;
    [SerializeField] private float doorSpeed = 5f;

    private Vector3 closedPosition;
    private int cratesOnPlate;
    private GUIStyle messageStyle;

    public bool IsPressed => cratesOnPlate > 0;

    public void Configure(Transform doorTransform, Collider2D blockingCollider, SpriteRenderer renderer)
    {
        door = doorTransform;
        doorCollider = blockingCollider;
        plateRenderer = renderer;
    }

    private void Awake()
    {
        if (door) closedPosition = door.position;
        UpdatePlateColor();
    }

    private void Update()
    {
        if (!door) return;
        Vector3 target = closedPosition + (IsPressed ? Vector3.up * openHeight : Vector3.zero);
        door.position = Vector3.MoveTowards(door.position, target, doorSpeed * Time.deltaTime);

        if (doorCollider)
            doorCollider.enabled = door.position.y < closedPosition.y + openHeight * 0.82f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsCrate(other)) return;
        cratesOnPlate++;
        UpdatePlateColor();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsCrate(other)) return;
        cratesOnPlate = Mathf.Max(0, cratesOnPlate - 1);
        UpdatePlateColor();
    }

    private static bool IsCrate(Collider2D other)
    {
        return other.attachedRigidbody && other.attachedRigidbody.gameObject.name.Contains("PushCrate");
    }

    private void UpdatePlateColor()
    {
        if (plateRenderer)
            plateRenderer.color = IsPressed ? new Color(0.05f, 1f, 0.55f) : new Color(0.95f, 0.22f, 0.18f);
    }

    private void OnGUI()
    {
        if (messageStyle == null)
        {
            messageStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = Mathf.Max(16, Screen.height / 38),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
        }

        messageStyle.normal.textColor = IsPressed ? new Color(0.15f, 1f, 0.65f) : Color.white;
        string message = IsPressed ? "ACCESO CONCEDIDO - PUERTA ABIERTA" : "EMPUJA LA CAJA SOBRE LA PLACA";
        GUI.Label(new Rect(0, 34, Screen.width, 42), message, messageStyle);
        GUI.Label(new Rect(0, Screen.height - 48, Screen.width, 32), "R  REINICIAR NIVEL", messageStyle);
    }
}
