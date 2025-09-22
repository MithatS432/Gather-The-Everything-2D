using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject circle;
    public Vector3 offset;
    private float smoothspeed = 2f;
    private void LateUpdate()
    {
        Vector3 targetpos = circle.transform.position + offset;
        Vector3 smoothedpos = Vector3.Lerp(transform.position, targetpos, smoothspeed * Time.deltaTime);
        transform.position = smoothedpos;
    }
}
