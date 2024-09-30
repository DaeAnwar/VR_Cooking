using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Radio : MonoBehaviour
{
    public List<AudioClip> songQueue;
    private int currentSongIndex = 0;
    private bool isPlaying = false;
    [SerializeField] GameObject PauseBtn;
    [SerializeField] GameObject StartBtn;
    [SerializeField] AudioSource audioSource;
    private Transform playerTransform;
    public float maxVolumeDistance = 3f;

    void Start()
    {
        playerTransform = Camera.main.transform;
        PlayCurrentSong();
    }

    void Update()
    {
        // Calculate the distance between the radio and the player
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // Adjust volume based on distance
        audioSource.volume = Mathf.Clamp01(1.0f - distance / maxVolumeDistance);

        // Adjust stereo panning based on player's direction relative to the radio
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float stereoPan = Vector3.Dot(directionToPlayer.normalized, transform.right);
        audioSource.panStereo = stereoPan;

        // Check if the current song has finished
        if (!audioSource.isPlaying && isPlaying)
        {
            PlayNext();
        }
    }

    public void PlayPrevious()
    {
        if (currentSongIndex > 0)
        {
            currentSongIndex--;
            PlayCurrentSong();
        }
    }

    public void TogglePause()
    {
        if (isPlaying)
        {
            audioSource.Pause();
            PauseBtn.SetActive(false);
            StartBtn.SetActive(true);
            isPlaying = false;
        }
        else
        {
            audioSource.UnPause();
            PauseBtn.SetActive(true);
            StartBtn.SetActive(false);
            isPlaying = true;
        }
    }

    public void PlayNext()
    {
        if (currentSongIndex < songQueue.Count - 1)
        {
            currentSongIndex++;
            PlayCurrentSong();
        }
        else
        {
            currentSongIndex = 0; // Reset to the first song if you reach the end of the list
            PlayCurrentSong();
        }
    }

    private void PlayCurrentSong()
    {
        audioSource.clip = songQueue[currentSongIndex];
        audioSource.Play();
        isPlaying = true;
        PauseBtn.SetActive(true);
        StartBtn.SetActive(false);
    }
}
