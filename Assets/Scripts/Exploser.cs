using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radiusOverlap = 1f;
    [SerializeField] private float _power = 5f;
    [SerializeField] private Generator _generator;

    private void OnEnable()
    {
        _generator.Separated += Explode;    
        _generator.Destroyed += Explode;
    }

    private void OnDisable()
    {
        _generator.Separated -= Explode;
        _generator.Destroyed -= Explode;
    }

    private void Explode(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.AddForce(_power);
        }
    }

    private void Explode(Cube currentCube)
    {
        Collider[] colliders = Physics.OverlapSphere(currentCube.transform.position, _radiusOverlap);

        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out Cube cube))
            {
                float distance = Vector3.Distance(currentCube.transform.position, cube.transform.position);
                cube.AddForce(_power, distance);
            }
        }
    }
}