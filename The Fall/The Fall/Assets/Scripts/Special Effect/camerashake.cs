using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerashake : MonoBehaviour {
    private Vector3 deltaPosition = Vector3.zero; 
    private bool cancelShake = false;

    static Camera _camera;
    static Camera Camera
    {
        get
        {
            if (_camera != null)
                return _camera;

            _camera = Camera.main;
            return _camera;
        }
    }

    static bool shaking;
    static Vector3 orgPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shake()
    {
        if (!shaking)
            orgPosition = Camera.transform.position;  //摄像机震动前的位置
        StartCoroutine(ShakeCamera());
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
        shaking = true;

        while (t < 1 && !cancelShake)
        {
            if (_camera == null) yield break;
            t += Time.deltaTime * speed;
            Camera.transform.position = orgPosition + new Vector3(Mathf.Sin(rate * t), Mathf.Cos(rate * t), 0) * Mathf.Lerp(shakeStrength, 0, t);
            yield return null;
        }
        cancelShake = false;
        Camera.transform.position = orgPosition;      //还原摄像机位置
        shaking = false ;

        Destroy(this.gameObject);
    }

	private void OnCollisionEnter(Collision collision)
	{
        var go = new GameObject().AddComponent<camerashake>();
        Handheld.Vibrate();
        go.Shake();
	}
    
}
