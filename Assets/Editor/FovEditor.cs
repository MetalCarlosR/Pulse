using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineaDeVison))]
public class FovEditor : Editor
{
    private void OnSceneGUI()
    {
        LineaDeVison FOV = (LineaDeVison)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(FOV.transform.position, Vector3.forward, Vector3.up, 360, FOV.radioVision);

        Vector3 AnguloDer = FOV.ApuntarAngulo(FOV.anguloVision / 2);
        Vector3 AnguloIz = FOV.ApuntarAngulo(-FOV.anguloVision / 2);

        Handles.color = Color.red;
        Handles.DrawLine(FOV.transform.position, FOV.transform.position + AnguloDer * FOV.radioVision);
        Handles.DrawLine(FOV.transform.position, FOV.transform.position + AnguloIz * FOV.radioVision);
    }
}
