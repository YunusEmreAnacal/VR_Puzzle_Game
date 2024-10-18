using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SwitchControl))]
public class SwitchControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Varsayýlan inspector'u göster
        DrawDefaultInspector();

        SwitchControl switchControl = (SwitchControl)target;

        // Bir baþlýk ekleyelim
        GUILayout.Label("Elektrik Devresi Test Araçlarý");

        // Mavi anahtarý test etme butonu
        if (GUILayout.Button("Test Blue Switch"))
        {
            switchControl.OnSwitchTouched("Blue");
        }

        // Kýrmýzý anahtarý test etme butonu
        if (GUILayout.Button("Test Red Switch"))
        {
            switchControl.OnSwitchTouched("Red");
        }

        // Siyah anahtarý test etme butonu
        if (GUILayout.Button("Test Black Switch"))
        {
            switchControl.OnSwitchTouched("Black");
        }

        // Sarý anahtarý test etme butonu
        if (GUILayout.Button("Test Yellow Switch"))
        {
            switchControl.OnSwitchTouched("Yellow");
        }

        // Þalterleri sýfýrlama butonu
        if (GUILayout.Button("Reset Switches"))
        {
            switchControl.ResetSwitches();
        }
    }
}
