using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject Door1;

    // bien penser a
    // -> mettre un tag sur le player
    // -> mettre un collider sur le trigger et cocher istrigger
    // -> dans animator declarer les parametres
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Destroy(Door1);

            //Door1.SetActive(false);
            
            //Door1.GetComponent<Animator>().SetBool("onOpen?", true);
            
            //Door1.GetComponent<Animator>().SetTrigger("Open");
        }
    }
}
