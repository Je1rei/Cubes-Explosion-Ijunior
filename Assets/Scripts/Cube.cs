using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _chanceSeparate = 100;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public event Action<Cube> Clicked;

    public int ChanceSeparate => _chanceSeparate;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(int dividerScale, int dividerChance)
    {
        transform.localScale /= dividerScale;
        _chanceSeparate /= dividerChance;
        _renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    public void AddForce(float force, float distance = 1)
    {
        float minValue = 50f;
        float maxValue = 100f;
        force /= distance;

        Vector3 direction = new Vector3(force * RandomizeValue(minValue, maxValue + 1),
                force * RandomizeValue(minValue, maxValue + 1), force * RandomizeValue(minValue, maxValue + 1));

        _rigidbody.AddForce(direction);

        Debug.Log(force);
    }

    public void Destroy()
    {
        Clicked?.Invoke(this);
        Destroy(gameObject);
    }

    private float RandomizeValue(float minValue, float maxValue) => UnityEngine.Random.Range(minValue, maxValue);
}
