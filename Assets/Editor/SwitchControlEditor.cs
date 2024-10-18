using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SwitchControl))]
public class SwitchControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Varsay�lan inspector'u g�ster
        DrawDefaultInspector();

        SwitchControl switchControl = (SwitchControl)target;

        // Bir ba�l�k ekleyelim
        GUILayout.Label("Elektrik Devresi Test Ara�lar�");

        // Mavi anahtar� test etme butonu
        if (GUILayout.Button("Test Blue Switch"))
        {
            switchControl.OnSwitchTouched("Blue");
        }

        // K�rm�z� anahtar� test etme butonu
        if (GUILayout.Button("Test Red Switch"))
        {
            switchControl.OnSwitchTouched("Red");
        }

        // Siyah anahtar� test etme butonu
        if (GUILayout.Button("Test Black Switch"))
        {
            switchControl.OnSwitchTouched("Black");
        }

        // Sar� anahtar� test etme butonu
        if (GUILayout.Button("Test Yellow Switch"))
        {
            switchControl.OnSwitchTouched("Yellow");
        }

        // �alterleri s�f�rlama butonu
        if (GUILayout.Button("Reset Switches"))
        {
            switchControl.ResetSwitches();
        }
    }
}
