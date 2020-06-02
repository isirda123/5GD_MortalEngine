using UnityEditor;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    [MenuItem("MyMenu/Do Something with a Shortcut Key %g")]
    static void DoSomethingWithAShortcutKey()
    {
        if (Selection.transforms[0].CompareTag("Hexagone"))
        {
            Selection.transforms[0].parent.GetComponent<TilesManager>().SetAllTiles();
        }
        Debug.Log("Set tiles");
    }
}
