//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OvrLaserPointer : MonoBehaviour
{
    LineRenderer _lr;
    /// Color of the laser pointer including alpha transparency
    public Color laserStartColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    public Color laserEndColor = new Color(1.0f, 1.0f, 1.0f, 0.01f);

    public GameObject reticle;
    float reticleScaleMultiplier = 0.1f;
    Vector3 _finalPoint;
    float depth;
    float _scale;
    public float DefaultDepth = 5;

    /// Sorting order to use for the reticle's renderer.
    /// Range values come from https://docs.unity3d.com/ScriptReference/Renderer-sortingOrder.html.
    //[Range(-32767, 32767)]
    //public int reticleSortingOrder = 32767;

    void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        //_lr.sortingOrder = reticleSortingOrder;
        if (reticle != null)
        {
            Renderer reticleRenderer = reticle.GetComponent<Renderer>();
            //reticleRenderer.sortingOrder = reticleSortingOrder;
        }
    }

    void Start()
    {
        UpdateLaserPointerProperties();
    }

    void LateUpdate()
    {
        UpdateLaserPointerProperties();
    }

    private void UpdateLaserPointerProperties()
    {
        _lr.startColor = laserStartColor;
        _lr.endColor = laserEndColor;
        _lr.SetPosition(0, transform.position);

        if (!OVRGazePointer.instance.hidden)
        {
            _finalPoint = OVRGazePointer.instance.transform.position;
            reticle.SetActive(false);
        }
        else
        {
            _finalPoint = OVRGazePointer.instance.rayTransform.position + OVRGazePointer.instance.rayTransform.forward * DefaultDepth;
            reticle.SetActive(true);
        }
        
        _lr.SetPosition(1, _finalPoint);
        reticle.transform.position = _finalPoint;
        depth = (_lr.GetPosition(0) - _lr.GetPosition(1)).magnitude;
        _scale = (_lr.GetPosition(0) - _lr.GetPosition(1)).magnitude * reticleScaleMultiplier;
        reticle.transform.localScale = new Vector3(_scale,_scale,_scale);
    }
}
