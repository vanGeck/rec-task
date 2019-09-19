using UnityEngine;

/* This script causes the overhead camera to follow a specified Transform instance.
 * The requirements are as follows:
 * A. The camera moves on the X/Z plane.
 * B. The camera has an acceleration and damping effect, meaning it's not "glued" to the
 *    transform, but rather gently adjusts upon transform position change.
 * C. Damping parameters can be adjusted via the inspector.
 */

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform _transformToFollow;
    [SerializeField] private float _dampingFactor;
    
    private Vector3 curentVel;

    private void Update()
    {
        var yFiltered = _transformToFollow.position;
        yFiltered.y = transform.position.y;
        var v = Vector3.SmoothDamp(transform.position,
            yFiltered, ref curentVel, _dampingFactor);

        transform.position = v;

    }


}
