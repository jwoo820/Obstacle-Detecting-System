using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using UnityEngine.XR.ARSubsystems;
public class PointCloudVisualization : MonoBehaviour
{
    ARPointCloud _pointCloud;

    ParticleSystem _particleSystem;
    ParticleSystem.Particle[] _particles;
    int _numParticles;
    static List<Vector3> _vertices = new List<Vector3>();
    static List<Vector3> _obstaclePoints = new List<Vector3>();
    public static int _obstaclePointNum;
    private float _criteria;
    void OnPointCloudChanged(ARPointCloudUpdatedEventArgs eventArgs)
    {
        var points = _vertices;
        points.Clear();
        _criteria = ClassificationPlane._referenceY;
        if (_pointCloud.positions.HasValue)
        {
            foreach (var point in _pointCloud.positions.Value)
            {
                for(int i=0; i< points.Count; ++i)
                {
                    _vertices.Add(point);
                    if (Mathf.Abs(_criteria - points[i].y) > ClassificationPlane._outlier)
                    {
                        _obstaclePoints.Add(point);
                    }
                }
                Debug.Log("Point Cloud Number : " + _vertices.Count);
                Debug.Log("Obstacle Point : " + _obstaclePoints.Count);
            }
        }
        int numParticles = points.Count;
        if (_particles == null || _particles.Length < numParticles)
            _particles = new ParticleSystem.Particle[numParticles];

        var color = _particleSystem.main.startColor.color;
        var obstacleColor = Color.red;
        var size = _particleSystem.main.startSize.constant;

        for (int i = 0; i < numParticles; ++i)
        {
            if (Mathf.Abs(_criteria - points[i].y) > ClassificationPlane._outlier)
            {
                //if (_pointCloud.positions.HasValue)
                //{
                //    foreach (var point in _pointCloud.positions.Value)
                //    {
                //        _obstaclePoints.Add(point);
                //    }
                //}
                //_obstaclePointNum = _obstaclePoints.Count;
                _particles[i].startColor = obstacleColor;
                _particles[i].startSize = size;
                _particles[i].position = points[i];
                _particles[i].remainingLifetime = 1f;
                //Debug.Log("Current ObstaclePoint Num :  " + _obstaclePointNum);
            }
            else
            {
                _particles[i].startColor = color;
                _particles[i].startSize = size;
                _particles[i].position = points[i];
                _particles[i].remainingLifetime = 1f;
            }
        }

        for (int i = numParticles; i < _numParticles; ++i)
        {
            _particles[i].remainingLifetime = -1f;
        }
        _particleSystem.SetParticles(_particles, Math.Max(numParticles, _numParticles));
        _numParticles = numParticles;
    }

    private void Awake()
    {
        _pointCloud = GetComponent<ARPointCloud>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _pointCloud.updated += OnPointCloudChanged;
        UpdateVisibility();
    }

    private void OnDisable()
    {
        _pointCloud.updated -= OnPointCloudChanged;
        UpdateVisibility();
    }

    private void Update()
    {
        UpdateVisibility();
    }

    void UpdateVisibility()
    {
        var visible =
            enabled &&
            (_pointCloud.trackingState != TrackingState.None);

        SetVisible(visible);
    }

    void SetVisible(bool visible)
    {
        if (_particleSystem == null)
            return;

        var renderer = _particleSystem.GetComponent<Renderer>();
        if (renderer != null)
            renderer.enabled = visible;
    }
}
