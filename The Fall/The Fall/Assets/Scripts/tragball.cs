using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class tragball : MonoBehaviour
{
    private Camera cam;//发射射线的摄像机
    private GameObject go;//射线碰撞的物体
    public static string btnName;//射线碰撞物体的名字
    private Vector3 screenSpace;
    private Vector3 offset;
    private bool isDrage = false;
    private float dis;
   
    public GameObject ball;
    public Rigidbody ding;
    private bool abale = true ; 
    void Start()
    {
        cam = Camera.main;
       
    }
    void Update()
    {
        //整体初始位置 
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //从摄像机发出到点击坐标的射线
        RaycastHit hitInfo;
        if(abale == true)
        {

            if (isDrage == false)
            {
                if (Physics.Raycast(ray, out hitInfo))
                {
                    //划出射线，只有在scene视图中才能看到
                    Debug.DrawLine(ray.origin, hitInfo.point);
                    go = hitInfo.collider.gameObject;
                   //
                    screenSpace = cam.WorldToScreenPoint(go.transform.position);
                    offset = go.transform.position - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
                    //物体的名字  
                    btnName = go.name;
                    //组件的名字

                }
                else
                {
                    btnName = null;
                }
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;

                if (btnName != null && btnName == "emputy")
                {
                    //Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    // location.z = 0;
                    //dis = (ball.transform.position - go.transform.position).sqrMagnitude;
                    if (Vector3.Distance(currentPosition, ball.transform.position) > 2)
                    {
                        var maxPosition = (currentPosition - ball.transform.position).normalized * 2.0f + ball.transform.position;
                        go.transform.position = maxPosition;
                    }
                    else
                    {
                        go.transform.position = currentPosition;
                   }

                }
                //            float distance = Vector3.Distance(ball.transform.position, go.transform.position);
                // DisplayTrajectoryLineRenderer2();

                isDrage = true;
            }
            else
            {
                isDrage = false;
            }
           // print(btnName);

            if (Input.GetMouseButtonUp(0))
            {
                Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;
                Vector3 dir = go.transform.position - ball.transform.position;
                ding.AddForce(dir, ForceMode.Impulse);
                abale = false; 
            }

        }



        }


}
