using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands;
using static UnityEngine.GraphicsBuffer;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget; //act repectively to the VR headset
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public GameObject handVisualizer;

    

    /// <summary>
    /// Will set the pos & rot of the rig target to VR target
    ///  
    /// Transform Point : Return the world position that rig target would have if it was a child of the VR target.
    /// </summary>
    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }

    public void MapHandTracking(int i)
    {
        switch (i)
        {
            case 1:
                //assigning Hands
                GameObject leftH = handVisualizer.transform.GetChild(0).transform.GetChild(0).gameObject;
                vrTarget.position = leftH.transform.position;
                Debug.Log("Left Hand Gotchaaaaaa!");

                rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
                rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
                
                break;

            case 2:
                GameObject rightH = handVisualizer.transform.GetChild(2).transform.GetChild(0).gameObject;
                vrTarget.position = rightH.transform.position;
                Debug.Log("Rightt Hand Gotchaaaaaa!");

                rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
                rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
                //assigning Hands
                break;

        }

       
       
            rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
            rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
        
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Transform headConstraints;
    public Vector3 headBodyOffset;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffset = transform.position - headConstraints.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = headConstraints.position + headBodyOffset;

        // We want our player body to rotate only in Y axis.
        transform.forward = Vector3.ProjectOnPlane(headConstraints.up, Vector3.up).normalized;

        //call this variable to the map function & their position always match to VR
        head.Map();
        leftHand.MapHandTracking(1);
        rightHand.MapHandTracking(2);
    }

    //Handvisualizer
    //1st child's 1st child & 3rd childs's 1st child
}
