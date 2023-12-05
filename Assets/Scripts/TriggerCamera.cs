using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[SelectionBase]
public class TriggerCamera : MonoBehaviour
{
    public GameObject CameraPosition;
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _virtualCamera.transform.position = CameraPosition.transform.position;
        _virtualCamera.transform.rotation = CameraPosition.transform.rotation;
    }
}
