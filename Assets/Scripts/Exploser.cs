using System.Collections.Generic;
using UnityEngine;

public class Exploser : MonoBehaviour
{
    [SerializeField] private float _power = 5f;
    [SerializeField] private Generator _generator;

    private void OnEnable()
    {
        _generator.Destroyed += Explode;
    }

    private void OnDisable()
    {
        _generator.Destroyed -= Explode;
    }

    private void Explode(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            cube.AddForce(_power);
        }
    }
}