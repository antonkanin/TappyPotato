using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace TappyPotato.Building
{
    public static class BuildPostProcessor
    {
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string pathToBuildProject)
        {
            if (target == BuildTarget.iOS)
            {
                // get plist
                string plistPath = pathToBuildProject + "/Info.plist";
                PlistDocument plist = new PlistDocument();
                plist.ReadFromFile(plistPath);

                PlistElementDict allowsDict = plist.root.CreateDict("NSAppTransportSecurity");
                allowsDict.SetBoolean("NSAllowsArbitraryLoads", true);

                PlistElementDict exceptionsDict = allowsDict.CreateDict("NSExceptionDomains");
                PlistElementDict domainDict = exceptionsDict.CreateDict("antonkanin.com");
                domainDict.SetBoolean("NSExceptionAllowsInsecureHTTPLoads", true);
                domainDict.SetBoolean("NSIncludesSubdomains", true);
                
                // write to file
                plist.WriteToFile(plistPath);
            }
        }
        
    }
}