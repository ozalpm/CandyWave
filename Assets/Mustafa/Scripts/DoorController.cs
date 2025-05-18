using UnityEngine;
using System.Collections;
using Mirror;

public class DoorController : NetworkBehaviour
{
    public float openAngle = 90f;
    public float closedAngle = 0f;
    public float rotationSpeed = 2f;

    private bool isOpen = false;
    private bool isRotating = false;

    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    private AudioSource audioSource;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, closedAngle, 0);
        isOpen = false;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    [Command(requiresAuthority = false)]
    public void ToggleDoor()
    {
        if (!isRotating)
        {
            if (isOpen)
            {
                if (doorCloseSound != null)
                    audioSource.PlayOneShot(doorCloseSound);

                StartCoroutine(RotateDoor(closedAngle));
            }
            else
            {
                if (doorOpenSound != null)
                    audioSource.PlayOneShot(doorOpenSound);

                StartCoroutine(RotateDoor(openAngle));
            }
            isOpen = !isOpen;
        }
    }

    IEnumerator RotateDoor(float targetAngle)
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        float duration = 1f / rotationSpeed;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }
}
