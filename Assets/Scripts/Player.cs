using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f; // 5 unit� par seconde

    [SerializeField]
    private float _rotationSpeed = 15.0f; // 15 unit�s par seconde

    private bool _isWalking = false;

    public bool IsWalking { get => _isWalking; set => _isWalking = value; }

    private void Update()
    {
        Vector2 lInputVector = new Vector2(0.0f, 0.0f);

        if (Input.GetKey(KeyCode.W)) // S
        {
            lInputVector.y = +1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            lInputVector.y = -1.0f;
        }
        if (Input.GetKey(KeyCode.A)) // Q
        {
            lInputVector.x = -1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            lInputVector.x = 1.0f;
        }
        lInputVector = lInputVector.normalized;

        IsWalking = lInputVector.magnitude > 0.0f;

        // Direction
        Vector3 lDirection = new Vector3(lInputVector.x, 0.0f, lInputVector.y);        
        
        // D�placement
        transform.position +=  lDirection * Time.deltaTime * _speed;
        
        // Rotation
        //transform.forward = lDirection;
        transform.forward = Vector3.Slerp(transform.forward, lDirection, _rotationSpeed * Time.deltaTime); // pas bien, mais on en reparlera
    }


}
