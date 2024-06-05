using System;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private int _minCount = 2;
    [SerializeField] private int _maxCount = 6;
    [SerializeField] private int _minChance = 0;
    [SerializeField] private int _maxChance = 100;

    [SerializeField] private int _dividerScale = 2;
    [SerializeField] private int _dividerChance = 2;

    [SerializeField] private List<Cube> _cubes;

    public event Action<List<Cube>> Destroyed;

    private void OnEnable()
    {
        foreach (var cube in _cubes)
            cube.Clicked += Spawn;
    }

    private void OnDisable()
    {
        foreach (var cube in _cubes)
            cube.Clicked -= Spawn;
    }

    private void Spawn(Cube cube)
    {
        List<Cube> createdCubes = new List<Cube>();

        if (cube.ChanceSeparate >= UnityEngine.Random.Range(_minChance, _maxChance))
        {
            int countSpawn = RandomizeCount();

            for (int i = _minCount; i <= countSpawn; i++)
            {
                createdCubes.Add(InstantiateCubes(cube));
            }

            _cubes.Remove(cube);
            cube.Clicked -= Spawn;
            Destroyed?.Invoke(createdCubes);
        }
    }

    private Cube InstantiateCubes(Cube cube)
    {
        Cube newCub = Instantiate(cube);
        newCub.Initialize(_dividerScale, _dividerChance);
        newCub.Clicked += Spawn;

        return newCub;
    }

    private int RandomizeCount() => UnityEngine.Random.Range(_minCount, _maxCount);
}
