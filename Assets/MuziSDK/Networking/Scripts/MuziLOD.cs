using Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MuziLODLevel
{
    OUT_OF_VIEW = 0,
    FAR = 1,
    MIDDLE = 2,
    NEAR = 3,
    CLOSE = 4,
    DETAIL = 5,
}

public class MuziLOD : MonoBehaviour
{
    public static float[] MuziLODDistanceByLevelLower = { 256, 128, 64, 32, 8, 0 };
    public static float[] MuziLODDistanceByLevelUpper = { 32768, 256, 128, 64, 32, 8 };

    public event Action<MuziLODLevel> OnMuziLODLevelChanged;
    Transform myNetworkPlayer;
    public MuziLODLevel myMuziLODLevel = MuziLODLevel.OUT_OF_VIEW;
    public float TimeIntervalCheck = -1;
    private void Start()
    {
        OnMuziLODLevelChanged?.Invoke(myMuziLODLevel);
        if (TimeIntervalCheck > 0)
            StartCoroutine(AutoCheckInterval());
    }
    IEnumerator AutoCheckInterval()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, TimeIntervalCheck)); // avoid all muziLOD objects calculate at once
        while (true)
        {
            yield return new WaitForSeconds(TimeIntervalCheck);
            ReCheckNakamaLODLevel();
        }

    }

    public void ReCheckNakamaLODLevel()
    {
        if (myNetworkPlayer == null) myNetworkPlayer = NakamaMyNetworkPlayer.instance.transform;// myNetworkPlayer = NakamaContentManager.instance.controllablePlayer.transform;
        float distance = Vector3.Distance(myNetworkPlayer.position, transform.position);

        MuziLODLevel myMuziLODLeveltmp = MuziLODLevel.OUT_OF_VIEW;
        if (distance >= MuziLODDistanceByLevelLower[1]) myMuziLODLeveltmp = MuziLODLevel.FAR;
        else if (distance >= MuziLODDistanceByLevelLower[2]) myMuziLODLeveltmp = MuziLODLevel.MIDDLE;
        else if (distance >= MuziLODDistanceByLevelLower[3]) myMuziLODLeveltmp = MuziLODLevel.NEAR;
        else if (distance >= MuziLODDistanceByLevelLower[4]) myMuziLODLeveltmp = MuziLODLevel.CLOSE;
        else if (distance >= MuziLODDistanceByLevelLower[5]) myMuziLODLeveltmp = MuziLODLevel.DETAIL;
        if (myMuziLODLeveltmp != myMuziLODLevel)
        {
            myMuziLODLevel = myMuziLODLeveltmp;
            OnMuziLODLevelChanged?.Invoke(myMuziLODLeveltmp);
        }

    }
}

