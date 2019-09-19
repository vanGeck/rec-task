using UnityEngine;

/*
 * This is a simple character controller. Requirements are as follows:
 * A. The character moves at a constant speed according to WSAD keys input.
 *     Movement direction is independent of character heading/rotation.
 * B. Pressing the left Shift key increases movement speed by a percentage.
 * C. Walking and running speed can be adjusted via the inspector.
 * D. The character always rotates directly towards the mouse pointer on his Y-axis.
 * 
 *
 * Please fill out the TurnTowardsMouse, ReadMovementInput, and Move methods.
 * No model animation is required.
 */

public class ManController : MonoBehaviour
{
    private void Update()
    {
        var dir = ReadMovementInput();
        Move(dir);
        TurnTowardsMouse();
    }

    private void TurnTowardsMouse()
    {
        
    }

    private Vector3 ReadMovementInput()
    {
        return Vector3.zero;
    }

    private void Move(Vector3 direction)
    {
        
    }
}
