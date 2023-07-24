using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioSource playerAudio;

    // Start is called before the first frame update
    public void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void PlayClickSound()
    {
        playerAudio.PlayOneShot(clickSound);
    }

    public void OnMouseDown()
    {
        PlayClickSound();
    }
}
