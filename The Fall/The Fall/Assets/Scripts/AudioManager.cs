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

    public int randomChord;
    public int newCollisionCount;

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
        
        NoteGrouping();

    }

    private void Awake()
    {
        PlayChord();
    }

    public void PlayNote()
    {
    
        AudioSource source = gameObject.AddComponent<AudioSource>();

        //Load Clip into Audio Source
        source.clip = melNotes[newCollisionCount];

        //Set the output for audio source
        source.outputAudioMixerGroup = melOutput;

        //Play the clip
        source.Play();
        Debug.Log("melNote" + newCollisionCount);
        //Destroy the audio source when played

        Destroy(source, melNotes[newCollisionCount].length);
    }
    public void PlayChord()
    {
        //Randomize
        randomChord = Random.Range(0, chords.Length);

        // create the audio source
        AudioSource source = gameObject.AddComponent<AudioSource>();

        //Load Clip into Audio Source
        source.clip = chords[randomChord];

        //Set the output for audio source
        source.outputAudioMixerGroup = chordOutput;

        //Play the clip
        source.Play();
        Debug.Log("chord" + randomChord);
        //Destroy the audio source when played

        Destroy(source, chords[randomChord].length);

    }

    void NoteGrouping()
    {
        newCollisionCount = ballPath.collisionCount;

        if (randomChord == 1)
        {
            newCollisionCount += 3;
        }
        else if (randomChord == 2)
        {
            newCollisionCount += 6;
        }
        else
        {
            newCollisionCount = +0;
        }
    }


}
