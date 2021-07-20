using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum States
    {
        NORMAL,
        DYING,
    }

    public States current;

    public void Set(States state)
    {
        current = state;
    }

    public static PlayerState instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
    }
}
