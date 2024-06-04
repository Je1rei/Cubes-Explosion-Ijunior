using System;
using System.Collections.Generic;
using System.Linq;
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
            int countSpawn = RandomCount();

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
        newCub.Clicked += Spawn;
        newCub.DivideChance(_dividerChance);

        newCub.SetColor(RandomColor());
        newCub.transform.localScale /= _dividerScale;

        return newCub;
    }

    private Color RandomColor() => UnityEngine.Random.ColorHSV();

    private int RandomCount() => UnityEngine.Random.Range(_minCount, _maxCount);
}
