using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public static class ProjectBuild
    {
        public static void BuildSinglePoolDemoApp()
        {
            var outdir = Environment.CurrentDirectory + "/Builds/Android";
            var outputPath = Path.Combine(outdir, $"{Application.productName} - {Application.version} - Single.apk");
            
            // Обработка папки
            if (!Directory.Exists(outdir)) Directory.CreateDirectory(outdir);
            if (File.Exists(outputPath)) File.Delete(outputPath);

            // Запускаем проект в один клик
            string[] scenes = {"Assets/BurningLab/ObjectsPool/Examples/Single Pool Example/Scenes/SP_App.unity"};
            BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android, BuildOptions.Development);
            
            if (File.Exists(outputPath))
            {
                Debug.Log("Build Success :" + outputPath);
            }
            else
            {
                Debug.LogException(new Exception("Build Fail! Please Check the log! "));
            }       
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void BuildMultiplePoolDemoApp()
        {
            var outdir = Environment.CurrentDirectory + "/Builds/Android";
            var outputPath = Path.Combine(outdir, $"{Application.productName} - {Application.version} - Multiple.apk");
            
            // Обработка папки
            if (!Directory.Exists(outdir)) Directory.CreateDirectory(outdir);
            if (File.Exists(outputPath)) File.Delete(outputPath);

            // Запускаем проект в один клик
            string[] scenes = {"Assets/BurningLab/ObjectsPool/Examples/Multiple Pools Example/Scenes/MP_App.unity"};
            BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android, BuildOptions.Development);
            
            if (File.Exists(outputPath))
            {
                Debug.Log("Build Success :" + outputPath);
            }
            else
            {
                Debug.LogException(new Exception("Build Fail! Please Check the log! "));
            }       
        }
    }
}