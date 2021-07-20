using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Particle
{
    public enum Type
    {
        NONE,
        BLUE_ENERGY,
        POPPER_BLUE,
        POPPER_PINK,
        POPPER_RED,
        POPPER_PURPLE,
    }

    [InspectorName("Type")] public Type particleType;
    public GameObject particle;
}
