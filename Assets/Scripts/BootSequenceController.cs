using System.Collections;
using UnityEngine;

public class BootSequenceController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private RobotVisualAnimator visualAnimator;
    [SerializeField] private Rigidbody2D robotBody;
    [SerializeField] private SpriteRenderer eye;
    [SerializeField] private SpriteRenderer doorLight;

    private float blackAlpha = 1f;
    private float objectiveAlpha;
    private bool ready;
    private GUIStyle bootStyle;
    private GUIStyle objectiveStyle;

    public void Configure(PlayerController controller, RobotVisualAnimator animator, Rigidbody2D body, SpriteRenderer robotEye, SpriteRenderer exitLight)
    {
        playerController = controller;
        visualAnimator = animator;
        robotBody = body;
        eye = robotEye;
        doorLight = exitLight;
    }

    private void Awake()
    {
        if (playerController) playerController.enabled = false;
        if (visualAnimator) visualAnimator.enabled = false;
        if (robotBody)
        {
            robotBody.linearVelocity = Vector2.zero;
            robotBody.simulated = false;
        }
        if (eye) eye.enabled = false;
        if (doorLight) doorLight.color = new Color(0.9f, 0.08f, 0.12f);
    }

    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(0.25f);

        if (eye)
        {
            eye.enabled = true;
            eye.color = new Color(0f, 1f, 0.65f);
        }

        float elapsed = 0f;
        while (elapsed < 0.85f)
        {
            elapsed += Time.unscaledDeltaTime;
            blackAlpha = Mathf.Lerp(1f, 0f, elapsed / 0.85f);
            yield return null;
        }

        blackAlpha = 0f;
        ready = true;
        if (robotBody) robotBody.simulated = true;
        if (visualAnimator) visualAnimator.enabled = true;
        if (playerController) playerController.enabled = true;
        if (doorLight) doorLight.color = new Color(0.05f, 1f, 0.62f);

        elapsed = 0f;
        while (elapsed < 0.6f)
        {
            elapsed += Time.unscaledDeltaTime;
            objectiveAlpha = Mathf.Clamp01(elapsed / 0.6f);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(4f);
        while (objectiveAlpha > 0f)
        {
            objectiveAlpha -= Time.unscaledDeltaTime;
            yield return null;
        }
    }

    private void OnGUI()
    {
        EnsureStyles();

        if (blackAlpha > 0.001f)
        {
            Color oldColor = GUI.color;
            GUI.color = new Color(0.005f, 0.008f, 0.012f, blackAlpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
            GUI.color = oldColor;
        }

        if (ready && objectiveAlpha > 0f)
        {
            Color oldColor = GUI.color;
            GUI.color = new Color(1f, 1f, 1f, objectiveAlpha);
            GUI.Label(new Rect(0, 36, Screen.width, 42), "OBJETIVO: SAL DEL AREA DE PRUEBAS", objectiveStyle);
            GUI.Label(new Rect(0, Screen.height - 62, Screen.width, 32), "A / D  MOVER     ·     ESPACIO  SALTAR", bootStyle);
            GUI.color = oldColor;
        }
    }

    private void EnsureStyles()
    {
        if (bootStyle != null) return;

        bootStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = Mathf.Max(14, Screen.height / 45),
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleLeft
        };
        bootStyle.normal.textColor = new Color(0.15f, 1f, 0.78f);

        objectiveStyle = new GUIStyle(bootStyle)
        {
            fontSize = Mathf.Max(18, Screen.height / 32),
            alignment = TextAnchor.MiddleCenter
        };
        objectiveStyle.normal.textColor = Color.white;
    }
}
