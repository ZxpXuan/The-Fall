using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEndSting : MonoBehaviour { 

    public bool endGame;

    void OnTriggerEnter(Collider other)
    {
        endGame = true;
        print("endgame");
    }
}
