using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class tragball : MonoBehaviour
{
    //reference 
    private Camera cam;//发射射线的摄像机
    private GameObject go;//射线碰撞的物体
    public static string btnName;//射线碰撞物体的名字
    private Vector3 screenSpace;
    private Vector3 offset;
    private bool isDrage = false;
    private float dis;
    public float force;

    public float ballSpeed = 1;
    public GameObject emputy;

    public Rigidbody ding;
    [SerializeField]
    private float speedMultiplier =1f;
    public Rigidbody ding2;
    private int abale = 1;
    public GameObject MaxRing;
    public GameManager gm;
    public float minDistance = 2;
    private Vector3 mousePos;


    [SerializeField]
    public float maxForceDistance=3;
    void Start()
    {
        cam = Camera.main;
        FindObjectOfType<limitation>().maxRing.GetComponent<Renderer>().enabled = false;
        ding = FindObjectOfType<limitation>().GetComponent<Rigidbody>();
        gm = GetComponent<GameManager>();
    }
    void Update()
    {

        if (GameManager.Instance.hasBallBeenShot != true) { 
            //整体初始位置 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //从摄像机发出到点击坐标的射线
            RaycastHit hitInfo;

            //ball1 control
            if (abale == 1)
            {
                if (isDrage == false)
                {
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        //划出射线，只有在scene视图中才能看到
                        Debug.DrawLine(ray.origin, hitInfo.point);
                        go = hitInfo.collider.gameObject;

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

                if (Input.GetMouseButton(0) && btnName == "emputy")
                {
                    Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                    Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;

                    if (btnName != null && btnName == "emputy")
                    {
                        if (Vector3.Distance(currentPosition, ding.transform.position) > 0.4)
                        {
                            var maxPosition = (currentPosition - ding.transform.position).normalized * 3.0f + ding.transform.position;
                            mousePos = maxPosition;
                        }
                        else
                        {
                            mousePos = currentPosition;
                        }

                        Vector3 dir = new Vector3(mousePos.x - ding.transform.position.x, mousePos.y - ding.transform.position.y, 0);

                        force = dir.magnitude;
                        AkSoundEngine.SetRTPCValue("Aim_Velocity", force);
                    }

                    isDrage = true;

                    ding.GetComponentInChildren<BallAngle>().startFadeOut();
                    FindObjectOfType<limitation>().maxRing.GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    isDrage = false;
                }

                if (Input.GetMouseButtonUp(0) && btnName == "emputy")
                {
                    Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                    Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;
                    Vector3 dir = new Vector3(mousePos.x - ding.transform.position.x, mousePos.y - ding.transform.position.y, 0);
                    float mult = Vector3.Distance(cam.ScreenToWorldPoint(currentScreenSpace) + offset, ding.transform.position);

                    if (force > minDistance)
                    {
                        ding.AddForce(dir * Mathf.Clamp( mult,0,maxForceDistance) * speedMultiplier, ForceMode.Impulse);
                        FindObjectOfType<GameManager>().GetComponent<LineRenderer>().enabled = false;

                        abale = abale + 1;
                        PlayerPrefs.SetFloat("xShot", currentPosition.x);
                        PlayerPrefs.SetFloat("yShot", currentPosition.y);
                        PlayerPrefs.SetInt("hasBallBeenShot", 1);
                        ding.GetComponentInChildren<TextMesh>().gameObject.SetActive(false);
                        GameManager.Instance.hasBallBeenShot = true;
                        ding.GetComponentInChildren<BallAngle>().startFadeOut();
                        if (cam.GetComponent<Animator>() != null)
                        {
                            cam.GetComponent<Animator>().enabled = false;
                        }
                    }
                    else
                    {
                        AkSoundEngine.PostEvent("Aim_Error", gameObject);
                        ding.GetComponentInChildren<BallAngle>().startFadeIn();
                    }
                    FindObjectOfType<limitation>().maxRing.GetComponent<Renderer>().enabled = false;
                }
            }

            if (abale == 2)
            {

                if (isDrage == false)
                {
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        //划出射线，只有在scene视图中才能看到
                        Debug.DrawLine(ray.origin, hitInfo.point);
                        go = hitInfo.collider.gameObject;

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
                if (Input.GetMouseButton(0) && btnName == "emputy2")
                {
                    Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                    Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;

                    if (btnName != null && btnName == "emputy2")
                    {
                        if (Vector3.Distance(currentPosition, ding2.transform.position) > 3)
                        {
                            var maxPosition = (currentPosition - ding2.transform.position).normalized * 3.0f + ding2.transform.position;
                            go.transform.position = maxPosition;
                        }
                        else
                        {
                            go.transform.position = currentPosition;
                        }
                    }

                    isDrage = true;
                }
                else
                {
                    isDrage = false;
                }
                if (Input.GetMouseButtonUp(0) && btnName == "emputy2")
                {
                    Vector3 dir2 = new Vector3(go.transform.position.x - ding2.transform.position.x, go.transform.position.y - ding2.transform.position.y, 0);
                    ding2.AddForce(dir2, ForceMode.Impulse);
                    abale = abale + 1;
                }
            }
        }
    }
}
