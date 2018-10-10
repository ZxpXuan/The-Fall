using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound
{
    public AudioMixerGroup audMixGroup;

    private AudioSource source;
    public string clipName;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(1f, 3f)]
    public float pitch;

    public bool loop = false;
    public bool playOnAwake = false;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.pitch = pitch;
        source.volume = volume;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.outputAudioMixerGroup = audMixGroup;
    }

    public void Play()
    {
        source.Play();
    }

}
public class NewAudioManager : MonoBehaviour
{
    public static NewAudioManager instance;

    [SerializeField]
    Sound[] sound;


    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);

        }

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }


    private void Start()
    {
        for (int i = 0; i < sound.Length; i++)
        {
            GameObject _go = new GameObject("Sound" + i + "_" + sound[i].clipName);
            _go.transform.SetParent(this.transform);
            sound[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("Music");       
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if(sound[i].clipName == _name)
            {
                sound[i].Play();
                return;
            }
        }
    }
}

