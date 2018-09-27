using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    laser ballPath;

    [SerializeField]
    PlayEndSting endSting;

    //Input
    public AudioClip[] melNotes;
    public AudioClip[] chords;
    public AudioClip finish;

    //Output
    public AudioMixerGroup melOutput;
    public AudioMixerGroup chordOutput;

    public float changeTime = 1f;

    float timer;

    void Update()
    {      
        timer += Time.deltaTime;
        if (timer > changeTime)
        {
            timer = 0f;
            PlayChord();
        }
    }

    private void Awake()
    {
        PlayChord();
    }

    public void PlayNote()
    {
    
        AudioSource source = gameObject.AddComponent<AudioSource>();

        //Load Clip into Audio Source
        source.clip = melNotes[ballPath.collisionCount];

        //Set the output for audio source
        source.outputAudioMixerGroup = melOutput;

        //Play the clip
        source.Play();
        //Destroy the audio source when played

        Destroy(source, melNotes[ballPath.collisionCount].length);
    }
    public void PlayChord()
    {
        //Randomize
        int randomChord = Random.Range(0, chords.Length);

        // create the audio source
        AudioSource source = gameObject.AddComponent<AudioSource>();

        //Load Clip into Audio Source
        source.clip = chords[randomChord];

        //Set the output for audio source
        source.outputAudioMixerGroup = chordOutput;

        //Play the clip
        source.Play();
        //Destroy the audio source when played

        Destroy(source, chords[randomChord].length);

    }

    void PlayFinish()
    {
        if (endSting.endGame == true)
        {
            print("endgame");
        }
    }


}
