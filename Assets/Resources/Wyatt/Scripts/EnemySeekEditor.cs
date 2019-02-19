using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (EnemySeek))]
public class EnemySeekEditor : Editor
{
    private void OnSceneGUI()
    {
       EnemySeek FOV = (EnemySeek)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(FOV.transform.position, Vector3.up, Vector3.forward, 360, FOV.viewRad);
        Vector3 viewAngleA = FOV.DirectionFromAng(-FOV.viewAng / 2, false);
        Vector3 viewAngleB = FOV.DirectionFromAng(FOV.viewAng / 2, false);

        Handles.DrawLine(FOV.transform.position, FOV.transform.position + viewAngleA * FOV.viewRad);
        Handles.DrawLine(FOV.transform.position, FOV.transform.position + viewAngleB * FOV.viewRad);
        Handles.color = Color.blue;
        foreach(Transform visibleTarget in FOV.visibleTargets)
        {
            Handles.DrawLine(FOV.transform.position, visibleTarget.position);
        }
    }
}
