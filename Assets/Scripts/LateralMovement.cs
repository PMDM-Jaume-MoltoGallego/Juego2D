using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para el movimiento de las plataformas
public class LateralMovement : MonoBehaviour
{
    public Vector3 destination;
    public float time;
    // Use this for initialization
    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", destination,
        "time", time,
        "easetype", iTween.EaseType.easeInOutSine,
        "looptype", iTween.LoopType.pingPong));
    }
}