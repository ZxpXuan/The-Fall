using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundbank_Manager : MonoBehaviour
{

    public static Soundbank_Manager instance;

    int getCurrentLevel;
    levelmanager lm;

    private static bool created = false;
    int previousLevel;

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
        lm = FindObjectOfType<levelmanager>();
        if (lm == null)
        {
            getCurrentLevel = 0;
        }
        else
        {
            getCurrentLevel = lm.nextLevelid - 1;

        }

        ChangeMusic();
       
    }


    void OnLevelWasLoaded(int level)
    {
        if (this != instance) return;

        previousLevel = getCurrentLevel;
        lm = FindObjectOfType<levelmanager>();
        if (lm == null)
        {
            getCurrentLevel = 0;
        }
        else
        {
            getCurrentLevel = lm.nextLevelid - 1;
        }
        if (previousLevel != getCurrentLevel && (Application.loadedLevelName != "LevelSelect"))
            ChangeMusic();

        if (previousLevel == getCurrentLevel && previousLevel == 0)
        {
            if (Application.loadedLevelName != "LevelSelect");

        }


        ChangeMusic();


    }


    public void MenuSystem()
    {
        AkSoundEngine.PostEvent("Stop_Menu_Music", gameObject);
        AkSoundEngine.PostEvent("Start_Game", gameObject);
        AkSoundEngine.SetSwitch("Level_Difficulty", "Easy", gameObject);
        AkSoundEngine.PostEvent("Start_Music", gameObject);
    }

    public void MenuClick()
    {
        AkSoundEngine.PostEvent("Menu_Button", gameObject);
    }

    public void AnimationSFX()
    {
        AkSoundEngine.PostEvent("Start_Opening_Anim", gameObject);
        
    }
    public void startMusic(int level)
    {
        getCurrentLevel = level;
        ChangeMusic();
        AkSoundEngine.PostEvent("Start_Music", gameObject);
     //   print(level);


    }

    public void ChangeMusic()
    {
        if (getCurrentLevel == 0)
        {
            AkSoundEngine.PostEvent("Stop_Music", gameObject); 
            AkSoundEngine.PostEvent("Stop_Menu_Music", gameObject);

            AkSoundEngine.PostEvent("Play_Menu_Sting", gameObject);
            AkSoundEngine.PostEvent("Start_Menu_Music", gameObject);
           
        }

        if (getCurrentLevel < 0)
        {
            AkSoundEngine.PostEvent("Stop_Music", gameObject);
        }


        if ( getCurrentLevel > 0)
        {
            AkSoundEngine.PostEvent("Stop_Menu_Music", gameObject);          
          //  print("stop_menu_music");
        }

        if (getCurrentLevel == 2)
        {
            AkSoundEngine.SetSwitch("Level_Difficulty", "Easy", gameObject);
        //    print("Easy_Switch");
        }
         
        if (getCurrentLevel == 4 || getCurrentLevel == 5)
        {
            AkSoundEngine.SetSwitch("Level_Difficulty", "Medium", gameObject);
        //    print("Med_Switch");
        }

        if (getCurrentLevel == 6 || getCurrentLevel == 7)
        {
            AkSoundEngine.SetSwitch("Level_Difficulty", "Hard", gameObject);
        //    print("hardswitch");
        }

        if (getCurrentLevel == 8 || getCurrentLevel == 9 || getCurrentLevel == 10)
        {
            AkSoundEngine.SetSwitch("Level_Difficulty", "Second_Easy", gameObject); //secondeasy
         //      print("secondeasyswitch");
        }

        if (getCurrentLevel == 11 || getCurrentLevel == 12 || getCurrentLevel == 13)
        {
            AkSoundEngine.SetSwitch("Level_Difficulty", "Second_Med", gameObject); //secondmed
        //       print("secondmedswitch");
        }

        if (getCurrentLevel == 14 || getCurrentLevel == 15 || getCurrentLevel == 16 || getCurrentLevel > 16)
        {
            AkSoundEngine.SetSwitch("Level_Difficulty", "Second_Easy", gameObject); //secondeasy
       //     print("secondHardSwitch");
        }

        if (getCurrentLevel > 16)
        {
            AkSoundEngine.SetSwitch("Level_Difficulty", "Second_Med", gameObject); //secondMed
            //   print("hardswitch");
        }

        if (getCurrentLevel == 20)
        {
            AkSoundEngine.SetSwitch("Level_Difficulty", "Second_Easy", gameObject); //Seconeasy
            //   print("hardswitch");
        }




    }
}

