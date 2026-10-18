using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class VictoryPortal : MonoBehaviour
{
    private bool hasTriggered;

    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null || other != player.mainCollider) return;

        GameManager gameManager = GameManager.Instance;
        if (gameManager == null || gameManager.IsDeathSequenceActive) return;

        // The capsule can overlap the portal before the climb is complete.
        if (gameManager.LegsTraining < 1f || gameManager.ChestTraining < 1f || gameManager.BackTraining < 1f) return;

        hasTriggered = true;
        gameManager.YouWin();
    }
}
