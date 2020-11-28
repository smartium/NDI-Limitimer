using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Auto build por Felipe Brito
/// Automação do processo de build sem a necessidade de troca de plataforma
/// É criada uma pasta com a versão definida no Player Settings
/// </summary>

public class AutoBuild : MonoBehaviour
{

  public static void Build(BuildTarget buildTarget, string extension)
  {
    string path = "Build/" +
    Application.version + "/" +
    buildTarget + "/" +
    Application.productName + extension;

    BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
    {
      locationPathName = path,
      target = buildTarget,
      options = BuildOptions.CompressWithLz4
    };
    BuildPipeline.BuildPlayer(buildPlayerOptions);
  }


  [MenuItem("Builds/Todas", priority = 0)]
  public static void BuildAll()
  {
    BuildMac();
    BuildWebGL();
    BuildAndroid();
    BuildWindows();
  }

  [MenuItem("Builds/Mac")]
  public static void BuildMac()
  {
    Build(BuildTarget.StandaloneOSX, ".app");
  }

  [MenuItem("Builds/Windows")]
  public static void BuildWindows()
  {
    Build(BuildTarget.StandaloneWindows64, ".exe");
  }

  [MenuItem("Builds/Android")]
  public static void BuildAndroid()
  {
    Build(BuildTarget.Android, ".apk");
  }

  [MenuItem("Builds/WebGL")]
  public static void BuildWebGL()
  {
    Build(BuildTarget.WebGL, "");
  }


}
