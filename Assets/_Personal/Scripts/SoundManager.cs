using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    AudioSource audioSource;
    public AudioSource musicSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioClip backgroundMusic = (AudioClip)Resources.Load("Sound/Monster City");
        PlayMusicBackGround(backgroundMusic, 0.5f);
    }

    public void RecolteFeedBackSound()
    {
        AudioClip recolteSound = (AudioClip) Resources.Load("Sound/Recolte 2");
        PlayInstantFeedBack(recolteSound,1.25f);
    }

    public void LVLSound()
    {
        AudioClip lvlSound = (AudioClip)Resources.Load("Sound/LVL");
        PlayInstantFeedBack(lvlSound,1);
    }

    public void LowSupplySound()
    {
        AudioClip lowSupplySound = (AudioClip)Resources.Load("Sound/Low Supply");
        PlayInstantFeedBack(lowSupplySound,0.5f);
    }

    public void LooTSound()
    {
        AudioClip looTSound = (AudioClip)Resources.Load("Sound/LooT");
        PlayInstantFeedBack(looTSound,1);
    }

    public void Gems2Sound()
    {
        AudioClip gems2Sound = (AudioClip)Resources.Load("Sound/Gems 2");
        PlayInstantFeedBack(gems2Sound,1);
    }

    public void GameOverSound()
    {
        AudioClip gameOVerSound = (AudioClip)Resources.Load("Sound/Game Over");
        PlayInstantFeedBack(gameOVerSound,0.3f);
    }

    public void DecreeSound()
    {
        AudioClip decreeSound = (AudioClip)Resources.Load("Sound/Decret");
        PlayInstantFeedBack(decreeSound,1);
    }

    public void Deplacement()
    {
        AudioClip Deplacement = (AudioClip)Resources.Load("Sound/Deplacement");
        PlayInstantFeedBack(Deplacement, 0.2f);
    }



    public void PlayInstantFeedBack(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip,volume);
        
    }

    public void StopInstantFeedBack()
    {
        audioSource.Stop();
    }

    public void PlayMusicBackGround(AudioClip music, float volume)
    {
        musicSource.clip = music;
        musicSource.volume = volume;
        musicSource.Play();
    }
}
