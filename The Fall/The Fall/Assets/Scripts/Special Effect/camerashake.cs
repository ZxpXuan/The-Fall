﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashake : MonoBehaviour {
    private Vector3 deltaPosition = Vector3.zero; 
    private bool cancelShake = false;  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void ShakeCamera()
    //{
    //    Camera.main.transform.localPosition -= deltaPosition;  //减去偏移
    //    deltaPosition = Random.insideUnitCircle / 3.0f; //在一个3.0单位圆内里震动
    //    Camera.main.transform.position += deltaPosition;       //加上偏移
    //}


    /*
     * 震动摄像机
     * shakeStrength ->震动幅度
     * rate -> 震动频率
     * shakeTime -> 震动时长
     */
    IEnumerator ShakeCamera(float shakeStrength = 0.2f, float rate = 14, float shakeTime = 0.4f)
    {
        float t = 0;    //计时器
        float speed = 1 / shakeTime;    //震动速度
        Vector3 orgPosition = Camera.main.transform.localPosition;  //摄像机震动前的位置

        while (t < 1 && !cancelShake)
        {
            t += Time.deltaTime * speed;
            Camera.main.transform.position = orgPosition + new Vector3(Mathf.Sin(rate * t), Mathf.Cos(rate * t), 0) * Mathf.Lerp(shakeStrength, 0, t);
            yield return null;
        }
        cancelShake = false;
        Camera.main.transform.position = orgPosition;      //还原摄像机位置
    }

	private void OnCollisionEnter(Collision collision)
	{
        StartCoroutine(ShakeCamera());
	}

}
