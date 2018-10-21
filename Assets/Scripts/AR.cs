using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vuforia;


public class AR : MonoBehaviour, ITrackableEventHandler
{


    protected TrackableBehaviour mTrackableBehaviour;

    public GameObject AIXIPrefab;

    public GameObject RFX1Prefab1;
    public GameObject RFX1Prefab2;

    private GameObject aixi;

    private GameObject fx1;
    private GameObject fx2;



    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS



    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            OnTrackingLost();
        }
    }


    /// <summary>
    /// 识别到物体
    /// </summary>
    protected virtual void OnTrackingFound()
    {
        aixi = GameObject.Instantiate(AIXIPrefab);
        aixi.transform.position = this.transform.position;
        aixi.transform.SetParent(this.transform);

        fx1 = GameObject.Instantiate(RFX1Prefab1);
        fx2 = GameObject.Instantiate(RFX1Prefab2);
        fx1.transform.position = this.transform.position;
        fx2.transform.position = this.transform.position;
        fx1.transform.SetParent(this.transform);
        fx2.transform.SetParent(this.transform);

        Destroy(fx1, 5f);
        Destroy(fx2, 5f);

    }


    /// <summary>
    /// 丢失物体
    /// </summary>
    protected virtual void OnTrackingLost()
    {
        if (aixi != null)
        {
            Destroy(aixi);
        }
        if(fx1!=null)
        {
            Destroy(fx1);
        }
        if (fx2 != null)
        {
            Destroy(fx2);
        }

    }


}