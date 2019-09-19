using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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
    [SerializeField] private Camera _cam;
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private float _movementRunModifier = 1.5f;

    
    private void Update()
    {
        var dir = ReadMovementInput();
        Move(dir);
        TurnTowardsMouse();
    }

    private void TurnTowardsMouse()
    {
        Ray mouseRay = _cam.ScreenPointToRay(Input.mousePosition);
        float midPoint = (transform.position - _cam.transform.position).magnitude * 0.5f;
        var position = mouseRay.origin + mouseRay.direction * midPoint;
        position.y = transform.position.y;
        transform.LookAt(position, Vector3.up);
    }

    private Vector3 ReadMovementInput()
    {
        var output = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) output += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) output += Vector3.back;
        if (Input.GetKey(KeyCode.A)) output += Vector3.left;
        if (Input.GetKey(KeyCode.D)) output += Vector3.right;

        output = output.normalized * _movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift)) output *= _movementRunModifier;

        return output;
    }

    private void Move(Vector3 movementVector)
    {
        transform.position += movementVector * Time.deltaTime;
    }
}
