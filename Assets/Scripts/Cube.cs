using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer), typeof(ClickableObject))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _chanceSeparate = 100;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private ClickableObject _clickableObject;

    public int ChanceSeparate => _chanceSeparate;

    public event Action<Cube> Clicked;

    private void OnEnable()
    {
        _clickableObject.Clicked += DestroyCube;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _clickableObject = GetComponent<ClickableObject>();
    }

    private void OnDisable()
    {
        _clickableObject.Clicked -= DestroyCube;
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

    private void DestroyCube()
    {
        Clicked?.Invoke(this);
        Destroy(gameObject); 
    }

    private float RandomValue(float minValue, float maxValue) => UnityEngine.Random.Range(minValue, maxValue);
}
