using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linedraw : MonoBehaviour {

    public LineRenderer launchRay;
	public LineRenderer aimingRay;

    public Transform p0;

    public Transform p1;

    public int layerOrder = 0;

    private bool able = true;

    public bool lockDistance;
    public float maxDistance;

    void Start()
    {
        AkSoundEngine.PostEvent("Stop_Aiming", gameObject);

        launchRay.positionCount = 2;
		aimingRay.positionCount = 2;
	}



    void Update()
    {
        AimingLine();
    }
    
    public void AimingLine()
    {
        if (p0 == null || p1 == null) return;

        if (able == true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                launchRay.enabled = true;
				aimingRay.gameObject.SetActive(true);
			}
            Vector3 targPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
			targPos.z = 0;
			
			var toTarget = (targPos - p1.position);
			var dist = toTarget.sqrMagnitude;

			RaycastHit hit;
			var aimRayStartPos = p1.position + toTarget.normalized * maxDistance;
			var aimRayEndPos = aimRayStartPos;
			if (Physics.SphereCast(p1.position, 0.5f, toTarget, out hit))
			{
				aimRayEndPos = aimRayStartPos + toTarget.normalized * (hit.distance - maxDistance);
			}

			if (dist > maxDistance * maxDistance)
			{
				targPos = aimRayStartPos;
			}
			
            launchRay.SetPosition(0, targPos);
            launchRay.SetPosition(1, p1.position);
			aimingRay.SetPosition(0, aimRayStartPos);
			aimingRay.SetPosition(1, aimRayEndPos);

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
            }
        }
    }

    void OnMouseDown()
    {
        able = true;
        AkSoundEngine.PostEvent("Play_Aiming", gameObject);

    }

    private void OnMouseUp()
    {
        AkSoundEngine.PostEvent("Stop_Aiming", gameObject);
        AkSoundEngine.PostEvent("Play_Shoot", gameObject);      
    }
}
