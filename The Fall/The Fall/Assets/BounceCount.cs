using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceCount : MonoBehaviour {

    void Start()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {

        yield return new WaitForSeconds(1.4f);
        Destroy(gameObject);
    }
}
