using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationData", menuName = "ScriptableObjects/AnimationData", order = 1)]
public class AnimationData : ScriptableObject
{
    public AnimationClip animationClip;
    public float fadeTime;
    public float startTime;
    public float earlyExitTime;
    public float speedFactor;
}