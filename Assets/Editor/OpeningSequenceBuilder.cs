using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public static class OpeningSequenceBuilder
{
    private const string IntroPath = "Assets/Scenes/Level00_Despertar.unity";
    private const string LevelOnePath = "Assets/Scenes/Level01.unity";
    private static Sprite square;

    [InitializeOnLoadMethod]
    private static void BuildOnFirstImport()
    {
        if (!AssetDatabase.LoadAssetAtPath<SceneAsset>(IntroPath))
            EditorApplication.delayCall += Build;
    }

    [MenuItem("Tools/Skynet Protocol/Crear prologo de despertar")]
    public static void Build()
    {
        square = FirstSprite("Assets/Art/ground.png");
        if (!square)
        {
            Debug.LogError("No se encontro el sprite base para construir el prologo.");
            return;
        }

        Scene sourceScene = EditorSceneManager.OpenScene(LevelOnePath, OpenSceneMode.Single);
        GameObject sourceRobot = GameObject.Find("PlayerRobot");
        if (!sourceRobot)
        {
            Debug.LogError("No se encontro PlayerRobot en Level01.");
            return;
        }

        Scene introScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
        GameObject robot = Object.Instantiate(sourceRobot);
        robot.name = "PlayerRobot";
        SceneManager.MoveGameObjectToScene(robot, introScene);
        SceneManager.SetActiveScene(introScene);
        EditorSceneManager.CloseScene(sourceScene, true);

        robot.transform.position = new Vector3(-4.2f, -1.82f, 0f);
        robot.transform.localScale = Vector3.one;

        CreateCamera();
        CreateGlobalLight();
        CreateRoom();
        SpriteRenderer exitLight = CreateExit();

        var directorObject = new GameObject("BootSequenceDirector");
        var director = directorObject.AddComponent<BootSequenceController>();
        var player = robot.GetComponent<PlayerController>();
        var animator = robot.GetComponent<RobotVisualAnimator>();
        var body = robot.GetComponent<Rigidbody2D>();
        var eye = robot.transform.Find("Visual/Eye").GetComponent<SpriteRenderer>();
        director.Configure(player, animator, body, eye, exitLight);

        EditorSceneManager.SaveScene(introScene, IntroPath);
        ConfigureBuildSettings();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Prologo creado: Level00_Despertar con secuencia de inicio y salida hacia Level01.");
    }

    private static void CreateCamera()
    {
        var cameraObject = new GameObject("Main Camera");
        cameraObject.tag = "MainCamera";
        cameraObject.transform.position = new Vector3(0f, 0f, -10f);
        var camera = cameraObject.AddComponent<Camera>();
        camera.orthographic = true;
        camera.orthographicSize = 5.2f;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = Hex("05080D");
        cameraObject.AddComponent<AudioListener>();
        cameraObject.AddComponent<UniversalAdditionalCameraData>();
    }

    private static void CreateGlobalLight()
    {
        var lightObject = new GameObject("Global Light 2D");
        var light = lightObject.AddComponent<Light2D>();
        light.lightType = Light2D.LightType.Global;
        light.intensity = 0.72f;
        light.color = new Color(0.72f, 0.86f, 1f);
    }

    private static void CreateRoom()
    {
        var room = new GameObject("ActivationChamber");
        Rect(room.transform, "Background", 0, 0, 18f, 10.2f, Hex("0B111A"), -30);
        Rect(room.transform, "BackPanel", 0, .15f, 16.5f, 7.6f, Hex("17212C"), -25);

        for (int i = 0; i < 7; i++)
        {
            float x = -7.2f + i * 2.4f;
            Rect(room.transform, "WallSeam", x, .15f, .07f, 7.5f, Hex("263746"), -23);
        }

        GameObject floor = Rect(room.transform, "Floor", 0, -3.15f, 18f, .75f, Hex("35434D"), 0);
        floor.layer = 6;
        floor.AddComponent<BoxCollider2D>();
        Rect(room.transform, "FloorGlow", 0, -2.75f, 18f, .08f, Hex("1AB8B1"), 1);

        GameObject leftWall = Rect(room.transform, "LeftWall", -8.65f, 0, .55f, 10f, Hex("283640"), 1);
        leftWall.layer = 6;
        leftWall.AddComponent<BoxCollider2D>();

        Capsule(room.transform);
        CeilingLights(room.transform);

        InstantiateDecoration("Assets/Prefabs/Environment/Furniture/PF_LabDesk.prefab", new Vector3(1.55f, -1.75f, 0));
        InstantiateDecoration("Assets/Prefabs/Environment/Electronics/PF_LaptopOpen.prefab", new Vector3(1.55f, -.93f, 0));
        InstantiateDecoration("Assets/Prefabs/Environment/Signs/PF_IDTerminal.prefab", new Vector3(5.75f, -.95f, 0));

        Rect(room.transform, "ObservationWindow", -.25f, 1.25f, 4.0f, 1.75f, Hex("07151D"), -20);
        Rect(room.transform, "WindowTop", -.25f, 2.12f, 4.25f, .16f, Hex("52616B"), -18);
        Rect(room.transform, "WindowBottom", -.25f, .38f, 4.25f, .16f, Hex("52616B"), -18);
        Rect(room.transform, "WindowScan", -.25f, 1.25f, 3.4f, .06f, Hex("158E99"), -17);
    }

    private static void Capsule(Transform root)
    {
        var capsule = new GameObject("RobotActivationPod");
        capsule.transform.SetParent(root, false);
        capsule.transform.localPosition = new Vector3(-4.2f, -.75f, 0);

        Rect(capsule.transform, "Glass", 0, .25f, 2.35f, 3.9f, new Color(.05f, .38f, .44f, .27f), -3);
        Rect(capsule.transform, "SideL", -1.18f, .25f, .22f, 4.15f, Hex("63717A"), -1);
        Rect(capsule.transform, "SideR", 1.18f, .25f, .22f, 4.15f, Hex("63717A"), -1);
        Rect(capsule.transform, "Top", 0, 2.28f, 2.55f, .28f, Hex("46535D"), -1);
        Rect(capsule.transform, "Base", 0, -1.83f, 2.7f, .34f, Hex("46535D"), 2);
        Rect(capsule.transform, "Status", 0, 2.30f, .75f, .08f, Hex("E34B5D"), 1);
        Rect(capsule.transform, "OpenDoor", 1.65f, .05f, .18f, 3.5f, Hex("8DA1A8"), -2, -12f);
    }

    private static void CeilingLights(Transform root)
    {
        for (int i = 0; i < 3; i++)
        {
            float x = -5.6f + i * 5.6f;
            Rect(root, "CeilingLamp", x, 4.1f, 2.8f, .22f, Hex("71808A"), -10);
            Rect(root, "CeilingGlow", x, 3.92f, 2.35f, .08f, Hex("B9FFFF"), -9);
        }
    }

    private static SpriteRenderer CreateExit()
    {
        var exit = new GameObject("ExitToLevel01");
        exit.transform.position = new Vector3(7.15f, -.65f, 0);
        Rect(exit.transform, "Door", 0, 0, 2.1f, 4.25f, Hex("111922"), -4);
        Rect(exit.transform, "FrameL", -1.08f, 0, .22f, 4.55f, Hex("53616B"), 2);
        Rect(exit.transform, "FrameR", 1.08f, 0, .22f, 4.55f, Hex("53616B"), 2);
        Rect(exit.transform, "FrameTop", 0, 2.18f, 2.35f, .22f, Hex("53616B"), 2);
        Rect(exit.transform, "DoorLine", 0, 0, .08f, 3.8f, Hex("263746"), -2);
        SpriteRenderer light = Rect(exit.transform, "AccessLight", .72f, 1.68f, .28f, .16f, Hex("E34B5D"), 4).GetComponent<SpriteRenderer>();

        var trigger = exit.AddComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        trigger.size = new Vector2(1.5f, 4f);
        exit.AddComponent<SceneExitTrigger>();
        return light;
    }

    private static void InstantiateDecoration(string path, Vector3 position)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (!prefab) return;
        GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        if (!instance) return;
        instance.transform.position = position;
    }

    private static GameObject Rect(Transform parent, string name, float x, float y, float width, float height, Color color, int order, float rotation = 0f)
    {
        var part = new GameObject(name);
        part.transform.SetParent(parent, false);
        part.transform.localPosition = new Vector3(x, y, 0);
        part.transform.localScale = new Vector3(width, height, 1);
        part.transform.localRotation = Quaternion.Euler(0, 0, rotation);
        var renderer = part.AddComponent<SpriteRenderer>();
        renderer.sprite = square;
        renderer.color = color;
        renderer.sortingOrder = order;
        return part;
    }

    private static Sprite FirstSprite(string path)
    {
        foreach (Object asset in AssetDatabase.LoadAllAssetsAtPath(path))
            if (asset is Sprite sprite) return sprite;
        return null;
    }

    private static void ConfigureBuildSettings()
    {
        var paths = new List<string> { IntroPath, LevelOnePath };
        paths.AddRange(EditorBuildSettings.scenes.Select(scene => scene.path).Where(path => !paths.Contains(path)));
        EditorBuildSettings.scenes = paths.Select(path => new EditorBuildSettingsScene(path, true)).ToArray();
    }

    private static Color Hex(string value)
    {
        ColorUtility.TryParseHtmlString("#" + value, out Color color);
        return color;
    }
}
