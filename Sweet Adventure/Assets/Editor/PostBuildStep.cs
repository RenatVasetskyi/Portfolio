using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.IO;
using UnityEditor.iOS.Xcode;

namespace EditorExtensionsSpace
{
    public class PostBuildStep
    {
        // private const string TrackingDescription = "Your data will be used to provide you a better and personalized ad experience.";
 
        [PostProcessBuild(0)]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode)
        {
            if (buildTarget == BuildTarget.iOS)
                AddPListValues(pathToXcode);
        }
 
        static void AddPListValues(string pathToXcode) 
        {
            // string plistPath = pathToXcode + "/Info.plist";
            // PlistDocument plistObj = new PlistDocument();
 
            // plistObj.ReadFromString(File.ReadAllText(plistPath));
 
            // PlistElementDict plistRoot = plistObj.root;
 
            // plistRoot.SetString("NSUserTrackingUsageDescription", TrackingDescription);
 
            // File.WriteAllText(plistPath, plistObj.WriteToString());
        }
    }
}