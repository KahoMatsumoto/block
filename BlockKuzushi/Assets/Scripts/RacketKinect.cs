﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Kinect = Windows.Kinect;

public class RacketKinect : MonoBehaviour
{

    public GameObject BodySourceManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();

    private BodySourceManager _BodyManager;

    // githubてすと
    //GameObject bE;
    GameObject jointObj;

    // Use this for initialization
    void Start()
    {
        //      this.bE = GameObject.Find("bulletE");
        //this.spnMid = GameObject.Find("spnMid");
    }

    // Update is called once per frame
    void Update()
    {
        if (BodySourceManager == null)
        {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds)
        {
            if (!trackedIds.Contains(trackingId))
            {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                if (!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }

                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }



        //マウスのx,y座標を取得
        Vector3 vecSpineMid = jointObj.transform.position;
        //ワールド座標に変換じゃ！！
        //Vector3 screenPos = Camera.main.ScreenToWorldPoint(vecMouse);
        //オブジェクトに代入じゃ！！
        Vector3 racketPos = new Vector3(vecSpineMid.x, 0, -6);
        transform.position = racketPos;


        //      JugdeE (bE);


    }

    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);

        for (Kinect.JointType jt = Kinect.JointType.SpineMid; jt <= Kinect.JointType.SpineMid; jt++)
        {
            jointObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            //lr.SetVertexCount(2);
            //lr.material = BoneMaterial;
            //lr.SetWidth(0.05f, 0.05f);

            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();
            jointObj.transform.parent = body.transform;
        }

        return body;
    }
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        Vector3 center = GetVector3FromJoint(body.Joints[Kinect.JointType.SpineBase]);

        for (Kinect.JointType jt = Kinect.JointType.SpineMid; jt <= Kinect.JointType.SpineMid; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            //    Kinect.Joint? targetJoint = null;

            //            if (_BoneMap.ContainsKey(jt))
            //          {
            //            targetJoint = body.Joints[_BoneMap[jt]];
            //      }

            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);
            /*
            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if (targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.SetColors(GetColorForState(sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                lr.enabled = false;
            }
            */
        }
    }

    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
