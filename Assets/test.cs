﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        StartCoroutine(YleAPI.Init().Items().Get(data => { Debug.Log(data); }));
    }

    // Update is called once per frame
    void Update()
    {
    }
}