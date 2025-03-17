using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TWToast_KeyInstruction : TWBoard
{
    [SerializeField] GameObject _childContent;

    Transform _interactTarget;
    Vector3 _offset;
    Camera _mainCamera;

    void Start()
    {
        base.InitTWBoard(false);
        _mainCamera = Camera.main;
        
    }

    private void Update()
    {
        if (_interactTarget != null)
        {
            Vector3 positionToShow = _mainCamera.WorldToScreenPoint(_interactTarget.position + _offset);
            transform.position = positionToShow;
        }
    }

    public void SetSpawnTarget(Transform target, Vector3 offset)
    {
        _interactTarget = target;
        _offset = offset;
        Invoke(nameof(SetContentActive), 0.05f);
    }

    void SetContentActive()
    {
        _childContent.SetActive(true);
    }
}
