using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[SelectionBase]
public class TriggerCamera : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public GameObject CameraPosition;

    private void OnTriggerEnter(Collider other)
    {
        VirtualCamera.transform.position = CameraPosition.transform.position;
        VirtualCamera.transform.rotation = CameraPosition.transform.rotation;
    }
}
