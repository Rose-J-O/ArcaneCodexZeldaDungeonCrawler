#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueCreatorWindow : EditorWindow
{
    private static readonly Vector2 WindowMinSize = new Vector2(610, 250);


    // Adds menu option under "Tools/Inventory"
    [MenuItem("Tools/Dialogue/Create Sequence")]
    public static void Open()
    {
        // Creates and shows the editor window
        var window = GetWindow<DialogueCreatorWindow>("Create Sequence");
        window.minSize = WindowMinSize;
    }
}
#endif