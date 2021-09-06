//using System.Linq;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleARCore;
//public class PlanePoint : MonoBehaviour
//{
//    public Color PointColor;
//    public int MaxPointsToAddPerFrame = 1;
//    public float _maxPointCount = 100;
//    private int _defaultSize = 10;
//    private Mesh _mesh;
//    private MeshRenderer _meshRenderer;
//    private int _screenWidthId;
//    private int _screenHeightId;
//    private int _colorId;
//    private MaterialPropertyBlock _propertyBlock;
//    private Resolution _cachedResolution;
//    private Color _cachedColor;
//    private LinkedList<PointInfo> _cachedPoints;
//    // 장애물 기준점 결정
//    private float _criteria;
//    void Start()
//    {
//        _meshRenderer = GetComponent<MeshRenderer>();
//        _mesh = GetComponent<MeshFilter>().mesh;
//        if (_mesh == new Mesh())
//        {
//            _mesh = new Mesh();
//        }
//        _mesh.Clear();
//        _cachedColor = PointColor;

//        _screenWidthId = Shader.PropertyToID("_ScreenWidth");
//        _screenHeightId = Shader.PropertyToID("_ScreenHeight");
//        _colorId = Shader.PropertyToID("_PlaneColor");

//        _propertyBlock = new MaterialPropertyBlock();
//        _meshRenderer.GetPropertyBlock(_propertyBlock);
//        _propertyBlock.SetColor(_colorId, _cachedColor);
//        _meshRenderer.SetPropertyBlock(_propertyBlock);

//        _cachedPoints = new LinkedList<PointInfo>();
//    }

//    /// <summary>
//    /// The Unity OnDisable() method.
//    /// </summary>
//    public void OnDisable()
//    {
//        ClearCachedPoints();
//    }

//    /// <summary>
//    /// The Unity Update() method.
//    /// </summary>
//    public void Update()
//    {
//        // If ARCore is not tracking, clear the caches and don't update.
//        if (Session.Status != SessionStatus.Tracking)
//        {
//            ClearCachedPoints();
//            return;
//        }

//        if (Screen.currentResolution.height != _cachedResolution.height
//            || Screen.currentResolution.width != _cachedResolution.width)
//        {
//            UpdateResolution();
//        }

//        if (_cachedColor != PointColor)
//        {
//            UpdateColor();
//        }
//        else
//        {
//            AddAllPointsToCache();
//        }
//        //_criteria = GroundDetection.ObstacleCriteria();
//        _criteria = GroundDetection.real_y;
//        UpdateMesh();
//    }

//    /// <summary>
//    /// Clears all cached feature points.
//    /// </summary>
//    private void ClearCachedPoints()
//    {
//        _cachedPoints.Clear();
//        _mesh.Clear();
//    }

//    /// <summary>
//    /// Updates the screen resolution.
//    /// </summary>
//    private void UpdateResolution()
//    {
//        _cachedResolution = Screen.currentResolution;
//        if (_meshRenderer != null)
//        {
//            _meshRenderer.GetPropertyBlock(_propertyBlock);

//            _propertyBlock.SetFloat(_screenWidthId, _cachedResolution.width);
//            _propertyBlock.SetFloat(_screenHeightId, _cachedResolution.height);

//            _meshRenderer.SetPropertyBlock(_propertyBlock);
//        }
//    }

//    /// <summary>
//    /// Updates the color of the feature points.
//    /// </summary>
//    private void UpdateColor()
//    {
//        _cachedColor = PointColor;
//        _meshRenderer.GetPropertyBlock(_propertyBlock);
//        _propertyBlock.SetColor("_PlaneColor", _cachedColor);
//        _meshRenderer.SetPropertyBlock(_propertyBlock);
//    }

//    /// <summary>
//    /// Adds all points from this frame's pointcloud to the cache.
//    /// </summary>
//    private void AddAllPointsToCache()
//    {
//        if (Frame.PointCloud.IsUpdatedThisFrame)
//        {
//            for (int i = 0; i < Frame.PointCloud.PointCount; i++)
//            {
//                AddPointToCache(Frame.PointCloud.GetPointAsStruct(i));
//            }
//        }
//    }

//    /// <summary>
//    /// Adds the specified point to cache.
//    /// </summary>
//    /// <param name="point">A feature point to be added.</param>
//    private void AddPointToCache(Vector3 point)
//    {
//        if (_cachedPoints.Count >= _maxPointCount)
//        {
//            _cachedPoints.RemoveFirst();
//        }
//        if (!ROI.RoiCheck(point)) return;
//        if (Mathf.Abs(_criteria - point.y) <= GroundDetection._outlier)
//        {
//            _cachedPoints.AddLast(new PointInfo(point, new Vector2(_defaultSize, _defaultSize),
//                                                     Time.time));

//        }
//    }
//    /// <summary>
//    /// Updates the mesh, adding the feature points.
//    /// </summary>
//    private void UpdateMesh()
//    {
//        _mesh.Clear();
//        _mesh.vertices = _cachedPoints.Select(p => p.Position).ToArray();
//        _mesh.uv = _cachedPoints.Select(p => p.Size).ToArray();
//        _mesh.SetIndices(Enumerable.Range(0, _cachedPoints.Count).ToArray(),
//                          MeshTopology.Points, 0);
//    }

//    private struct PointInfo
//    {
//        /// <summary>
//        /// The position of the point.
//        /// </summary>
//        public Vector3 Position;

//        /// <summary>
//        /// The size of the point.
//        /// </summary>
//        public Vector2 Size;

//        /// <summary>
//        /// The creation time of the point.
//        /// </summary>
//        public float CreationTime;

//        public PointInfo(Vector3 position, Vector2 size, float creationTime)
//        {
//            Position = position;
//            Size = size;
//            CreationTime = creationTime;
//        }
//    }
//}