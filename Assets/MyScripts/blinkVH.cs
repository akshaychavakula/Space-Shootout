﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkVH : MonoBehaviour {


    private SkinnedMeshRenderer smr;
    public GameObject body;

    private float openLim = 5;
    private float closeLim = 0.05f;
    private float timerOpen;
    private int blendshapeVal = 200;
    private bool isOpen = true;

	void Start () {
        smr = body.GetComponent<SkinnedMeshRenderer>();
    }
	
	void Update () {

        if (!smr) {
            smr = body.GetComponent<SkinnedMeshRenderer>();
        }

        if (isOpen) {
            timerOpen += Time.deltaTime;
            if (timerOpen > openLim) {
                isOpen = false;
                timerOpen = 0;
            }
        }
        else {
            StartCoroutine(blinkEye());
        }
    }
    IEnumerator openSlowly() {
        for (int i = blendshapeVal; i>=0; i-=50) {
            smr.SetBlendShapeWeight(0, i);
            smr.SetBlendShapeWeight(1, i);
            yield return null;
        }
    }
    IEnumerator closeSlowly() {
        for (int i = 0; i<=200; i+=50) {
            smr.SetBlendShapeWeight(0, i);
            smr.SetBlendShapeWeight(1, i);
            yield return null;
        }
    }
    IEnumerator blinkEye() {
        StartCoroutine(closeSlowly());
        yield return new WaitForSeconds(closeLim);
        StartCoroutine(openSlowly());
        openLim = Random.Range(4, 8);
        isOpen = true;
    }

}

