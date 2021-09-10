//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.ARSubsystems;
//using UnityEngine.XR.ARFoundation;
//using Unity.Collections.LowLevel.Unsafe;
//using System;
//public class PointCloud : ARTrackable<XRPointCloud, ARPointCloud>
//{
//    XRPointCloudData _data;
//    bool _pointUpdated = false;

//    public event Action<ARPointCloudUpdatedEventArgs> updated;

//    public NativeSlice<Vector3>? positions
//    {
//        get
//        {
//            if(_data.positions.IsCreated)
//            {
//                return _data.positions;
//            }
//            return null;
//        }
//    }

//    public NatviesSlice<ulong>? identifiers
//    {
//        get
//        {
//            if(_data.identifiers.IsCreated)
//            {
//                return _data.identifiers;
//            }
//            return null;
//        }
//    }

//    public NativeArray<float>? confidenceValues
//    {
//        get
//        {
//            if(_data.confidenceValues.IsCreated)
//            {
//                return _data.confidenceValues;
//            }
//            return null;
//        }
//    }    

//    // Update is called once per frame
//    void Update()
//    {
//        if(_pointUpdated && updated != null)
//        {
//            _pointUpdated = false;
//            updated(new ARPointCloudUpdatedEventArgs());
//        }
//    }

//    private void OnDestroy()
//    {
//        _data.Dispose();
//    }

//    internal void UpdateData(XRDepthSubsystem subsystem)
//    {
//        _data.Dispose();
//        _data = subsystem.GetPointCloudData(trackableId, Allocator.Persistent);
//        _pointUpdated = _data.positions.IsCreated;
//    }
//}
