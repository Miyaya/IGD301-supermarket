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

    private LineRenderer lineRenderer;
    private bool isIndexFingerPinching;
    private void Start()
    {
        lineRenderer = rightHand.GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        Transform rightHandTransform = rightHand.transform;
        var pos = rightHandTransform.position;
        lineRenderer.SetPosition(0, pos);

        var left = -rightHandTransform.right; // NOTE: Idk why .forward doesn't really work, instead have to use left
        RaycastHit hit;
        bool hasHit = Physics.Raycast(pos, left, out hit, Mathf.Infinity);
        isIndexFingerPinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        // Checking that the user pushed the trigger
        if (isIndexFingerPinching && hasHit)
        {
            // Sending the selected object hit by the raycast
            currentSelectedObject = hit.collider.gameObject;
        }

        // Determining the end of the LineRenderer depending on whether we hit an object or not
        if (hasHit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, raycastMaxDistance * left);
        }


        // DO NOT REMOVE
        // If currentSelectedObject is not null, this will send it to the TaskManager for handling
        // Then it will set currentSelectedObject back to null
        base.CheckForSelection();
    }
}
