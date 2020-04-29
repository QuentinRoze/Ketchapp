using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyArea : MonoBehaviour
{
    public int whichDirtyArea;
    public Transform[] dirtyAreas;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.pc.currentDirtyArea = this;
        }
    }

    public Transform GetNextDirtyAreaTransform()
    {
        return dirtyAreas[whichDirtyArea];
    }

    public void CleanDirtyArea()
    {
        Destroy(dirtyAreas[whichDirtyArea].gameObject);
        whichDirtyArea++;
        if(whichDirtyArea == dirtyAreas.Length)
        {
            NoMoreDirtyArea();
        }
    }

    public void NoMoreDirtyArea()
    {
        PlayerController.pc.currentDirtyArea = null;
        Destroy(gameObject);
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.pc.currentDirtyArea = null;
        }
    }


}
