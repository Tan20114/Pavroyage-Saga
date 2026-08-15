using UnityEditor;
using UnityEngine;

public abstract class BaseInspector : Editor
{
    protected string DrawString(string label, string value)
    {
        return EditorGUILayout.TextField(label, value);
    }

    protected int DrawInt(string label, int value)
    {
        return EditorGUILayout.IntField(label, value);
    }

    protected int DrawInt(string label, int value, bool useSlider, int min, int max)
    {
        return useSlider
            ? EditorGUILayout.IntSlider(label, value, min, max)
            : EditorGUILayout.IntField(label, value);
    }

    protected float DrawFloat(string label, float value)
    {
        return EditorGUILayout.FloatField(label, value);
    }

    protected float DrawFloat(
        string label,
        float value,
        bool useSlider,
        float min,
        float max)
    {
        return useSlider
            ? EditorGUILayout.Slider(label, value, min, max)
            : EditorGUILayout.FloatField(label, value);
    }

    protected bool DrawBool(string label, bool value)
    {
        return EditorGUILayout.Toggle(label, value);
    }

    protected void DrawHeader(string label)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }

    protected void DrawInfo(string name, string value)
    {
        EditorGUILayout.LabelField(name, value, EditorStyles.label);
    }
}