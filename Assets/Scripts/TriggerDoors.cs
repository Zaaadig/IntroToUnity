using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[SelectionBase]
public class TriggerDoors : MonoBehaviour
{
    public GameObject LeftDoorReference;
    public GameObject RightDoorReference;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            LeftDoorReference.GetComponent<Animator>().SetTrigger("Open");
            RightDoorReference.GetComponent<Animator>().SetTrigger("Open");
        }
    }
}
