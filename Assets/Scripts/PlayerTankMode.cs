using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankMode : MonoBehaviour
{
    public float SpeedInMeterPerSecond = 6.0f;  // 6m/s => 21.6km/h
    public float RotationSpeedInDegreePerSecond = 180.0f; // demi tour en 1 seconde

    public GameObject _playerVisual;
    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        // on récupère une référence vers le CharacterController du Player
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // # Vous pouvez retrouver les axes dans les menus de Unity : Edit > Project Settings > Input Manager > Axes
        // il y a les axes 'Horizontal' et 'Vertical' pour le clavier ET la manette
        Vector3 lInputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

        // ## Rotation du Player avec un transfotm.Rotate, on utilise la composante x de l'input pour la rotation ( axe Horizontal )
        transform.Rotate(Vector3.up, lInputVector.x * (RotationSpeedInDegreePerSecond * Time.deltaTime));

        // ici on utilise la fonction SimpleMove du CharacterController avec la composante z de l'input pour le déplacement ( axe Vertical )
        _characterController.SimpleMove(transform.forward * SpeedInMeterPerSecond * lInputVector.z);
    }
}
