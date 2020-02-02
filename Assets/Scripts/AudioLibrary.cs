using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

//Audio Lists
public enum playerEffects { Attack, Damaged, MushMancy, RootWall, Cure }
public enum music { MainMenu, Background, Win }
public enum powerups { }

public class AudioLibrary : MonoBehaviour
{
    //Audio List from enum to array
    public AudioClip[] player;
    public AudioClip[] misc;
    public AudioClip[] music;

    //Volume
    [Range(0, 1)]
    float volume;

    public AudioSource musicPlayer;

    /// <summary>
    /// Plays a miscelanious sound at a certain point
    /// </summary>
    /// <param name="s">Miscelanious sound</param>
    public void miscSounds(powerups s, float volume)
    {
        AudioSource.PlayClipAtPoint(misc[(int)s], Vector3.zero, volume/* * GameManager.gMan.volume*/);
    }
    public void miscSounds(powerups s)
    {
        AudioSource.PlayClipAtPoint(misc[(int)s], Vector3.zero);
    }

    /// <summary>
    /// Plays different music on the games dedicated audio source
    /// </summary>
    /// <param name="s">What clip are we playing</param>
    /// <param name="playing">Is the clip playing</param>
    /// <param name="volume">What is the audio sources volume</param>
    public void musicSounds(music s, bool playing, float volume)
    {
        musicPlayer.clip = music[(int)s];
        Debug.Log("current s: " + s.ToString() + ", music player: " + musicPlayer.name);
        if (playing && !musicPlayer.isPlaying)
        {
            musicPlayer.volume = volume;
            musicPlayer.Play();
            Debug.Log("is playing");
        }
        else if (!playing)
        {
            musicPlayer.Stop();
        }
    }
    public void musicSounds(music s, bool playing)
    {
        musicPlayer.clip = music[(int)s];
        Debug.Log("current s: " + s.ToString() + ", music player: " + musicPlayer.name);
        if (playing && !musicPlayer.isPlaying)
        {
            musicPlayer.Play();
            Debug.Log("is playing");
        }
        else if (!playing)
        {
            musicPlayer.Stop();
        }
    }
}
