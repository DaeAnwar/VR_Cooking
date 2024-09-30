using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class ChefController : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public AudioSource audioSource;
    public List<AnimationAndTalking> animationsAndClips;
    public Transform playerCamera; // Reference to the camera's transform

    private NavMeshAgent navMeshAgent;
    private string currentAnimationName;
    private bool allowMovingCheck = false;
    private string afterWalkAnimationName;

    void Start()
    {
        InitializeComponents();
        PerformAction("Waving", true);
    }

    void Update()
    {
        if (allowMovingCheck)
        {
            CheckMovement();
        }
        else
        {
            FacePlayerCamera();
        }
    }

    private void InitializeComponents()
    {
        if (!animator)
            animator = GetComponent<Animator>();

        if (!audioSource)
            audioSource = GetComponent<AudioSource>();

        navMeshAgent = GetComponent<NavMeshAgent>() ?? gameObject.AddComponent<NavMeshAgent>();

        if (!playerCamera)
        {
            playerCamera = Camera.main?.transform;
            if (!playerCamera)
            {
                Debug.LogError("Player camera not found.");
            }
        }
    }

    private void CheckMovement()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            PerformAction(afterWalkAnimationName, true);
            allowMovingCheck = false;
            Debug.Log("<color=yellow> Reached Destination </color>");
        }
    }

    private void FacePlayerCamera()
    {
        if (playerCamera)
        {
            Vector3 lookDirection = playerCamera.position - transform.position;
            lookDirection.y = 0; // Keep only the horizontal direction
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    public void PerformAction(string animationName, bool animationState)
    {
        Debug.Log($"Performing action {animationName}");

        if (!string.IsNullOrEmpty(currentAnimationName))
        {
            animator.SetBool(currentAnimationName, false);
        }

        currentAnimationName = animationName;
        AnimationAndTalking action = animationsAndClips.Find(a => a.animationName == animationName);

        if (action != null)
        {
            animator.SetBool(animationName, animationState);

            // Stop any currently playing audio before playing the new clip
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            if (action.audioClip != null)
            {
                audioSource.PlayOneShot(action.audioClip);
                StartCoroutine(SetIdleAfterAudio(action.audioClip.length, animationName));
            }
        }
        else
        {
            Debug.LogWarning($"Animation '{animationName}' not found!");
        }
    }

    private IEnumerator SetIdleAfterAudio(float audioLength, string animationName)
    {
        yield return new WaitForSeconds(audioLength);
        if (currentAnimationName == animationName)
        {
            PerformAction("Idle", true);
        }
    }

    public void MoveToAndPerformAction(Transform target, string AfterWalkAnimationName)
    {
        afterWalkAnimationName = AfterWalkAnimationName;
        PerformAction("Walking", true);
        navMeshAgent.destination = target.position;
        allowMovingCheck = true;
    }
}
