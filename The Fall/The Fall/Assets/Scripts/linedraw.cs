using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class linedraw : MonoBehaviour {

    public LineRenderer launchRay;
	public LineRenderer aimingRay;

    public Transform p0;

    public Transform p1;

    public int layerOrder = 0;

    public bool able = true;

    public bool lockDistance;
    public float maxDistance=3;
    public Vector3 firstPos;
    Vector3 finalPos , prevPos;

    Vector3 finalPos2;
    void Start()
    {
     //   AkSoundEngine.PostEvent("Stop_Aiming", gameObject);
        maxDistance = 3;
        launchRay.positionCount = 2;
		aimingRay.positionCount = 2;
        able = false;
        transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
	}



    void Update()
    {
        AimingLine();
    }
    
    public void AimingLine()
    {
        if (p0 == null || p1 == null) return;
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.hasBallBeenShot)
        {
            launchRay.enabled = true;
          //  aimingRay.gameObject.SetActive(true);
            able = true;
      //      AkSoundEngine.PostEvent("Play_Aiming", gameObject);
            Vector3 targPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            targPos.z = 0;

            firstPos = targPos;
            finalPos = firstPos;
        }
        if (able == true && !GameManager.Instance.hasBallBeenShot)
        {

           
            Vector3 targPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
			targPos.z = 0;
            Vector3 newVec = firstPos - p1.position;
			var toTarget = (targPos - p1.position);
			var dist = toTarget.sqrMagnitude;
            finalPos = targPos;
            if (Vector3.Distance(finalPos, firstPos) < maxDistance)
            {
                //finalPos = aimRayStartPos;
                //finalPos = targPos;

            }
            if (Vector3.Distance(targPos, firstPos) < maxDistance)
            {
                //finalPos = targPos;
            }

            RaycastHit hit;
			var aimRayStartPos = p1.position +  (finalPos - firstPos).normalized * maxDistance;
			var aimRayEndPos = aimRayStartPos;
			if (Physics.SphereCast(p1.position, 0.2f,(finalPos-firstPos) *maxDistance, out hit))
			{
				aimRayEndPos = aimRayStartPos+ (finalPos - firstPos ).normalized * (hit.distance );
			}

           
			if (Vector3.Distance(firstPos,finalPos)>maxDistance)
			{
				targPos = aimRayStartPos;
                finalPos2 = targPos;

            }
            else
            {
                finalPos2 = p1.position + (finalPos - firstPos);



            }


            launchRay.SetPosition(0, finalPos2);


            launchRay.SetPosition(1, p1.position);

			aimingRay.SetPosition(0, aimRayStartPos);
			aimingRay.SetPosition(1,aimRayEndPos);


            aimingRay.transform.position = aimRayEndPos;
			aimingRay.material.mainTextureOffset = new Vector2(-Time.time % 1, 0);
			aimingRay.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, (Mathf.Sin(Time.time * 5) + 1) * 0.5f);
			
			launchRay.startWidth = 0.1f;
            launchRay.endWidth = 0.2f;
            if (Input.GetMouseButtonUp(0))
            {
                launchRay.enabled = false;
				aimingRay.gameObject.SetActive(false);
				able = false;

                PlayerPrefs.SetFloat("xShot", finalPos2.x);
                PlayerPrefs.SetFloat("yShot", finalPos2.y);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            launchRay.enabled = false;
            aimingRay.gameObject.SetActive(false);
            able = false;

            PlayerPrefs.SetFloat("xShot", finalPos2.x);
            PlayerPrefs.SetFloat("yShot", finalPos2.y);
        }
    }

    void OnMouseDown()
    {
       

    }

    private void OnMouseUp()
    {
       // AkSoundEngine.PostEvent("Stop_Aiming", gameObject);
       // AkSoundEngine.PostEvent("Play_Shoot", gameObject);      
    }
}
