using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = new Vector2();
        if (Input.GetKey(KeyCode.A)) // Move Left
            inputVector.x -= 1;
        if (Input.GetKey(KeyCode.D)) // Move Right
            inputVector.x += 1;
        if (Input.GetKey(KeyCode.W)) // Move Up
            inputVector.y += 1;
        if (Input.GetKey(KeyCode.S)) // Move Down
            inputVector.y -= 1;
        return inputVector.normalized;
    }
}
