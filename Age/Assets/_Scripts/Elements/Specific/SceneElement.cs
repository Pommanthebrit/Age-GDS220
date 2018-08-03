﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneElement : BaseElement
{
    private SeasonManager _seasonManager;
    [SerializeField] private AirElement _Air;
    [SerializeField] private EarthElement _Earth;
    [SerializeField] private FireElement _Fire;
    [SerializeField] private WaterElement _Water;

    [Header("Clouds")]
    private ParticleSystem.EmissionModule _cloudEmissionModule;
    [SerializeField] private float auRate, wnRate, spRate;

    [Header("Light")]
    [SerializeField] private GameObject _sceneLight;
    [SerializeField] private Vector3 smRotate, auRotate, wnRotate, spRotate;
    
    private void Start()
    {
        _seasonManager = GameObject.FindGameObjectWithTag("GameGod").GetComponent<SeasonManager>();

        _cloudEmissionModule = _Fire._cloudsPT.emission;
    }

    public override void Interact()
    {
        base.Interact();

        StartCoroutine(_seasonManager.ChangeSeason());
    }

    protected override void EnactSummerActions(bool initialAction)
    {
        _cloudEmissionModule.rateOverTime = auRate;

        StartCoroutine (LightRotate(auRotate, 5f));
    }

    protected override void EnactAutumnActions(bool initialAction)
    {
        _cloudEmissionModule.rateOverTime = wnRate;

        StartCoroutine(LightRotate(wnRotate, 5f));
    }

    protected override void EnactWinterActions(bool initialAction)
    {
        _cloudEmissionModule.rateOverTime = spRate;

        StartCoroutine(LightRotate(spRotate, 5f));

        foreach (ParticleSystem p in _Air._dandelionStillPT)
        {
            p.Play();
        }
        
    }

    protected override void EnactSpringActions(bool initialAction)
    {
		
    }

    private IEnumerator LightRotate (Vector3 rotateVector, float time)
    {
        float currentTime = 0.0f;

        Quaternion RotateTo = Quaternion.Euler(new Vector3(rotateVector.x, rotateVector.y, rotateVector.z));

        do
        {
            _sceneLight.transform.rotation = Quaternion.Slerp(_sceneLight.transform.rotation, RotateTo, Time.deltaTime / time);

            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);
    }
}