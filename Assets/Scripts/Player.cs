using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTankMode : MonoBehaviour
{
    public float SpeedInMeterPerSecond = 6.0f;
    public float RotationSpeedInDegreePerSecond = 15.0f;

    public GameObject _playerVisual;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // # Vous pouvez retrouver les axes dans les menus de Unity : Edit > Project Settings > Input Manager > Axes
        // il y a les axes 'Horizontal' et 'Vertical' pour le clavier ET la manette
        Vector3 lInputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        // # ici on normalize le vecteur pour avoir une vitesse constante dans toutes les directions
        Vector3 lDirection = lInputVector.normalized;

        // # Détection de collision
        // 1ere version de la detection de collision
        float lDetectionDistance = SpeedInMeterPerSecond * Time.deltaTime; // on calcule le prochain déplacement du joueur
        float lPlayerRadius = .5f; // le joueur a un rayon de 50cm
        Vector3 lPoint2 = transform.position + (Vector3.up * 1.8f);
        bool lHitSomething = Physics.CapsuleCast(transform.position, lPoint2, lPlayerRadius, lDirection, out RaycastHit raycastHit, lDetectionDistance);

        // visualisation du rayon de détection
        //Debug.DrawRay(lDetectionOrigin, lDirection * lDetectionDistance, Color.yellow, 1/60);

        // on peut bouger si on a rien touché ou si on a touché un trigger
        bool lCanMove = (lHitSomething == false) || (raycastHit.collider.isTrigger == true);

        if (lCanMove == false)
        {
            Vector3 moveDirX = new Vector3(lDirection.x, 0, 0).normalized;
            lHitSomething = Physics.CapsuleCast(transform.position, lPoint2, lPlayerRadius, moveDirX, lDetectionDistance);

            if (lHitSomething == false)
            {
                lDirection = moveDirX;
                lCanMove = true;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, lDirection.z).normalized;
                lHitSomething = Physics.CapsuleCast(transform.position, lPoint2, lPlayerRadius, moveDirZ, lDetectionDistance);
                if (lHitSomething == false)
                {
                    lDirection = moveDirZ;
                    lCanMove = true;
                }
                else
                {
                    lDirection = Vector3.zero;
                    lCanMove = false;
                }
            }
        }


        if (lCanMove == true)
        {
            //float lMoveDistance = SpeedInMeterPerSecond * Time.deltaTime;
            transform.position = transform.position + (lDirection * lDetectionDistance);

            // # Déplacement du Player
            // on récupère la position actuelle du Player et on lui ajoute la direction multipliée par la moveDistance

            // même chose avec la notation +=
            //transform.position += lDirection * SpeedInMeterPerSecond * Time.deltaTime;

            // encore une alternative avec la fonction 'Translate' qui est disponible sur tous les GameObjects
            //transform.Translate(lDirection * SpeedInMeterPerSecond * Time.deltaTime);
        }


        // # Rotation du Player                
        if (lDirection.magnitude > 0)
        {
            // On connait la direction du coup on peut entrer la valeur directement dans le forward vector qui est disponible sur tous les GameObjects
            // transform.forward = lDirection;

            // version smooth en utilisant (mal) une interpolation sphérique (Slerp => https://docs.unity3d.com/ScriptReference/Vector3.Slerp.html)
            _playerVisual.transform.forward = Vector3.Slerp(_playerVisual.transform.forward, lDirection, RotationSpeedInDegreePerSecond * Time.deltaTime);

            // version smooth alternative en utilisant (mal) une interpolation sphérique et des quaternions
            //_playerVisual.transform.rotation = Quaternion.Slerp(_playerVisual.transform.rotation, Quaternion.LookRotation(lDirection), RotationSpeedInDegreePerSecond * Time.deltaTime);
        }
    }
}
