using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleMovement : MonoBehaviour
{

    public Transform bone3;
    public float yFactor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 polePos = bone3.position;
        polePos += new Vector3(0f, yFactor, 0f);
        transform.position = polePos;
    }
}
