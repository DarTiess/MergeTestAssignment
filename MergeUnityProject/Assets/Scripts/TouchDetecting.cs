using System;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class TouchDetecting: MonoBehaviour
    {
        private Card.Card _selectedObject;
        private Vector3 _offset;
        private Camera _camera;

        private void Start()
        {
            _camera=Camera.main;
        }

        private void Update()
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D point = Physics2D.OverlapPoint(mousePosition);
                if (point.TryGetComponent(out Card.Card card))
                {
                    _selectedObject = card;
                    _offset = _selectedObject.transform.position - mousePosition;
                }
            }
            if (_selectedObject)
            {
                _selectedObject.transform.position = mousePosition + _offset;
            }
            if (Input.GetMouseButtonUp(0) && _selectedObject)
            {
                _selectedObject = null;
            }
        }
    }
}