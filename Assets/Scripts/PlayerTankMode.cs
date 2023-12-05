using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTankMode : MonoBehaviour
{
    public float SpeedInMeterPerSecond = 6.0f;  // 6m/s => 21.6km/h
    public float RotationSpeedInDegreePerSecond = 180.0f; // demi tour en 1 seconde
    public float _jumpHeight = 0.5f; // hauteur du saut en mètre
    
    public GameObject _playerVisual;

    private CharacterController _characterController;
    private Vector3 _playerVelocity;
    private float _gravityValue = -9.81f;
    private bool _groundedPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        // on récupère une référence vers le CharacterController du Player
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // code source du saut depuis la doc de unity
        //https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
        _groundedPlayer = _characterController.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        // Avec quelques modifications pour gérer la rotation et la translation
        // Rotation avec l'axe horizontal (manette ou clavier => gauche/droite)
        transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * (RotationSpeedInDegreePerSecond * Time.deltaTime));

        // Translation uniquement avec l'axe Vertical (manette ou clavier => avant/arrière)
        _characterController.Move(transform.forward * Input.GetAxis("Vertical") * (SpeedInMeterPerSecond * Time.deltaTime));

        // Si le joueur appuie sur la touche Jump ET (&&) qu'il est au sol, on lui applique une force vers le haut
        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }
        // on applique la gravité
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        // Translation du joueur en fonction de la gravité sur l'axe Y du monde (Unity est Y-UP)
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }
}
