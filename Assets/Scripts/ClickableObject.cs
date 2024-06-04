using System;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Ray _ray;

    public event Action Clicked;

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
            if (hit.transform == transform)
            {
                Clicked?.Invoke();
            }
        }
    }
}
