using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTarget : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    public float speedFactor;

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        if (Input.GetKey("w"))
        {
            mZCoord = mZCoord + speedFactor;
        }
        if (Input.GetKey("s"))
        {
            mZCoord = mZCoord - speedFactor;
        }
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    public void OnMouseUp()
    {
        FindObjectOfType<AngleController>().GetAngles();
        FindObjectOfType<SocketManager>().SendAngles();
    }

}
