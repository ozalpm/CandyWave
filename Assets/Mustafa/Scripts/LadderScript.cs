using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class LadderTeleporter : MonoBehaviour
{
    [Tooltip("Tag of ladder trigger colliders")]
    public string ladderTag = "Ladder";

    [Tooltip("Offset above the top marker to place the player")]
    public float topOffset = 1f;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only respond to ladders
        if (!other.CompareTag(ladderTag))
            return;

        // Find the LadderTop child under this ladder instance
        Transform topMarker = other.transform.Find("LadderTop");
        if (topMarker == null)
        {
            Debug.LogWarning($"No LadderTop found under {other.name}");
            return;
        }

        // Calculate intended teleport Y
        float targetY = topMarker.position.y + topOffset;

        // If we're already at or above that Y, skip teleport
        if (transform.position.y >= targetY)
            return;

        // Otherwise, perform teleport
        Vector3 targetPos = new Vector3(
            topMarker.position.x,
            targetY,
            topMarker.position.z
        );

        controller.enabled = false;         // Disable so SetPosition isn’t blocked
        transform.position = targetPos;
        controller.enabled = true;          // Re‑enable CharacterController
    }
}
