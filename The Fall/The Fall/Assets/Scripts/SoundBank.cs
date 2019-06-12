using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{

    public bool IsVoiceEnabled ;

    #region Singleton
    public static SoundBank instance;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion
    public int currentState = 0;
    [System.Serializable]
    public class Sound
    {
        public List<AudioClip> soundFile;
        public Brain.MoodTypes type;
        public Brain.GameState state;

    }

    [System.Serializable]

    public class LevelSound
    {
        public AudioClip soundFile;

        public string name;
    }


    public List<LevelSound> levelSounds;
    public List<AudioClip> shootSound;


    public List<Sound> soundList;

    public AudioSource m_AudioSource;
    public AudioSource m_LevelAudio;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleVoice()
    {

        IsVoiceEnabled = !IsVoiceEnabled;
        if (IsVoiceEnabled)
        {
            PlayerPrefs.SetInt("Voice", 1);


        }
        else
        {
            PlayerPrefs.SetInt("Voice", 0);


        }

        GameManager.Instance.unPauseGame();

    }

    public void PlaySound(Brain.MoodTypes type, Brain.GameState state)
    {

        if(Application.loadedLevelName!="LevelSelect" || Application.loadedLevelName !="MainMenu")
        if (PlayerPrefs.GetInt("Voice", 1) ==1 && Random.Range(0,100f) > 30f && Soundbank_Manager.instance.getCurrentLevel!=1)
        {
            Sound soundbank;
            if(state == Brain.GameState.VeryImpatient)
            {
                state = Brain.GameState.Impatient;
            }
            soundbank = soundList.Find(x => x.state == state && x.type == type);

            int RandomNumber = Random.Range(0, soundbank.soundFile.Count);
            print(RandomNumber);

            m_AudioSource.clip = soundbank.soundFile[RandomNumber];
            m_AudioSource.Play();

        }
    }
    public void PlayLevelSound(string name)
    {
        if(currentState == 0)
        {

            if (name == "Easy")
            {
                currentState = 1;
            }
            else if(name == "Medium")
            {

                currentState = 2;

            }

            else if (name == "Hard")
            {

                currentState = 3;

            }

            m_LevelAudio.clip = levelSounds.Find(x => x.name == name).soundFile;
            m_LevelAudio.Play();
        }

        else if (currentState == 1)
        {
            if (name == "Easy")
            {
                
            }
            else if (name == "Medium")
            {

                currentState = 2;
                m_LevelAudio.clip = levelSounds.Find(x => x.name == name).soundFile;
                m_LevelAudio.Play();

            }

            else if (name == "Hard")
            {

                currentState = 3;
                m_LevelAudio.clip = levelSounds.Find(x => x.name == name).soundFile;
                m_LevelAudio.Play();

            }

        }
        else if(currentState == 2)
        {
            if (name == "Easy")
            {

                currentState = 1;
                m_LevelAudio.clip = levelSounds.Find(x => x.name == name).soundFile;
                m_LevelAudio.Play();
            }
            else if (name == "Medium")
            {


            }

            else if (name == "Hard")
            {

                currentState = 3;
                m_LevelAudio.clip = levelSounds.Find(x => x.name == name).soundFile;
                m_LevelAudio.Play();

            }

        }
        else if (currentState == 3)
        {
            if (name == "Easy")
            {
                currentState = 1;
                m_LevelAudio.clip = levelSounds.Find(x => x.name == name).soundFile;
                m_LevelAudio.Play();

            }
            else if (name == "Medium")
            {

                currentState = 2;
                m_LevelAudio.clip = levelSounds.Find(x => x.name == name).soundFile;
                m_LevelAudio.Play();

            }

            else if (name == "Hard")
            {

                
            }

        }
     

    }
    public void PlayShootSound()
    {

        m_AudioSource.clip = shootSound[Random.Range(0, shootSound.Count)];
        m_AudioSource.Play();
    }
}
