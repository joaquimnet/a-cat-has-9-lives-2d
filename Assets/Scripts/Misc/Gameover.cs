using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    float start;

    private void Start()
    {
        start = Time.time;
    }

    void Update()
    {
        if (Time.time - start < 2f)
        {
            return;
        }
        if (Input.GetAxis("Jump") > 0.5f || Input.GetAxis("Cancel") > 0.5f)
        {
            GameMaster.instance.LoadSpecificLevel(0);
        }
    }
}
