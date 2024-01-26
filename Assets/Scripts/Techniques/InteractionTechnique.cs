using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InteractionTechnique : MonoBehaviour
{
    public UnityEvent<GameObject, int> objectSelectedEvent;
    protected GameObject currentSelectedObject = null;
    protected GameObject removeSelectedObject = null;
    protected GameObject putbackSelectedObject = null;
    protected void CheckForSelection()
    {
        if (currentSelectedObject != null)
        {
            SendObjectSelectedEvent(currentSelectedObject, 2);
            currentSelectedObject = null;
        }
        if (removeSelectedObject != null)
        {
            SendObjectSelectedEvent(removeSelectedObject, 0);
            removeSelectedObject = null;
        }

        if (putbackSelectedObject != null)
        {
            SendObjectSelectedEvent(putbackSelectedObject, 1);
        }
    }

    protected void SendObjectSelectedEvent(GameObject selectedObject, int action)
    {
        // action - 0: remove object; 1: put back object; 2: select object
        objectSelectedEvent.Invoke(selectedObject, action);
    }
}
