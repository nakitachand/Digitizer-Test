using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ReticleController : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager planeManager;

    [SerializeField]
    private ARRaycastManager rayCastManager;

    [SerializeField]
    private GameObject recticlePrefab;

    private GameObject recticle;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //public FurnitureConfig selectedFurniture;

    [SerializeField]
    private Transform cameraTransform;

    private void Awake()
    {
        recticle = Instantiate(recticlePrefab);
        recticle.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {

        Vector2 screenCenter = ScreenUtils.GetScreenCenter();

        ////Raycast returns true/false
        //if (rayCastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        //{
        //    //reposition the recticle
        //    RepositionRecticle();
        //}

    }

    //private void RepositionRecticle()
    //{
    //    //saves the first detected position/rotation to variable pose
    //    Pose pose = hits[0].pose;

    //    //restricts orientation of detected planes via dot product of ray vector to plane's normal vector
    //    Vector3 cameraDirection = cameraTransform.position - pose.position;
    //    Vector3 planeNormalVector = pose.rotation * Vector3.up;

    //    if (Vector3.Dot(cameraDirection, planeNormalVector) >= 0)
    //    {
    //        recticle.transform.SetPositionAndRotation(pose.position, pose.rotation);
    //        recticle.SetActive(true);
    //    }
    //    else
    //    {
    //        recticle.SetActive(false);
    //    }


    //}
}
