using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    //public parameters
    public int chainLength = 3;
    public float iterations = 10f;
    public float delta = 0.001f;
    public float[] angleLimits;

    public Transform target;

    //Setup for protected parameters
    protected Transform root;
    protected Transform[] bones;
    protected float[] boneLengths;
    protected float completeLength;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        bones = new Transform[chainLength + 1];
        boneLengths = new float[chainLength];

        var currentBone = transform;
        for(int i = bones.Length - 1; i >= 0; i--)
        {
            bones[i] = currentBone;
            currentBone = currentBone.parent;
        }

        completeLength = 0f;
        for(int i = boneLengths.Length -1; i >= 0; i--)
        {
            boneLengths[i] = (bones[i + 1].position - bones[i].position).magnitude;
            completeLength = completeLength + boneLengths[i];
        }
        Debug.Log(completeLength);
        Debug.Log(boneLengths[2]);
    }

    void LateUpdate()
    {
        SolveIK();
    }

    void SolveIK()
    {
        bones[0] = root;

        if ((target.position - root.position).sqrMagnitude >= completeLength * completeLength)
        {
            Vector3[] bonePos;
            bonePos = new Vector3[bones.Length];
            bonePos[0] = root.position;
            Vector3 uVect = (target.position - root.position).normalized;
            for(int i = 0; i <= boneLengths.Length - 1; i++)
            {
                bonePos[i + 1] = bonePos[i] + uVect * boneLengths[i];
                bones[i + 1].position = bonePos[1 + i];
            }
        }
        else
        {
            float count = iterations;

            Vector3[] forPos;
            forPos = new Vector3[bones.Length]; //new forward values and new potential bone positions
            while (count > 0 && (forPos[bones.Length - 1] - target.position).sqrMagnitude >= delta * delta)
            {
                Vector3[] backPos;
                backPos = new Vector3[bones.Length]; //new back values
                backPos[bones.Length] = target.position;

                //backward
                for (int i = boneLengths.Length - 1; i >= 0; i--)
                {
                    Vector3 backBLVect = ((bones[i - 1].position - backPos[i]).normalized) * boneLengths[i - 1];
                    backPos[i - 1] = backPos[i] + backBLVect;
                }


                forPos[0] = root.position; //sets p0'' = to root
                                           //forward
                for (int i = 0; i <= boneLengths.Length - 1; i++)
                {
                    Vector3 forBLVect = ((backPos[i + 1] - forPos[i]).normalized) * boneLengths[i];
                    forPos[i + 1] = forPos[i] + forBLVect;
                }
                count--;
            }

            for (int i = bones.Length - 1; i >= 0; i--)
            {
                bones[i].position = forPos[i];
            }
        }

    }
}
