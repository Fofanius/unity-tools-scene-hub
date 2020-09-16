using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class UploadPackageEditor : EditorWindow
{
    private string _version;
    private string _branchName;

    [MenuItem("Utils/Upload Package")]
    private static void OpenWindow() => GetWindow<UploadPackageEditor>().Show();

    private void OnEnable()
    {
        _version = "1.0.0";
        _branchName = "upm";
    }

    private void OnGUI()
    {
        _version = EditorGUILayout.TextField("Version", _version);
        _branchName = EditorGUILayout.TextField("Branch name", _branchName);
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Upload"))
        {
            UploadProcess();
        }
    }

    private void UploadProcess()
    {
        var cmd = RunConsole();

        using (var sw = cmd.StandardInput)
        {
            sw.WriteLine($"git subtree split --prefix=Assets/SceneHub --branch {_branchName}");
            sw.WriteLine($"git tag {_version} {_branchName}");
            sw.WriteLine($"git push origin {_branchName} --tags");
        }

        cmd.WaitForExit();
    }

    private static Process RunConsole()
    {
        var process = new Process
        {
            StartInfo =
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = false,
                UseShellExecute = false
            }
        };


        process.Start();
        return process;
    }
}