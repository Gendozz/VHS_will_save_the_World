using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("-----     Настройки     -----")]
    [Header("Точка на герое, откуда стреляет крюк")]
    [SerializeField] private Transform _firePoint;

    [Header("Рендерер верёвки")]
    [SerializeField] private LineRenderer _lineRenderer;

    [Header("Слой, за который можно зацепиться")]
    [SerializeField] private LayerMask _grappableLayer;

    [Header("Радиус, в котором работает детект зацепляемых объектов")]
    [SerializeField] private float _detectGrapplablePointRadius;

    [Header("-----     Компоненты и системные     -----")]
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private Transform player;

    private Collider[] _grapplablePoints = new Collider[2];   // How much grapple points may be in detection radius?

    private Vector3 _currentGrapplablePoint;

    private int _currentGrappablePointsAmount;

    private SpringJoint _joint;

    private void Update()
    {
        if (_playerInput.IsGrappleButtonPressed)
        {
            StartGrapple();
        }

        if (_playerInput.IsJumpButtonPressed)
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void DrawRope()
    {
        if (!_joint) 
            return;

        _lineRenderer.SetPosition(0, _firePoint.position);
        _lineRenderer.SetPosition(1, _currentGrapplablePoint);
    }

    private void FixedUpdate()
    {
        CheckGrapplablePoints();

    }

    private void CheckGrapplablePoints()
    {
        _currentGrappablePointsAmount = Physics.OverlapSphereNonAlloc(transform.position, _detectGrapplablePointRadius, _grapplablePoints, _grappableLayer);
    }

    private void StartGrapple()
    {
        if (_currentGrappablePointsAmount > 0 && !_joint)
        {
            _playerMovement.ChangeMovementParamsOnStartGrappling();

            // Define grappable point
            Vector3 nearestGrappablePointPosition = Vector3.zero;
            float currentMinDistance = float.MaxValue;
            for (int i = 0; i < _currentGrappablePointsAmount; i++)
            {
                if (Vector3.Distance(transform.position, _grapplablePoints[i].transform.position) < currentMinDistance)
                {
                    currentMinDistance = Vector3.Distance(transform.position, _grapplablePoints[i].transform.position);
                    nearestGrappablePointPosition = _grapplablePoints[i].transform.position;
                }
            }

            _currentGrapplablePoint = nearestGrappablePointPosition;

            // Connect and set joints
            _joint = player.gameObject.AddComponent<SpringJoint>();
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedAnchor = _currentGrapplablePoint;
            _joint.minDistance = currentMinDistance * 0.25f;
            _joint.maxDistance = currentMinDistance * 0.8f;

            _joint.spring = 4.5f;
            _joint.damper = 7f;
            _joint.massScale = 4.5f;

            _lineRenderer.positionCount = 2;
        }
    }

    private void StopGrapple()
    {
        _playerMovement.RestoreMovementParamsOnStartGrappling();
        _lineRenderer.positionCount = 0;
        Destroy(_joint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectGrapplablePointRadius);
    }
}
