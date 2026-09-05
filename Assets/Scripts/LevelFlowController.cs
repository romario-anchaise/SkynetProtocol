using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelFlowController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private bool completed;
    private GUIStyle titleStyle;
    private GUIStyle detailStyle;

    public void Configure(PlayerController playerController)
    {
        player = playerController;
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null && keyboard.rKey.wasPressedThisFrame)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Complete()
    {
        if (completed) return;
        completed = true;
        if (player)
        {
            player.enabled = false;
            Rigidbody2D body = player.GetComponent<Rigidbody2D>();
            if (body) body.linearVelocity = Vector2.zero;
        }
    }

    private void OnGUI()
    {
        if (!completed) return;
        EnsureStyles();

        Color oldColor = GUI.color;
        GUI.color = new Color(0.01f, 0.03f, 0.05f, 0.88f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        GUI.color = Color.white;
        GUI.Label(new Rect(0, Screen.height * .36f, Screen.width, 70), "PRUEBA COMPLETADA", titleStyle);
        GUI.Label(new Rect(0, Screen.height * .49f, Screen.width, 45), "La unidad aprendio a modificar su entorno.", detailStyle);
        GUI.Label(new Rect(0, Screen.height * .59f, Screen.width, 45), "PULSA R PARA REINICIAR", detailStyle);
        GUI.color = oldColor;
    }

    private void EnsureStyles()
    {
        if (titleStyle != null) return;
        titleStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = Mathf.Max(28, Screen.height / 18),
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };
        titleStyle.normal.textColor = new Color(0.1f, 1f, 0.7f);

        detailStyle = new GUIStyle(titleStyle)
        {
            fontSize = Mathf.Max(16, Screen.height / 36)
        };
        detailStyle.normal.textColor = Color.white;
    }
}
