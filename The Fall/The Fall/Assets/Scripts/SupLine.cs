using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupLine : MonoBehaviour {

    private float angle;
    public GameObject ball;
    Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标
                                 // Use this for initialization
    public GameObject heng, shu, xie1, xie2;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            MouseFollow();
            Vector3 dric = mousePositionInWorld - ball.transform.position;
            Vector3 sp = Vector3.right;

            angle = Vector3.Angle(sp, dric);


            if (angle > 0 && angle < 22.5f)
            {
                heng.SetActive(true);
                shu.SetActive(true);
                xie1.SetActive(false);
                xie2.SetActive(false);
            }
            if (angle > 22.5f && angle < 67.5f)
            {
                xie1.SetActive(true);
                xie2.SetActive(true);
                heng.SetActive(false);
                shu.SetActive(false);
            }
            if (angle > 67.5f && angle < 112.5f)
            {
                heng.SetActive(true);
                shu.SetActive(true);
                xie1.SetActive(false);
                xie2.SetActive(false);
            }
            if (angle > 112.5f && angle < 157.5f)
            {
                xie1.SetActive(true);
                xie2.SetActive(true);
                heng.SetActive(false);
                shu.SetActive(false);
            }
            if (angle > 157.5f && angle < 180f)
            {
                heng.SetActive(true);
                shu.SetActive(true);
                xie1.SetActive(false);
                xie2.SetActive(false);
            }
        }

            
	}

    void MouseFollow()
    {
        //获取鼠标在相机中（世界中）的位置，转换为屏幕坐标；
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        //获取鼠标在场景中坐标
        mousePositionOnScreen = Input.mousePosition;
        //让场景中的Z=鼠标坐标的Z
        mousePositionOnScreen.z = screenPosition.z;
        //将相机中的坐标转化为世界坐标
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        //物体跟随鼠标移动
        //transform.position = mousePositionInWorld;
        //物体跟随鼠标X轴移动
       // transform.position = new Vector3(mousePositionInWorld.x, transform.position.y, transform.position.z);
    }
}
