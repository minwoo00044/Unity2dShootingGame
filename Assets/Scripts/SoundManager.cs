using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BGM, 효과음 재생
//필요한거 : 오디오 클립

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmAudioSource;
    public AudioSource effAudioSource;

    public List<AudioClip> bgmAudioClips = new List<AudioClip>();
    public List<AudioClip> explosionAudioClips = new List<AudioClip>();
    public List<AudioClip> itemAudioClips = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
