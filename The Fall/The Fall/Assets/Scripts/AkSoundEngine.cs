using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkSoundEngine : MonoBehaviour
{
    public static AkSoundEngine Instance;


    #region Singleton
    public static AkSoundEngine instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PostEvent(string name, GameObject go)
    {


    }
}
