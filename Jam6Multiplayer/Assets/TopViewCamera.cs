using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]
public class TopViewCamera : MonoBehaviour
{
    public GameObject target;

    bool fileDialog = false;

    private string currentPath;
    private string filename;
    private Vector2 scrollPosition;
    private GUIStyle style = null;
    private GUIContent label1 = null;

    private Camera cam = null;

    // Use this for initialization
    void Start()
    {
        currentPath = System.IO.Path.GetFullPath(Application.dataPath);
        filename = "map.png";
        scrollPosition = Vector2.zero;

        style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.fontSize = 36;

        label1 = new GUIContent("Press p to generate image file.");

        cam = GetComponent<Camera>();
        cam.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.one);

        if (null != target)
        {
            Renderer[] renderers = target.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                bounds.Encapsulate(r.bounds);
            }
        }

        cam.transform.position = new Vector3(bounds.center.x, bounds.max.y + 10.0f, bounds.center.z);
        cam.orthographicSize = Mathf.Max(bounds.size.x, bounds.size.z) * 0.5f;

        if (Input.GetKeyDown(KeyCode.P))
        {
            fileDialog = !fileDialog;
            //string path = System.IO.Path.Combine(Application.persistentDataPath, "map.png");
            //RenderToPNG(path, 2048, 2048);
        }
    }

    void OnGUI()
    {
        if (!fileDialog)
        {
            Vector2 size = style.CalcSize(label1);
            Rect r = new Rect((Screen.width - size.x) * 0.5f, (Screen.height - size.y) * 0.07f, size.x, size.y);

            GUI.Label(r, label1, style);
        }
        else
        {
            float max = Mathf.Min(Screen.width, Screen.height);
            float w = max * 0.8f, h = max * 0.6f;
            Rect r = new Rect((Screen.width - w) * 0.5f, (Screen.height - h) * 0.5f, w, h);
            GUI.backgroundColor = Color.gray;
            GUILayout.BeginArea(r);
            GUI.Box(new Rect(0, 0, w, h), string.Empty);

            GUILayout.Label("- Current Folder: ");
            GUILayout.Label(currentPath);
            
            if (string.IsNullOrEmpty(currentPath))
            {
                GUILayout.Label("- Drives: ");
                var drives = System.IO.Directory.GetLogicalDrives();
                foreach (var drive in drives)
                {
                    if (GUILayout.Button('[' + drive + ']'))
                    {
                        currentPath = drive;
                        scrollPosition = Vector2.zero;
                    }
                }
            }
            else
            {
                GUILayout.Label("- File Name: ");
                filename = GUILayout.TextField(filename);

                if (GUILayout.Button("Save"))
                {
                    if (System.IO.Path.GetExtension(filename) != ".png")
                    {
                        filename = System.IO.Path.GetFileNameWithoutExtension(filename) + ".png";
                    }

                    string path = System.IO.Path.Combine(currentPath, filename);

                    RenderToPNG(path, 2048, 2048);
                    fileDialog = false;
                    PlayerPrefs.SetString("seed", target.GetComponent<MapGenerator>().seed);
                    SceneManager.LoadScene("MainGame");
                }
                string[] dirs = System.IO.Directory.GetDirectories(currentPath);

                GUILayout.Label("- Folder Content: ");
                scrollPosition = GUILayout.BeginScrollView(scrollPosition);

                if (GUILayout.Button("[ .. ]"))
                {
                    currentPath = System.IO.Path.GetDirectoryName(currentPath);
                    scrollPosition = Vector2.zero;
                }
                foreach (var dir in dirs)
                {
                    if (GUILayout.Button('[' + System.IO.Path.GetFileName(dir) + ']'))
                    {
                        currentPath = System.IO.Path.GetFullPath(dir);
                        scrollPosition = Vector2.zero;
                    }
                }

                GUILayout.EndScrollView();
            }

            GUILayout.EndArea();
        }
    }

    void RenderToPNG(string filename, int width, int height)
    {
        var tex = new Texture2D(width, height);
        var rt = new RenderTexture(width, height, 24);

        cam.targetTexture = rt;
        cam.Render();

        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        cam.targetTexture = null;
        RenderTexture.active = null;

        Destroy(rt);

        byte[] data = tex.EncodeToPNG();

        System.IO.File.WriteAllBytes(filename, data);
    }
}
