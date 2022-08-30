using UnityEngine;

public class Camera_Follow : MonoBehaviour
{

    [SerializeField] Transform target;
    //The higher value this has, the faster the camera looks onto the target. 
    [Header("Camera lock on speed")]
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] Vector3 offSet;
    private void FixedUpdate()
    {
        //Vector variebale for tracking where the camera should be placed. 
        Vector3 desiredPosition = target.position + offSet;

        //Smooth out the position for the camera.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        //Set the camera's position to the players. 
        transform.position = smoothedPosition;

        //Allways look at the player.
        transform.LookAt(target);
    }
}
