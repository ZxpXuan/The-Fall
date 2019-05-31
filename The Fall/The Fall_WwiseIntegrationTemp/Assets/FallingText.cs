using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingText : MonoBehaviour {
    [SerializeField]
    public TextMesh objectToSpawn;
    private float startTime;
    public float intervalTime;
    public bool hasFallingStarted;
    public int numberSpawned;
    public int numberNeedToSpawn;
    public int numberToDisplay;
	// Use this for initialization
	void Start () {
		

	}
	public void spawnNumbers(int number, int numberToDisp)
    {
        numberSpawned = 0;
        numberNeedToSpawn = number;
        startTime = Time.time;
        hasFallingStarted = true;
        numberToDisplay = numberToDisp;

    }
	// Update is called once per frame
	void Update () {
		if(hasFallingStarted && numberSpawned <= numberNeedToSpawn)
        {
            if (startTime + intervalTime < Time.time)
            {
                TextMesh tm = Instantiate(objectToSpawn, transform.position, Quaternion.identity) as TextMesh;
                tm.text = "" + numberToDisplay;
                var randomR = Random.Range(0.2f, 1f);
                tm.transform.localScale = new Vector3(randomR, randomR, randomR);
                numberSpawned++;
                startTime = Time.time;
            }
        }
        else
        {
            hasFallingStarted = false;
            numberSpawned = 0;
        }

	}
}
