using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

//Audio Lists
public enum playerEffect { Dead, Slide, Jump, Walk }
public enum powerup { Glider, Thruster, Anchor, Cloud }
public enum music { MainMenu, Background, Win }
public enum misc { Repair, Crashing, }

public class AudioLibrary : MonoBehaviour
{
    //Audio List from enum to array
    public AudioClip[] player;
    public AudioClip[] powerups;
    public AudioClip[] music;
    public AudioClip[] misc;

    //Volume
    [Range(0, 1)]
    float volume;

    public AudioSource musicPlayer;

    /// <summary>
    /// Player sounds
    /// </summary>
    /// <param name="s">player sound</param>
    public void Player(playerEffect s, float volume)
    {
        AudioSource.PlayClipAtPoint(player[(int)s], Vector3.zero, volume/* * GameManager.gMan.volume*/);
    }
    public void Player(playerEffect s)
    {
        AudioSource.PlayClipAtPoint(player[(int)s], Vector3.zero);
    }

    /// <summary>
    /// Plays powerup sounds
    /// </summary>
    /// <param name="s">powerup sound</param>
    public void Powerups(powerup s, float volume)
    {
        AudioSource.PlayClipAtPoint(powerups[(int)s], Vector3.zero, volume/* * GameManager.gMan.volume*/);
    }
    public void Powerups(powerup s)
    {
        AudioSource.PlayClipAtPoint(powerups[(int)s], Vector3.zero);
    }

    /// <summary>
    /// Misc sounds
    /// </summary>
    /// <param name="s">misc sound</param>
    public void Misc(misc s, float volume)
    {
        AudioSource.PlayClipAtPoint(misc[(int)s], Vector3.zero, volume/* * GameManager.gMan.volume*/);
    }
    public void Misc(misc s)
    {
        AudioSource.PlayClipAtPoint(misc[(int)s], Vector3.zero);
    }

    /// <summary>
    /// Plays different music on the games dedicated audio source
    /// </summary>
    /// <param name="s">What clip are we playing</param>
    /// <param name="playing">Is the clip playing</param>
    /// <param name="volume">What is the audio sources volume</param>
    public void MusicSounds(music s, bool playing, float volume)
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
    public void MusicSounds(music s, bool playing)
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
