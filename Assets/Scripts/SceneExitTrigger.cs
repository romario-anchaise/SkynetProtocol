using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExitTrigger : MonoBehaviour
{
    [SerializeField] private string destinationScene = "Level01";
    private bool loading;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (loading || other.gameObject.name != "PlayerRobot") return;
        loading = true;
        SceneManager.LoadScene(destinationScene);
    }
}
