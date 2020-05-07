using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grasping : MonoBehaviour
{
    int gripSend = 0;
    public static string graspMessage;
    Animator gripperAnimator;
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        gripperAnimator = hand.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        while (Input.GetKeyDown(KeyCode.Space))
        {
            if(gripSend == 0)
            {
                Debug.Log("Gripper is Closing");
                gripSend = 1;
                gripperAnimator.SetBool("GripperIsOpening", false);
                gripperAnimator.SetBool("GripperIsClosing", true);
                return;
            }
            if(gripSend == 1)
            {
                Debug.Log("Gripper is Opening");
                gripSend = 0;
                gripperAnimator.SetBool("GripperIsOpening", true);
                gripperAnimator.SetBool("GripperIsClosing", false);
                return;
            }
        }


        graspMessage = gripSend.ToString() + "\n\n";
        FindObjectOfType<AngleController>().GetAngles();
        FindObjectOfType<SocketManager>().SendAngles();
    }
}
