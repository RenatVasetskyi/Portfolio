using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    public class CashCleaner : MonoBehaviour
    {
        [MenuItem("Window/Clean Cash")]
        public static void CleanCash()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}