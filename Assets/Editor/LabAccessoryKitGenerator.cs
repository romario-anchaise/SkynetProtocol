using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public static class LabAccessoryKitGenerator
{
    private const string RootPath = "Assets/Prefabs/Environment";
    private const string SentinelPath = RootPath + "/Clutter/PF_LooseCable.prefab";

    private static readonly Color Ink = Hex("151922");
    private static readonly Color Dark = Hex("252C36");
    private static readonly Color Metal = Hex("4A5562");
    private static readonly Color LightMetal = Hex("75818C");
    private static readonly Color Cyan = Hex("19E0D0");
    private static readonly Color Blue = Hex("2D87B8");
    private static readonly Color Green = Hex("62E6A7");
    private static readonly Color Red = Hex("E54B5D");
    private static readonly Color Yellow = Hex("F2C94C");
    private static readonly Color Paper = Hex("D5D9D7");
    private static Sprite square;
    private static Sprite circle;

    [InitializeOnLoadMethod]
    private static void GenerateOnFirstImport()
    {
        if (!AssetDatabase.LoadAssetAtPath<GameObject>(SentinelPath))
            EditorApplication.delayCall += GenerateAll;
    }

    [MenuItem("Tools/Skynet Protocol/Regenerar kit de accesorios del laboratorio")]
    public static void GenerateAll()
    {
        LoadSprites();
        EnsureFolders("Furniture", "Electronics", "Science", "Clutter", "Signs");

        Furniture();
        Electronics();
        Science();
        Clutter();
        Signs();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Kit de laboratorio creado: 32 prefabs modulares listos para arrastrar y superponer.");
    }

    private static void Furniture()
    {
        Save("Furniture", "PF_LabDesk", r =>
        {
            Rect(r, "Top", 0, .35f, 3.8f, .28f, Metal, 2);
            Rect(r, "FrontEdge", 0, .20f, 3.9f, .12f, LightMetal, 3);
            Rect(r, "LegL", -1.55f, -.65f, .25f, 1.8f, Dark, 1);
            Rect(r, "LegR", 1.55f, -.65f, .25f, 1.8f, Dark, 1);
            Rect(r, "Drawer", 1.0f, -.12f, .85f, .52f, Ink, 2);
            Rect(r, "Handle", 1.0f, -.08f, .35f, .06f, Cyan, 3);
        });
        Save("Furniture", "PF_LabDeskDamaged", r =>
        {
            Rect(r, "Top", 0, .28f, 3.7f, .28f, Metal, 2, -4);
            Rect(r, "LegL", -1.45f, -.72f, .25f, 1.8f, Dark, 1, -5);
            Rect(r, "BrokenLeg", 1.35f, -.62f, .22f, 1.45f, Dark, 1, 18);
            Rect(r, "Cable", .75f, -.35f, 1.2f, .08f, Cyan, 3, -28);
            Rect(r, "Spark", 1.22f, -.72f, .20f, .06f, Yellow, 4, 40);
        });
        Save("Furniture", "PF_OfficeChair", r =>
        {
            Rect(r, "Back", 0, .62f, 1.15f, 1.45f, Dark, 2);
            Rect(r, "BackGlow", 0, .62f, .75f, .08f, Cyan, 3);
            Rect(r, "Seat", 0, -.25f, 1.25f, .28f, Metal, 2);
            Rect(r, "Post", 0, -.72f, .14f, .75f, LightMetal, 1);
            Rect(r, "Base", 0, -1.08f, 1.35f, .12f, Dark, 1);
            Circle(r, "WheelL", -.52f, -1.18f, .20f, Ink, 2);
            Circle(r, "WheelR", .52f, -1.18f, .20f, Ink, 2);
        });
        Save("Furniture", "PF_LabStool", r =>
        {
            Circle(r, "Seat", 0, .42f, 1.15f, Metal, 2);
            Rect(r, "Post", 0, -.28f, .16f, 1.15f, LightMetal, 1);
            Rect(r, "Base", 0, -.85f, 1.25f, .13f, Dark, 1);
            Circle(r, "Foot", 0, -.84f, .28f, Cyan, 2);
        });
        Save("Furniture", "PF_MetalShelf", r =>
        {
            Rect(r, "SideL", -1.35f, 0, .18f, 3.2f, Dark, 3);
            Rect(r, "SideR", 1.35f, 0, .18f, 3.2f, Dark, 3);
            for (int i = 0; i < 4; i++) Rect(r, "Shelf" + i, 0, -1.35f + i * .9f, 2.9f, .16f, Metal, 2);
            Rect(r, "BoxA", -.72f, -.96f, .75f, .58f, Hex("76523C"), 3);
            Rect(r, "BoxB", .55f, -.08f, 1.0f, .55f, Blue, 3);
            Rect(r, "Bottle", .82f, .92f, .24f, .62f, Green, 3);
        });
        Save("Furniture", "PF_WallLocker", r =>
        {
            Rect(r, "Body", 0, 0, 1.8f, 3.6f, Dark, 1);
            Rect(r, "Door", 0, 0, 1.55f, 3.25f, Metal, 2);
            for (int i = 0; i < 4; i++) Rect(r, "Vent" + i, 0, 1.05f - i * .16f, .75f, .06f, Ink, 3);
            Rect(r, "Handle", .55f, -.25f, .08f, .55f, Cyan, 3);
        });
    }

    private static void Electronics()
    {
        Save("Electronics", "PF_LaptopOpen", r =>
        {
            Rect(r, "ScreenFrame", 0, .42f, 1.75f, 1.05f, Dark, 2, -2);
            Rect(r, "Screen", 0, .45f, 1.45f, .73f, Hex("073849"), 3, -2);
            Rect(r, "Scan", .12f, .44f, 1.0f, .06f, Cyan, 4, -2);
            Rect(r, "Base", .05f, -.22f, 2.0f, .34f, LightMetal, 3, 3);
            Rect(r, "Keyboard", -.05f, -.18f, 1.25f, .10f, Ink, 4, 3);
        });
        Save("Electronics", "PF_LaptopClosed", r =>
        {
            Rect(r, "Case", 0, 0, 1.85f, .22f, Dark, 2, -6);
            Rect(r, "Edge", 0, -.10f, 1.95f, .08f, LightMetal, 3, -6);
            Circle(r, "Logo", 0, .03f, .20f, Cyan, 4);
        });
        Save("Electronics", "PF_DualMonitorStation", r =>
        {
            Monitor(r, -.95f, .35f, -4);
            Monitor(r, .95f, .35f, 4);
            Rect(r, "Stand", 0, -.55f, .18f, 1.15f, LightMetal, 1);
            Rect(r, "Foot", 0, -1.06f, 1.25f, .16f, Dark, 2);
        });
        Save("Electronics", "PF_Keyboard", r =>
        {
            Rect(r, "Body", 0, 0, 2.0f, .55f, Dark, 1);
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 8; x++) Rect(r, "Key", -.78f + x * .225f, .16f - y * .16f, .13f, .09f, LightMetal, 2);
        });
        Save("Electronics", "PF_Tablet", r =>
        {
            Rect(r, "Frame", 0, 0, 1.15f, 1.65f, Dark, 2, -12);
            Rect(r, "Glass", 0, .06f, .88f, 1.25f, Hex("073849"), 3, -12);
            Rect(r, "Data", 0, .12f, .52f, .06f, Cyan, 4, -12);
            Circle(r, "Button", -.15f, -.68f, .12f, LightMetal, 4);
        });
        Save("Electronics", "PF_SecurityCamera", r =>
        {
            Rect(r, "WallMount", -1.0f, .55f, .28f, 1.2f, Metal, 1);
            Rect(r, "Arm", -.62f, .25f, .85f, .18f, LightMetal, 2, -18);
            Rect(r, "Camera", .15f, .10f, 1.35f, .68f, Dark, 3, -8);
            Circle(r, "Lens", .62f, .10f, .43f, Cyan, 4);
            Rect(r, "Shade", .15f, .46f, 1.55f, .15f, Ink, 4, -8);
        });
        Save("Electronics", "PF_Oscilloscope", r =>
        {
            Rect(r, "Case", 0, 0, 1.85f, 1.28f, Metal, 1);
            Rect(r, "Screen", -.28f, .12f, 1.0f, .72f, Hex("062A32"), 2);
            Zigzag(r, "Wave", -.62f, .12f, Cyan, 3);
            Circle(r, "DialA", .57f, .28f, .26f, Ink, 2);
            Circle(r, "DialB", .57f, -.22f, .26f, Yellow, 2);
        });
        Save("Electronics", "PF_ControlConsole", r =>
        {
            Rect(r, "Body", 0, -.22f, 2.7f, 1.25f, Dark, 1);
            Rect(r, "Panel", 0, .28f, 2.5f, .68f, Metal, 2, -5);
            Rect(r, "Screen", -.62f, .32f, .92f, .40f, Hex("073849"), 3, -5);
            for (int i = 0; i < 4; i++) Circle(r, "Button" + i, .25f + i * .35f, .18f, .16f, i == 3 ? Red : Cyan, 4);
            Rect(r, "Base", 0, -.92f, 2.2f, .22f, Ink, 2);
        });
    }

    private static void Science()
    {
        Save("Science", "PF_Microscope", r =>
        {
            Rect(r, "Base", 0, -.85f, 1.55f, .22f, Dark, 2);
            Rect(r, "Post", -.42f, -.05f, .25f, 1.55f, Metal, 2, -12);
            Rect(r, "Arm", -.05f, .48f, .85f, .20f, LightMetal, 3, 24);
            Rect(r, "Lens", .31f, .18f, .22f, .58f, Ink, 3, 8);
            Circle(r, "Focus", -.35f, -.05f, .33f, Cyan, 4);
            Rect(r, "Stage", .12f, -.36f, 1.05f, .12f, LightMetal, 3);
        });
        Save("Science", "PF_TestTubeRack", r =>
        {
            Rect(r, "RackTop", 0, .18f, 1.9f, .15f, Metal, 3);
            Rect(r, "RackBase", 0, -.62f, 2.1f, .18f, Dark, 2);
            Rect(r, "LegL", -.82f, -.22f, .12f, .75f, Metal, 2);
            Rect(r, "LegR", .82f, -.22f, .12f, .75f, Metal, 2);
            for (int i = 0; i < 5; i++)
            {
                Rect(r, "Tube" + i, -.65f + i * .33f, -.05f, .18f, .85f, i % 2 == 0 ? Cyan : Green, 2);
                Circle(r, "Cap" + i, -.65f + i * .33f, .38f, .18f, LightMetal, 3);
            }
        });
        Save("Science", "PF_ChemicalFlasks", r =>
        {
            Flask(r, -.55f, -.20f, .85f, Cyan);
            Flask(r, .20f, -.34f, 1.15f, Green);
            Flask(r, .86f, -.43f, .67f, Red);
        });
        Save("Science", "PF_SampleCanister", r =>
        {
            Rect(r, "Glass", 0, 0, 1.15f, 2.25f, new Color(.06f, .42f, .45f, .72f), 1);
            Rect(r, "Top", 0, 1.16f, 1.42f, .25f, Metal, 3);
            Rect(r, "Bottom", 0, -1.16f, 1.42f, .25f, Metal, 3);
            Circle(r, "Sample", 0, -.20f, .58f, Green, 2);
            Rect(r, "Label", 0, .55f, .68f, .20f, Paper, 4);
        });
        Save("Science", "PF_MedicalBed", r =>
        {
            Rect(r, "Mattress", 0, .28f, 3.5f, .55f, Hex("BFC8C8"), 3, -3);
            Rect(r, "Frame", 0, -.05f, 3.7f, .18f, Metal, 2, -3);
            Rect(r, "LegL", -1.35f, -.62f, .18f, 1.05f, Dark, 1);
            Rect(r, "LegR", 1.35f, -.62f, .18f, 1.05f, Dark, 1);
            Rect(r, "Pillow", -1.18f, .53f, .82f, .28f, Paper, 4, -8);
            Circle(r, "WheelL", -1.35f, -1.15f, .28f, Ink, 2);
            Circle(r, "WheelR", 1.35f, -1.15f, .28f, Ink, 2);
        });
        Save("Science", "PF_RoboticArm", r =>
        {
            Rect(r, "Base", 0, -.95f, 1.3f, .28f, Dark, 2);
            Circle(r, "JointA", 0, -.65f, .52f, Cyan, 3);
            Rect(r, "ArmA", -.18f, -.04f, .30f, 1.15f, Metal, 2, -18);
            Circle(r, "JointB", -.36f, .50f, .46f, Cyan, 3);
            Rect(r, "ArmB", .08f, .93f, .28f, 1.1f, LightMetal, 2, 48);
            Circle(r, "JointC", .50f, 1.30f, .38f, Yellow, 3);
            Rect(r, "ClawL", .73f, 1.55f, .12f, .58f, Ink, 3, -32);
            Rect(r, "ClawR", .95f, 1.45f, .12f, .58f, Ink, 3, 28);
        });
        Save("Science", "PF_PortableGenerator", r =>
        {
            Rect(r, "Body", 0, 0, 2.0f, 1.45f, Dark, 1);
            Rect(r, "Panel", -.32f, .10f, 1.0f, .72f, Metal, 2);
            Circle(r, "Core", -.32f, .10f, .48f, Cyan, 3);
            Rect(r, "Handle", 0, .92f, 1.35f, .15f, LightMetal, 2);
            Rect(r, "Side", .72f, 0, .28f, 1.1f, Yellow, 3);
            Circle(r, "FootL", -.67f, -.78f, .24f, Ink, 2);
            Circle(r, "FootR", .67f, -.78f, .24f, Ink, 2);
        });
    }

    private static void Clutter()
    {
        Save("Clutter", "PF_LooseCable", r =>
        {
            Rect(r, "CableA", -.72f, .02f, 1.45f, .10f, Ink, 2, 18);
            Rect(r, "CableB", .27f, .16f, .95f, .10f, Ink, 2, -25);
            Rect(r, "CableC", .92f, -.02f, .70f, .10f, Ink, 2, 22);
            Circle(r, "Plug", 1.28f, .12f, .28f, Cyan, 3);
        });
        Save("Clutter", "PF_CableCoil", r =>
        {
            for (int i = 0; i < 3; i++) Circle(r, "Loop" + i, -.42f + i * .42f, 0, 1.0f, Ink, 2);
            for (int i = 0; i < 3; i++) Circle(r, "Hole" + i, -.42f + i * .42f, 0, .67f, Hex("53616A"), 3);
            Rect(r, "Tie", 0, 0, .18f, 1.12f, Yellow, 4);
        });
        Save("Clutter", "PF_ScatteredPapers", r =>
        {
            Rect(r, "PaperA", -.45f, .05f, 1.1f, .72f, Paper, 1, 12);
            Rect(r, "PaperB", .35f, -.04f, 1.1f, .72f, Hex("AEB9B8"), 2, -9);
            Rect(r, "LineA", -.37f, .10f, .58f, .04f, Blue, 3, 12);
            Rect(r, "LineB", .40f, -.02f, .62f, .04f, Red, 4, -9);
        });
        Save("Clutter", "PF_Toolbox", r =>
        {
            Rect(r, "Box", 0, -.20f, 1.75f, .85f, Red, 2);
            Rect(r, "Lid", 0, .30f, 1.9f, .20f, Dark, 3);
            Rect(r, "HandleTop", 0, .76f, .85f, .12f, LightMetal, 3);
            Rect(r, "HandleL", -.38f, .56f, .10f, .42f, LightMetal, 3);
            Rect(r, "HandleR", .38f, .56f, .10f, .42f, LightMetal, 3);
            Rect(r, "Latch", 0, .08f, .25f, .22f, Yellow, 4);
        });
        Save("Clutter", "PF_BatteryPack", r =>
        {
            Rect(r, "Case", 0, 0, 1.05f, 1.55f, Dark, 1);
            Rect(r, "Charge", 0, -.18f, .65f, .75f, Cyan, 2);
            Rect(r, "TerminalL", -.26f, .88f, .18f, .22f, LightMetal, 3);
            Rect(r, "TerminalR", .26f, .88f, .18f, .22f, LightMetal, 3);
            Rect(r, "Stripe", 0, .32f, .72f, .10f, Yellow, 4);
        });
        Save("Clutter", "PF_ChemicalBarrel", r =>
        {
            Rect(r, "Drum", 0, 0, 1.35f, 2.0f, Blue, 1);
            Rect(r, "BandTop", 0, .72f, 1.45f, .16f, Ink, 2);
            Rect(r, "BandBottom", 0, -.72f, 1.45f, .16f, Ink, 2);
            Rect(r, "Label", 0, 0, .72f, .58f, Yellow, 3);
            Rect(r, "Hazard", 0, 0, .12f, .40f, Ink, 4, 45);
            Rect(r, "Hazard2", 0, 0, .12f, .40f, Ink, 4, -45);
        });
        Save("Clutter", "PF_TrashBag", r =>
        {
            Circle(r, "Bag", 0, -.18f, 1.55f, Ink, 1);
            Rect(r, "Bottom", 0, -.65f, 1.35f, .50f, Ink, 2);
            Rect(r, "Tie", 0, .63f, .22f, .35f, Metal, 3, 15);
            Rect(r, "Highlight", -.28f, .05f, .12f, .70f, Metal, 3, -25);
        });
        Save("Clutter", "PF_BrokenPanel", r =>
        {
            Rect(r, "Panel", 0, 0, 2.0f, 1.35f, Metal, 1, -8);
            Rect(r, "Inset", 0, 0, 1.65f, 1.0f, Dark, 2, -8);
            Rect(r, "CrackA", -.20f, .05f, .08f, .85f, Ink, 3, 38);
            Rect(r, "CrackB", .18f, -.18f, .08f, .60f, Ink, 3, -52);
            Rect(r, "WireA", .72f, -.72f, .75f, .07f, Red, 4, -58);
            Rect(r, "WireB", .88f, -.70f, .75f, .07f, Cyan, 4, -38);
        });
    }

    private static void Signs()
    {
        Save("Signs", "PF_HazardSign", r =>
        {
            Rect(r, "Plate", 0, 0, 1.65f, 1.35f, Yellow, 1);
            Rect(r, "BorderTop", 0, .60f, 1.75f, .10f, Ink, 2);
            Rect(r, "BorderBottom", 0, -.60f, 1.75f, .10f, Ink, 2);
            Rect(r, "Mark", 0, .12f, .18f, .62f, Ink, 3);
            Circle(r, "Dot", 0, -.36f, .18f, Ink, 3);
        });
        Save("Signs", "PF_ExitSign", r =>
        {
            Rect(r, "Plate", 0, 0, 1.9f, .78f, Hex("145D47"), 1);
            Rect(r, "ArrowBody", .22f, 0, .82f, .14f, Paper, 2);
            Rect(r, "ArrowUp", .64f, .13f, .36f, .12f, Paper, 2, 45);
            Rect(r, "ArrowDown", .64f, -.13f, .36f, .12f, Paper, 2, -45);
            Circle(r, "Person", -.55f, .18f, .22f, Paper, 2);
            Rect(r, "PersonBody", -.55f, -.16f, .22f, .48f, Paper, 2);
        });
        Save("Signs", "PF_IDTerminal", r =>
        {
            Rect(r, "Body", 0, 0, .78f, 1.35f, Dark, 1);
            Rect(r, "Screen", 0, .28f, .52f, .45f, Hex("073849"), 2);
            Rect(r, "Scan", 0, .30f, .38f, .06f, Cyan, 3);
            Circle(r, "Reader", 0, -.34f, .25f, Green, 3);
        });
    }

    private static void Monitor(Transform root, float x, float y, float rotation)
    {
        Rect(root, "MonitorFrame", x, y, 1.65f, 1.05f, Dark, 2, rotation);
        Rect(root, "MonitorScreen", x, y + .03f, 1.38f, .75f, Hex("073849"), 3, rotation);
        Rect(root, "MonitorGlow", x, y + .03f, .82f, .06f, Cyan, 4, rotation);
    }

    private static void Flask(Transform root, float x, float y, float size, Color liquid)
    {
        Circle(root, "Flask", x, y, size, new Color(.55f, .72f, .74f, .85f), 1);
        Rect(root, "Liquid", x, y - size * .16f, size * .72f, size * .35f, liquid, 2);
        Rect(root, "Neck", x, y + size * .52f, size * .24f, size * .55f, LightMetal, 2);
        Rect(root, "Rim", x, y + size * .80f, size * .38f, size * .10f, Dark, 3);
    }

    private static void Zigzag(Transform root, string name, float x, float y, Color color, int order)
    {
        for (int i = 0; i < 4; i++)
            Rect(root, name + i, x + i * .20f, y + (i % 2 == 0 ? -.08f : .08f), .28f, .045f, color, order, i % 2 == 0 ? 38 : -38);
    }

    private static void Save(string folder, string prefabName, Action<Transform> build)
    {
        var root = new GameObject(prefabName);
        root.layer = 0;
        var group = root.AddComponent<SortingGroup>();
        group.sortingLayerName = "Default";
        group.sortingOrder = 10;
        build(root.transform);
        PrefabUtility.SaveAsPrefabAsset(root, $"{RootPath}/{folder}/{prefabName}.prefab");
        UnityEngine.Object.DestroyImmediate(root);
    }

    private static GameObject Rect(Transform root, string name, float x, float y, float width, float height, Color color, int order, float rotation = 0)
    {
        return Part(root, name, square, new Vector2(x, y), new Vector2(width, height), color, order, rotation);
    }

    private static GameObject Circle(Transform root, string name, float x, float y, float size, Color color, int order)
    {
        return Part(root, name, circle, new Vector2(x, y), new Vector2(size, size), color, order, 0);
    }

    private static GameObject Part(Transform root, string name, Sprite sprite, Vector2 position, Vector2 scale, Color color, int order, float rotation)
    {
        var part = new GameObject(name);
        part.transform.SetParent(root, false);
        part.transform.localPosition = position;
        part.transform.localScale = new Vector3(scale.x, scale.y, 1);
        part.transform.localRotation = Quaternion.Euler(0, 0, rotation);
        var renderer = part.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.color = color;
        renderer.sortingOrder = order;
        return part;
    }

    private static void LoadSprites()
    {
        square = FirstSprite("Assets/Art/ground.png");
        circle = FirstSprite("Assets/Art/wheel-circle.png");
        if (!square || !circle) throw new InvalidOperationException("No se encontraron los sprites base del kit.");
    }

    private static Sprite FirstSprite(string path)
    {
        foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(path))
            if (asset is Sprite spriteAsset) return spriteAsset;
        return null;
    }

    private static void EnsureFolders(params string[] folders)
    {
        foreach (string folder in folders)
        {
            string path = RootPath + "/" + folder;
            if (!AssetDatabase.IsValidFolder(path)) AssetDatabase.CreateFolder(RootPath, folder);
        }
    }

    private static Color Hex(string value)
    {
        ColorUtility.TryParseHtmlString("#" + value, out Color color);
        return color;
    }
}
