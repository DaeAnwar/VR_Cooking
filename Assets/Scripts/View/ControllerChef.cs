using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControllerChef : MonoBehaviour
{
    public List<AnimationAndTalking> animationsAndClips;
    public AudioSource audioSource;
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public Transform Target;
    //public Button moveButton;
    public Transform playerCamera; // Reference to the camera's transform

    private int currentClipIndex = 0;
    private bool isMoving = false;

    void Start()
    {
        InitializeComponents();
        if (animationsAndClips.Count > 0)
        {
            PlayAnimationAndAudio(0);
        }

       
    }

    void Update()
    {
        if (isMoving)
        {
            
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                isMoving = false;
                animator.SetBool("IsWalking", false);
                PlayAnimationAndAudio(currentClipIndex);
                Debug.Log("<color=yellow> Reached Destination </color>");
            }
        }
        else
        {
            FacePlayerCamera();
        }

        if (audioSource.isPlaying == false && isMoving == false)
        {
            PlayIdleAnimation();
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
    public void PlayAnimationAndAudio(int CurrentClipIndex)
    {
        currentClipIndex = CurrentClipIndex;
        animator.SetTrigger(animationsAndClips[currentClipIndex].animationName);
        audioSource.clip = animationsAndClips[currentClipIndex].audioClip;
        audioSource.Play();
    }

    void PlayIdleAnimation()
    {
        animator.Play("Idle");
    }

    public void MoveToTarget(Transform target, int index)
    {

        currentClipIndex = index;
        Target = target;
        isMoving = true;
        audioSource.Stop();
        animator.SetBool("IsWalking", true);
        navMeshAgent.destination = target.position;

    }

    /*void OnAnimatorMove()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && isMoving)
        {
            
        }
    }*/

   /* public void PlaySpecificAnimation(int index)
    {
        if (index >= 0 && index < animationsAndClips.Count)
        {
            audioSource.Stop();
            
            PlayAnimationAndAudio(animationsAndClips[index]);
        }
    }*/
}
