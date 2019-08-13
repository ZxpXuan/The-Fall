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
    Animator anim;
    private Vector3 InputSecondPos;

    public Vector3 OrigCam;
    Transform Top, Right, Left, Bottom;

    public void Awake()
    {
        OrigCam = Camera.main.transform.position;
    }
    void Start()
    {

        cam = Camera.main;
        FindObjectOfType<limitation>().maxRing.GetComponent<Renderer>().enabled = false;
        ding = FindObjectOfType<limitation>().GetComponent<Rigidbody>();
        gm = GameManager.Instance;
        speedMultiplier = 3f;

        Top = GameObject.Find("Boundry").transform.GetChild(2);
        Right = GameObject.Find("Boundry").transform.GetChild(0);
        Bottom = GameObject.Find("Boundry").transform.GetChild(3);
        Left = GameObject.Find("Boundry").transform.GetChild(1);


    }

    public bool CheckInBounds(Vector3 top, Vector3 bottom, Vector3 left, Vector3 right, Vector3 sample)
    {

        if (sample.x < right.x && sample.x > left.x && sample.y < top.y && sample.y > bottom.y)

        {
            return true;
        }
        else
        {

            return false;
        }
    }
    void Update()
    {

        Vector3 v3 = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {


            v3.z = 10.0f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
        }
        
        if (GameManager.Instance.hasBallBeenShot != true) { 
           
            if (abale == 1)
            {
                if (isDrage == false)
                {
                  
                        offset = Vector3.zero;               
                }

                if (Input.GetMouseButtonDown(0) && (Camera.main.transform.position.y <= OrigCam.y + 0.6f && Camera.main.transform.position.y >= OrigCam.y - 0.6f))
                {
                    Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);
                    Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;
                    InputFirstPos = currentPosition;
                }

                if (Input.GetMouseButton(0) && (Camera.main.transform.position.y <= OrigCam.y+0.6f && Camera.main.transform.position.y >= OrigCam.y - 0.6f))
                {
                    Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                    Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;
                  
                    
                    if (true)
                    {
                      
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
                       // AkSoundEngine.SetRTPCValue("Aim_Velocity", mult);
                    }

                    isDrage = true;

                    ding.GetComponentInChildren<BallAngle>().startFadeOut();
                    
                   
                }
                else
                {
                    isDrage = false;
				}
				

				if (Input.GetMouseButtonUp(0))
                {
                    Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
                    Vector3 currentPosition = cam.ScreenToWorldPoint(currentScreenSpace) + offset;
					mousePos = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward *0);
                    Vector3 dir = new Vector3(mousePos.x - InputFirstPos.x, mousePos.y - InputFirstPos.y, 0).normalized;
					
                    float mult = Vector3.Distance(cam.ScreenToWorldPoint(currentScreenSpace) + offset, InputFirstPos);

                    if (force > minDistance)
                    {
                        ding.AddForce(dir * Mathf.Clamp( mult,0,maxForceDistance) * speedMultiplier, ForceMode.Impulse);
                       GameManager.Instance.GetComponent<LineRenderer>().enabled = false;

                        abale = abale + 1;
                     
                        PlayerPrefs.SetInt("hasBallBeenShot", 1);
                        GameManager.Instance.HideHint();

                        ding.GetComponentInChildren<TextMesh>().gameObject.SetActive(false);
                        GameManager.Instance.hasBallBeenShot = true;

                        SoundBank.instance.PlayShootSound();
                        ding.GetComponentInChildren<BallAngle>().startFadeOut();
                        if (cam.GetComponent<Animator>() != null && GameManager.Instance.hasBallBeenShot)
                        {
                            cam.GetComponent<Animator>().enabled = false;
                        }
                    }
                    else
                    {
                      //  AkSoundEngine.PostEvent("Aim_Error", gameObject);
                        ding.GetComponentInChildren<BallAngle>().startFadeIn();
                    }
                   // FindObjectOfType<limitation>().maxRing.GetComponent<Renderer>().enabled = false;
                }
            }

           
        }
    }
}
