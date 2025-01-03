using UnityEditor;
using UnityEngine;

namespace EditorExtensionsSpace
{
    public class EditorExtensions : MonoBehaviour
    {
        [MenuItem("Window/Delete All PlayerPrefs")]
        public static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}