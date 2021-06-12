using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLaserSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("PlayLaserSound _audioSource is NULL");
        }

        else
        {
            _audioSource.clip = _laserSoundClip;//  laserSoundClip will be played when we call _audioSource.Play()
        }

        PlayLaserSoundClip(); 
    }

    

    void PlayLaserSoundClip() // Everytime we call this function the "laser_shot" will play
    {
        _audioSource.Play(); // the selected sound clip
    }
}
