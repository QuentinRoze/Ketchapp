using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAimFollower : MonoBehaviour
{
    public Transform body;
    public float playerXPercentage;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(PlayerController.pc.body.position.x * playerXPercentage, PlayerController.pc.body.position.y, PlayerController.pc.body.position.z);
        body.position = newPosition;
    }
}
