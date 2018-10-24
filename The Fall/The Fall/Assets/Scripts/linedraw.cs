using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linedraw : MonoBehaviour {

    public LineRenderer lr;

    public Transform p0;

    public Transform p1;

    public int layerOrder = 0;

    private bool able = true;

    void Start()

    {
       
        lr.positionCount = (2);
       // lr.sortingLayerID = 15;
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
                lr.enabled = true;
            }

            Vector3 screenToWorld =Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z) );
            lr.SetPosition(0, new Vector3(screenToWorld.x,screenToWorld.y,0));
            lr.SetPosition(1, p1.position);
            
            lr.startWidth = 0.1f;
            lr.endWidth = 0.2f;

            if (Input.GetMouseButtonUp(0))
            {
                lr.enabled = false;
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
        AkSoundEngine.PostEvent("Play_Shoot", gameObject);
    }
}
