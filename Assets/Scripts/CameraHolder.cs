using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    [SerializeField] Transform camPos;
    void Update()
    {
        transform.position = camPos.position;
    }
}
