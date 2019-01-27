﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class Transition : MonoBehaviour
{
    [SerializeField]
    private Material transitionMaterial;
    private Material _transM;
    //[SerializeField]
    private string nextLevel;
    private bool isTransitioning = false;
    private bool done = false;
    private float t = 0.0f;
    [SerializeField]
    private AudioSource aud;
    [SerializeField]
    private AudioSource bgm;

    void Start()
    {
        _transM = Material.Instantiate(transitionMaterial);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, _transM);
    }

    void Update()
    {
        if (isTransitioning && t <= 1.0f)
        {
            _transM.SetFloat("_Cutoff", t);
            t += 0.01f;
        }
        else if (t > 1.0f)
        {
            SceneManager.LoadScene(nextLevel);
            //_transM.SetFloat("_Cutoff", 0.0f);
        }
    }

    public void ExecuteTransition(string lvl)
    {
        isTransitioning = true;
        nextLevel = lvl;
        if (bgm != null)
        {
            bgm.Pause();
        }
        if (aud != null)
        {
            aud.Play();
        }
    }
}