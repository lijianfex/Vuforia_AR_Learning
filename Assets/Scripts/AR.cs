using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// 识别状态检测
/// </summary>
public class AR : MonoBehaviour, ITrackableEventHandler
{


    protected TrackableBehaviour mTrackableBehaviour;

    public GameObject AIXIPrefab;//模型预制

    public GameObject RFX1Prefab1;//特效1预制
    public GameObject RFX1Prefab2;//特效2预制

    private GameObject aixi;//角色

    private GameObject fx1;//特效1
    private GameObject fx2;//特效2

    private AudioSource audioSource;//播放源
    public AudioClip wecomeClip;//欢迎音效片段

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
            
        }
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }


    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS



    /// <summary>
    ///  状态的改变
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
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        aixi = GameObject.Instantiate(AIXIPrefab,transform.localPosition, transform.localRotation);        
        aixi.transform.SetParent(this.transform);        

        fx1 = GameObject.Instantiate(RFX1Prefab1,transform.position, transform.localRotation);
        fx2 = GameObject.Instantiate(RFX1Prefab2,transform.position, transform.localRotation);
        fx1.transform.position = this.transform.position;
        fx2.transform.position = this.transform.position;
        fx1.transform.SetParent(this.transform);
        fx2.transform.SetParent(this.transform);

        Destroy(fx1, 5f);
        Destroy(fx2, 5f);
        Invoke("PlayWecomeClip", 5.0f);

    }

    private void PlayWecomeClip()
    {

        audioSource.PlayOneShot(wecomeClip);
        Debug.Log(mTrackableBehaviour.CurrentStatus);
        
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
        if (fx1 != null)
        {
            Destroy(fx1);
        }
        if (fx2 != null)
        {
            Destroy(fx2);
        }

    }


}