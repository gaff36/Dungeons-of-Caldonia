using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State")]
public class D_PlayerDetectedState : ScriptableObject
{
    public float actionTime = 1.5f;
    public float waitForTurnTime = 0.5f;
}
