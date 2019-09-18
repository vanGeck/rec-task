using UnityEngine;

public class Cube : MonoBehaviour
{
    public bool HasExpired => !_isAlive;

    private float _timeAlive;
    private  float _lifetimeSeconds = 3.0f;
    private bool _isAlive = true;

    private Renderer _renderer;
    private Material _mat;


    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _mat = _renderer.material;
    }

    public void ManualUpdate()
    {
        //CastRays();
        _timeAlive += Time.deltaTime;

        if (_timeAlive >= _lifetimeSeconds)
        {
            _isAlive = false;
        }
    }

    private void CastRays()
    {
        int hits = 0;
        if(Physics.Raycast(transform.position, Vector3.forward)) hits++;
        if(Physics.Raycast(transform.position, Vector3.back)) hits++;
        if(Physics.Raycast(transform.position, Vector3.left)) hits++;
        if(Physics.Raycast(transform.position, Vector3.right)) hits++;
        
        ReactToNeighbours(hits);

    }

    public void ReactToNeighbours(int hits)
    {
        switch (hits)
        {
            case 0:
                _mat.color = Color.black;
                break;
            case 1:
                _mat.color = Color.blue;
                break;
            case 2:
                _mat.color = Color.green;
                break;
            case 3:
                _mat.color = Color.cyan;
                break;
            case 4:
                _mat.color = Color.yellow;
                break;
        }
    }
    
    
    public void SetLifetime(float time)
    {
        _lifetimeSeconds = time;
    }
}