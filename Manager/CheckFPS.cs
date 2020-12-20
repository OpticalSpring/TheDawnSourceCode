using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFPS : MonoBehaviour
{
    public static CheckFPS instance;
    public bool check;
    private void Awake()
    {
        instance = this;
    }
    #region Public Members
    /// <summary>
    /// The duration while the average FPS will be computed
    /// </summary>
    public float interval = 1;
    #endregion

    #region Private Members
    /// <summary>
    /// The accumulated FPS since the last timeout
    /// </summary>
    private float _accumulationValue;
    /// <summary>
    /// The amount of frames since the last timeout
    /// </summary>
    private int _framesCount;
    /// <summary>
    /// The time of the the last timeout
    /// </summary>
    private float _timestamp;
    /// <summary>
    /// The raw FPS (1/deltaTime)
    /// </summary>
    private float _rawFps;
    /// <summary>
    /// The last computed mean FPS
    /// </summary>
    public float _meanFps;
    #endregion

    #region Overriden base class functions (https://docs.unity3d.com/ScriptReference/MonoBehaviour.html)
    private void Update()
    {
       
        if (Time.time - _timestamp > interval)
        {
            _meanFps = _accumulationValue / _framesCount;
            _timestamp = Time.time;
            _framesCount = 0;
            _accumulationValue = 0;
        }

        ++_framesCount;
        _rawFps = 1.0f / Time.deltaTime;
        _accumulationValue += _rawFps;
    }

    private void OnGUI()
    {
        if (check == false)
        {
            return;
        }
        GUI.color = Color.white;
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Mean FPS over " + interval + " second(s) = " + _meanFps + "\nRaw FPS = " + _rawFps);
    }
    #endregion
}
    

