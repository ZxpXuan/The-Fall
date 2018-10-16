using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorchanger : MonoBehaviour
{
    [SerializeField] Gradient topColors;
    [SerializeField] Gradient bottomColors;
    [SerializeField] float speed;
    [SerializeField] Material material;
    float timer;
	// Use this for initialization
	void Start ()
    {
        if (!material)
            material = GetComponent<Renderer>().sharedMaterial;

        if (!material)
            enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime * speed;
	    timer += timer > 1? -1: 0;

        material.SetColor("_TColor", topColors.Evaluate(timer));
        material.SetColor("_BottomColor", bottomColors.Evaluate(timer));
	}
}
