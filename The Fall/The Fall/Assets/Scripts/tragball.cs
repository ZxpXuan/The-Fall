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
    public float force;

    public Rigidbody ding;
    [SerializeField]
    private float speedMultiplier =3f;
    public Rigidbody ding2;
    private int abale = 1;
    public GameManager gm;
    public float minDistance = 2;
    private Vector3 mousePos;
	
    [SerializeField] float maxForceDistance=3;


    public Vector3 InputFirstPos;
    private Vector3 InputSecondPos;
    void Start()
    {
        cam = Camera.main;
        FindObjectOfType<limitation>().maxRing.GetComponent<Renderer>().enabled = false;
        ding = FindObjectOfType<limitation>().GetComponent<Rigidbody>();
        gm = GameManager.Instance;
        speedMultiplier = 3f;
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

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);
                    Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;
                    InputFirstPos = ding.transform.position;
                }

                if (Input.GetMouseButton(0) )
                {
                    Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                    Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;
                    print(currentPosition);
                    
                    if (true)
                    {
                        Debug.DrawLine(currentPosition, InputFirstPos);
                        if (Vector3.Distance(currentPosition, InputFirstPos) > 0.4)
                        {
                            var maxPosition = (currentPosition - InputFirstPos).normalized * 3.0f + InputFirstPos;
                            mousePos = maxPosition;
                        }
                        else
                        {
                            mousePos = currentPosition;
                        }
                        float mult = Vector3.Distance(cam.ScreenToWorldPoint(currentScreenSpace) + offset,InputFirstPos);
                        Vector3 dir = new Vector3(mousePos.x - InputFirstPos.x, mousePos.y -InputFirstPos.y, 0);

                        force = dir.magnitude;
                        //print(mult);
                        AkSoundEngine.SetRTPCValue("Aim_Velocity", mult);
                    }

                    isDrage = true;

                    ding.GetComponentInChildren<BallAngle>().startFadeOut();
                    FindObjectOfType<limitation>().maxRing.GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    isDrage = false;
				}
				Debug.DrawLine(mousePos, InputFirstPos, Color.blue);

				if (Input.GetMouseButtonUp(0))
                {
                    Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
                    Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;
					mousePos = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * screenSpace.z);
                    Vector3 dir = new Vector3(mousePos.x - InputFirstPos.x, mousePos.y - InputFirstPos.y, 0).normalized;
					
                    float mult = Vector3.Distance(cam.ScreenToWorldPoint(currentScreenSpace) + offset, InputFirstPos);

                    if (force > minDistance)
                    {
                        ding.AddForce(dir * Mathf.Clamp( mult,0,maxForceDistance) * speedMultiplier, ForceMode.Impulse);
                       GameManager.Instance.GetComponent<LineRenderer>().enabled = false;

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

           
        }
    }
}
