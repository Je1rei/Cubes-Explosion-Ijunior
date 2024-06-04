using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Ray _ray;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ProcessClick();
    }

    public void ProcessClick()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.TryGetComponent<Cube>(out Cube cube))
            {
                cube.DestroyCube();
            }
        }
    }
}

//1) ClickableObject - сколько кубов на сцене существует, столько рейкастов и будет обработано по нажатию мыши, 
//    смысла в этом нет, это лишния работа для процессора, вам достаточно одного рейкаста при нажатии, 
//    сравнение transform каждого куба с transform объекта на который нажали, не имеет никакого смысла, 
//    ведь нужный объект найдётся только один, 
//    нужно чтобы рейкасты обрабатывал отдельный объект не относящийся к кубам, 
//    пусть он пускает рейкасты, ищет куб по лучу вызывает у него метод DestroyCube