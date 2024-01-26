using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Your implemented technique inherits the InteractionTechnique class
public class MyTechnique : InteractionTechnique
{
    [SerializeField]
    int raycastMaxDistance = 1000;

    [SerializeField]
    private OVRHand rightHand;
    [SerializeField]
    private OVRHand leftHand;

    private LineRenderer lineRenderer;
    private bool leftPinching;
    private bool rightIndexPinching;
    private bool rightMiddlePinching;
    private void Start()
    {
        lineRenderer = rightHand.GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        Transform rightHandTransform = rightHand.transform;
        // make the starting point to palm
        Vector3 offset = rightHandTransform.right;
        float offsetScale = 0.08f;
        offset.x *= offsetScale;
        offset.y *= offsetScale;
        offset.z *= offsetScale;
        var pos = rightHandTransform.position - offset;
        lineRenderer.SetPosition(0, pos);

        var down = -rightHandTransform.up;
        RaycastHit hit;
        bool hasHit = Physics.Raycast(pos, down, out hit, Mathf.Infinity);
        leftPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        rightIndexPinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        rightMiddlePinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Middle);
        if (hasHit)
        {
            if (leftPinching)
            {
                currentSelectedObject = hit.collider.gameObject;
            }
            else if (rightIndexPinching)
            {
                removeSelectedObject = hit.collider.gameObject;
            }
            else if (rightMiddlePinching)
            {
                putbackSelectedObject = hit.collider.gameObject;
            }

            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, raycastMaxDistance * down);
        }


        // DO NOT REMOVE
        // If currentSelectedObject is not null, this will send it to the TaskManager for handling
        // Then it will set currentSelectedObject back to null
        base.CheckForSelection();
    }
}
