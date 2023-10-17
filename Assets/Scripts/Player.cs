using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f; // 5 unité par seconde

    [SerializeField]
    private float _rotationSpeed = 15.0f; // 15 unités par seconde

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

        // Direction
        Vector3 lDirection = new Vector3(lInputVector.x, 0.0f, lInputVector.y);        
        
        // Déplacement
        transform.position +=  lDirection * Time.deltaTime * _speed;
        
        // Rotation
        //transform.forward = lDirection;
        transform.forward = Vector3.Slerp(transform.forward, lDirection, _rotationSpeed * Time.deltaTime); // pas bien, mais on en reparlera
    }
}
