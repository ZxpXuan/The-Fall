using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linedraw : MonoBehaviour {

    public LineRenderer lr;

    public Transform p0;

    public Transform p1;

    public int layerOrder = 0;

    void Start()

    {

        lr.positionCount = (2);

        lr.sortingLayerID = layerOrder;

    }

    void Update()

    {
        if(Input.GetMouseButtonDown(0))
        {
            lr.enabled = true;
        }

        lr.SetPosition(0, p0.position);
        lr.SetPosition(1, p1.position);
        lr.startWidth = 0.1f;
        lr.endWidth = 0.2f;

        if(Input.GetMouseButtonUp(0))
        {
            lr.enabled = false;
        }

    }
}
