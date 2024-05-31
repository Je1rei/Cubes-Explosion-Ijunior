using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int _chanceSeparate = 100;

    private Ray _ray;
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event Action<Cube> Clicked;

    public int ChanceSeparate => _chanceSeparate;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CheckRaycast();
    }

    public void DivideChance(int divider) => _chanceSeparate /= divider;

    public void SetColor(Color newColor)
    {
        _renderer.material.color = newColor;
    }

    public void AddForce(float force)
    {
        float minValue = 50f;
        float maxValue = 100f;
        Vector3 direction = new Vector3(force * RandomValue(minValue, maxValue + 1),
                force * RandomValue(minValue, maxValue + 1), force * RandomValue(minValue, maxValue + 1));

        if (_rigidbody != null)
        {
            _rigidbody.AddForce(direction);
        }
    }

    private float RandomValue(float minValue, float maxValue) => UnityEngine.Random.Range(minValue, maxValue + 1);

    private void CheckRaycast()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.transform == transform)
            {
                Clicked?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}