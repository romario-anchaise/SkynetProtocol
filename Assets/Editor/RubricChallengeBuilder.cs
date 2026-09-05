using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class RubricChallengeBuilder
{
    private const string ScenePath = "Assets/Scenes/Level01.unity";
    private static Sprite square;

    [MenuItem("Tools/Skynet Protocol/Crear reto de la rubrica")]
    public static void Build()
    {
        square = FirstSprite("Assets/Art/ground.png");
        Scene scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);

        GameObject previous = GameObject.Find("RubricChallenge");
        if (previous) Object.DestroyImmediate(previous);

        GameObject robot = GameObject.Find("PlayerRobot");
        GameObject ground = GameObject.Find("Ground");
        if (!square || !robot || !ground)
        {
            Debug.LogError("No se pudo construir el reto: faltan el sprite base, PlayerRobot o Ground.");
            return;
        }

        robot.transform.position = new Vector3(-4f, -1.5f, 0f);
        ground.transform.localScale = new Vector3(14f, 1f, 1f);

        var root = new GameObject("RubricChallenge");
        CreateBackground(root.transform);
        CreateChallenge(root.transform, robot.GetComponent<PlayerController>());

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        AssetDatabase.SaveAssets();
        Debug.Log("Reto de rubrica creado: empujar caja, activar placa, cruzar puerta y reiniciar con R.");
    }

    private static void CreateChallenge(Transform root, PlayerController player)
    {
        GameObject cratePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Environment/Gameplay/PF_PushCrate.prefab");
        GameObject crate = PrefabUtility.InstantiatePrefab(cratePrefab) as GameObject;
        crate.name = "PF_PushCrate_RubricInteraction";
        crate.transform.position = new Vector3(-.2f, -1.9f, 0f);
        Rigidbody2D crateBody = crate.GetComponent<Rigidbody2D>();
        crateBody.interpolation = RigidbodyInterpolation2D.Interpolate;
        crateBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        crateBody.mass = 1.35f;
        crate.transform.SetParent(root);

        GameObject plate = Rect(root, "PressurePlate", 2.35f, -2.43f, 1.55f, .22f, Hex("E83F43"), 3);
        var plateCollider = plate.AddComponent<BoxCollider2D>();
        plateCollider.isTrigger = true;

        GameObject door = Rect(root, "SecurityDoor", 4.35f, -.7f, .55f, 3.8f, Hex("54636D"), 5);
        door.layer = 6;
        var doorCollider = door.AddComponent<BoxCollider2D>();
        Rect(door.transform, "DoorGlow", 0, 0, .16f, .82f, Hex("E83F43"), 6);

        var plateLogic = plate.AddComponent<PressurePlateDoor>();
        plateLogic.Configure(door.transform, doorCollider, plate.GetComponent<SpriteRenderer>());

        var flowObject = new GameObject("LevelFlow");
        flowObject.transform.SetParent(root);
        var flow = flowObject.AddComponent<LevelFlowController>();
        flow.Configure(player);

        var goal = new GameObject("PrototypeGoal");
        goal.transform.SetParent(root);
        goal.transform.position = new Vector3(5.55f, -1f, 0f);
        var goalCollider = goal.AddComponent<BoxCollider2D>();
        goalCollider.isTrigger = true;
        goalCollider.size = new Vector2(1.25f, 3.5f);
        var goalLogic = goal.AddComponent<PrototypeGoal>();
        goalLogic.Configure(flow);

        Rect(root, "GoalBeacon", 5.75f, -.55f, .18f, 3.9f, Hex("19E0D0"), 2);
        Rect(root, "GoalHeader", 5.75f, 1.42f, 1.35f, .20f, Hex("19E0D0"), 3);
        Rect(root, "PlateCable", 3.34f, -2.60f, 1.75f, .07f, Hex("19E0D0"), 2);
    }

    private static void CreateBackground(Transform root)
    {
        Rect(root, "BackWall", 0, .1f, 14f, 5.7f, Hex("121B25"), -20);
        for (int i = 0; i < 6; i++)
        {
            float x = -5.8f + i * 2.3f;
            Rect(root, "Panel", x, .1f, 2.05f, 5.35f, i % 2 == 0 ? Hex("1A2732") : Hex("18232D"), -18);
            Rect(root, "PanelLight", x, 2.45f, 1.35f, .07f, Hex("1A8D9A"), -17);
        }
        Rect(root, "InstructionScreen", -.9f, 1.1f, 2.7f, 1.25f, Hex("07151D"), -10);
        Rect(root, "InstructionLineA", -.9f, 1.25f, 1.65f, .08f, Hex("19E0D0"), -9);
        Rect(root, "InstructionLineB", -.9f, .92f, 1.15f, .06f, Hex("61727C"), -9);
    }

    private static GameObject Rect(Transform parent, string name, float x, float y, float width, float height, Color color, int order)
    {
        var part = new GameObject(name);
        part.transform.SetParent(parent, false);
        part.transform.localPosition = new Vector3(x, y, 0f);
        part.transform.localScale = new Vector3(width, height, 1f);
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

    private static Color Hex(string value)
    {
        ColorUtility.TryParseHtmlString("#" + value, out Color color);
        return color;
    }
}
