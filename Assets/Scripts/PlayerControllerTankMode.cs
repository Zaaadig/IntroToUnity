using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float SpeedInMeterPerSecond = 6.0f;
    public float RotationSpeedInDegreePerSecond = 15.0f;

    public GameObject _playerVisual;
    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        #region Input
        // # Vous pouvez retrouver les axes dans les menus de Unity : Edit > Project Settings > Input Manager > Axes
        // il y a les axes 'Horizontal' et 'Vertical' pour le clavier ET la manette
        Vector3 lInputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        // # ici on normalize le vecteur pour avoir une vitesse constante dans toutes les directions
        Vector3 lDirection = lInputVector.normalized;
        #endregion

        #region Rotation du player
        // ## Rotation du Player
        transform.Rotate(Vector3.up, lInputVector.x * RotationSpeedInDegreePerSecond * Time.deltaTime);
        #endregion


        #region Detection de collision
        // ## Détection de collision
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
        #endregion


        #region Déplacament du player
        // ## Déplacement du Player
        if (lCanMove == true)
        {
            _characterController.SimpleMove(transform.forward * SpeedInMeterPerSecond * lInputVector.z);
        }
        #endregion
    }
}
