using System.Collections;
using System.Linq;
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
    public static List<Vector3> _obstaclePoints = new List<Vector3>();
    private float _criteria;
    void OnPointCloudChanged(ARPointCloudUpdatedEventArgs eventArgs)
    {
        var points = _vertices;
        //var obstaclePoints = _obstaclePoints;

        points.Clear();
        _obstaclePoints.Clear();

        _criteria = ClassificationPlane._referenceY;
        if (_pointCloud.positions.HasValue)
        {
            foreach (var point in _pointCloud.positions.Value)
            {
                if (CheckRoi.ObstacleCheck(point))
                {
                    if (Mathf.Abs(_criteria - point.y) > ClassificationPlane._outlier)
                    {
                        _obstaclePoints.Add(point);
                    }
                }
                _vertices.Add(point);
            }
        }
        // if -> obstacle 이 20개 이상일때?? -> 이친구의 중심좌표를 저장(연산과정이 필요, 하나의 좌표)
        //_obstaclePointNum = _obstaclePoints.Count; 
        //Debug.Log("Obstacle Num : " + _obstaclePoints.Count);
        // 여기서 vertices는 실시간 계산되서 갯수가 계속해서 바뀜



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
                _particles[i].startColor = obstacleColor;
                _particles[i].startSize = size;
                _particles[i].position = points[i];
                _particles[i].remainingLifetime = 1f;
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
