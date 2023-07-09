using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Hands.Samples.VisualizerSample;


[System.Serializable]
public class MapTransforms
{
    public Transform vrTarget;
    public Transform ikTarget;

    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    /// <summary>
    /// VRMapping() method which maps the IK Target & VR Target
    /// </summary>
    /// 
    public void VRMapping()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }

}

[System.Serializable]
public class MapTransformsForHands
{
    public Transform vrHandTarget;
    public Transform ikHandTarget;

    public Vector3 trackingHandPositionOffset;
    public Vector3 trackingHandRotationOffset;

    public GameObject handVisualizer;


    /// <summary>
    /// VRMapping() method which maps the IK Target & VR Target
    /// </summary>
    /// 
    public void MapHandTracking(int i)
    {
        switch (i)
        {
            case 1:
                //assigning Hands
                GameObject leftH = handVisualizer.transform.GetChild(0).transform.GetChild(0).gameObject;
                Debug.Log((leftH.gameObject == null) + leftH.gameObject.name + handVisualizer.transform.GetChild(0).name);
                vrHandTarget = leftH.transform;
                Debug.Log("Left Hand Gotchaaaaaa!");
                ikHandTarget.position = vrHandTarget.TransformPoint(trackingHandPositionOffset);
                ikHandTarget.rotation = vrHandTarget.rotation * Quaternion.Euler(trackingHandRotationOffset);
                break;

            case 2:
                //assigning Hands
                GameObject rightH = handVisualizer.transform.GetChild(2).transform.GetChild(0).gameObject;
                Debug.Log((rightH.gameObject == null));
                vrHandTarget = rightH.transform;
                Debug.Log("Rightt Hand Gotchaaaaaa!");
                ikHandTarget.position = vrHandTarget.TransformPoint(trackingHandPositionOffset);
                ikHandTarget.rotation = vrHandTarget.rotation * Quaternion.Euler(trackingHandRotationOffset);
                break;

        }

    }

}

/// <summary>
/// 
/// </summary>
public class VRAvatarController : MonoBehaviour
{

    [SerializeField] private MapTransforms head;

    [SerializeField] private MapTransformsForHands leftHand;
    [SerializeField] private MapTransformsForHands rightHand;

    [SerializeField] private float turnSmoothness;
    [SerializeField] Transform ikHead;
    [SerializeField] Vector3 headBodyOffset;


    private void LateUpdate()
    {
        transform.position = ikHead.position + headBodyOffset;

        //transform.forward on the Y plane and Normalizing the vector so it always at Length 1. Using Lerp the rotation will be smooth.
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(ikHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        //This will map head (VR Camera)
        head.VRMapping();

        //This will map hands (XR Hands)
        StartCoroutine(VRHandMapping());
    }
    IEnumerator VRHandMapping()
    {

        yield return new WaitForSeconds(2);

        leftHand.MapHandTracking(1);

        rightHand.MapHandTracking(2);
    }

}
