using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private bool allowPlayerInput = true;
    public Vector2 GetMovementVectorNormalized()
    {
        if (!allowPlayerInput)
            return Vector2.zero;
        
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
// Change to player movement?
    public void DisablePlayerInput()
    {
        allowPlayerInput = false;
    }

    public void AllowPlayerInput()
    {
        allowPlayerInput = true;
    }
}
