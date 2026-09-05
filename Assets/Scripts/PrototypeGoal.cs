using UnityEngine;

public class PrototypeGoal : MonoBehaviour
{
    [SerializeField] private LevelFlowController flow;

    public void Configure(LevelFlowController controller)
    {
        flow = controller;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "PlayerRobot" && flow)
            flow.Complete();
    }
}
