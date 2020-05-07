using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleController : MonoBehaviour
{
    protected Transform[] bones;
    private int dOF = 5;
    public Transform target;
    public static string angleMessage;
    // Start is called before the first frame update
    void Awake()
    {
        bones = new Transform[dOF];
        var current = transform;
        for(int i = bones.Length - 1; i >= 0; i--)
        {
            if(i == 0)
            {
                current = current.parent;

            }
            bones[i] = current;
            current = current.parent;
        }
        Debug.Log(bones[0]);
        Debug.Log(bones[1]);
        Debug.Log(bones[2]);
    }

    public void GetAngles()
    {
        float[] angles;
        angles = new float[bones.Length];
        for(int i = bones.Length - 1; i >= 1; i--)
        {
            if (i == bones.Length - 1)
            {
                if (bones[i].localRotation.eulerAngles.z > 180f)
                {
                    angles[i] = -360f + Mathf.Round(bones[i].localRotation.eulerAngles.z * 100.0f) / 100.0f;
                }
                else
                {
                    angles[i] = Mathf.Round(bones[i].localRotation.eulerAngles.z * 100.0f) / 100.0f;
                }
            }
            else
            {
                if (bones[i].localRotation.eulerAngles.x > 180f)
                {
                    angles[i] = -360f + Mathf.Round(bones[i].localRotation.eulerAngles.x * 100.0f) / 100.0f;
                }
                else
                {
                    angles[i] = Mathf.Round(bones[i].localRotation.eulerAngles.x * 100.0f) / 100.0f;
                }
            }
        }

        if (bones[0].localRotation.eulerAngles.z > 180f)
        {
            angles[0] = -360f + Mathf.Round(bones[0].localRotation.eulerAngles.z * 100.0f) / 100.0f;
        }
        else
        {
            angles[0] = Mathf.Round(bones[0].localRotation.eulerAngles.z * 100.0f) / 100.0f;
        }

        Debug.Log("j0 = " + angles[0]);
        Debug.Log("j1 = " + angles[1]);
        Debug.Log("j2 = " + angles[2]);
        Debug.Log("j3 = " + angles[3]);
        Debug.Log("j4 = " + angles[4]);

        for(int i = 0; i <= bones.Length - 1; i ++)
        {
            angleMessage = angleMessage + angles[i].ToString() + "\n";
        }
        angleMessage = angleMessage + Grasping.graspMessage;
    }
}
