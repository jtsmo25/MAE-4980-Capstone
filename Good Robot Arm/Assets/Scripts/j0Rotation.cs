using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class j0Rotation : MonoBehaviour
{
    public Transform target;
    public Transform j0;
    private Vector3 initRot;
    // Start is called before the first frame update
    void Start()
    {
        initRot = j0.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(target.hasChanged == true)
        {
            RotateJ0();
        }
    }
    void RotateJ0()
    {
        Vector2 home = new Vector2(0f, -1f);
        Vector2 targetPoint = new Vector2(target.position.x, target.position.z);
        Vector2 originPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        Vector2 rotVect = (targetPoint - originPoint).normalized;
        float rotAngle;
        //check which side target is on
        if (target.position.x > 0)
        {
           rotAngle = -1f * Vector2.Angle(home, rotVect);
        }
        else
        {
            rotAngle = Vector2.Angle(home, rotVect);
        }


        Vector3 newRot = initRot + new Vector3(0f, 0f, rotAngle);
        j0.transform.rotation = Quaternion.Euler(newRot);
    }
}
