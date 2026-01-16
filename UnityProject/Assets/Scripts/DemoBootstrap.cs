using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DemoBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameObject root = new GameObject("DemoBootstrapRuntime");

        GameObject lighting = new GameObject("Directional Light");
        Light dirLight = lighting.AddComponent<Light>();
        dirLight.type = LightType.Directional;
        dirLight.intensity = 0.6f;
        lighting.transform.rotation = Quaternion.Euler(50f, -30f, 0f);

        Material snowMaterial = CreateMaterial(new Color(0.9f, 0.95f, 1f));
        Material cabinMaterial = CreateMaterial(new Color(0.35f, 0.25f, 0.2f));
        Material propMaterial = CreateMaterial(new Color(0.55f, 0.55f, 0.6f));
        Material markerMaterial = CreateMaterial(new Color(0.1f, 0.9f, 0.9f));

        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(12f, 1f, 12f);
        ApplyMaterial(ground, snowMaterial);

        GameObject cabin = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cabin.name = "Cabin";
        cabin.transform.position = new Vector3(0f, 1.2f, 0f);
        cabin.transform.localScale = new Vector3(6f, 2.5f, 6f);
        ApplyMaterial(cabin, cabinMaterial);
        Collider cabinCollider = cabin.GetComponent<Collider>();
        if (cabinCollider != null)
        {
            cabinCollider.enabled = false;
        }

        GameObject cabinInteriorLight = new GameObject("CabinLight");
        Light interiorLight = cabinInteriorLight.AddComponent<Light>();
        interiorLight.type = LightType.Point;
        interiorLight.range = 12f;
        interiorLight.intensity = 1.4f;
        cabinInteriorLight.transform.position = new Vector3(0f, 2.2f, 0f);

        GameObject player = new GameObject("Player");
        CharacterController controller = player.AddComponent<CharacterController>();
        controller.height = 1.8f;
        controller.radius = 0.3f;
        player.transform.position = new Vector3(0f, 1f, -3.5f);

        GameObject cameraPivot = new GameObject("CameraPivot");
        cameraPivot.transform.SetParent(player.transform);
        cameraPivot.transform.localPosition = new Vector3(0f, 0.7f, 0f);

        GameObject cameraObject = new GameObject("PlayerCamera");
        Camera camera = cameraObject.AddComponent<Camera>();
        cameraObject.transform.SetParent(cameraPivot.transform);
        cameraObject.transform.localPosition = Vector3.zero;

        player.AddComponent<FirstPersonController>();

        Interactor interactor = player.AddComponent<Interactor>();
        interactor.SetPlayerCamera(camera);

        GameObject ui = new GameObject("UI");
        Canvas canvas = ui.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        ui.AddComponent<CanvasScaler>();
        ui.AddComponent<GraphicRaycaster>();

        TextMeshProUGUI subtitleText = CreateText(ui.transform, "Subtitle", new Vector2(0f, 40f), 36, TextAlignmentOptions.Center);
        TextMeshProUGUI promptText = CreateText(ui.transform, "Prompt", new Vector2(0f, -140f), 28, TextAlignmentOptions.Center);
        TextMeshProUGUI objectiveText = CreateText(ui.transform, "Objective", new Vector2(0f, 140f), 28, TextAlignmentOptions.Center);

        SubtitleManager subtitleManager = ui.AddComponent<SubtitleManager>();
        subtitleManager.SetText(subtitleText);

        PromptUI promptUI = promptText.gameObject.AddComponent<PromptUI>();
        ObjectiveUI objectiveUI = objectiveText.gameObject.AddComponent<ObjectiveUI>();

        interactor.PromptChanged += promptUI.SetPrompt;

        GameObject radio = CreateInteractableCube("Radio", new Vector3(1.5f, 1f, 1.5f));
        ApplyMaterial(radio, propMaterial);
        RadioInteractable radioInteractable = radio.AddComponent<RadioInteractable>();
        radioInteractable.SetSubtitleManager(subtitleManager);

        GameObject generator = CreateInteractableCube("Generator", new Vector3(-6f, 0.6f, 6f));
        ApplyMaterial(generator, propMaterial);
        GeneratorInteractable generatorInteractable = generator.AddComponent<GeneratorInteractable>();
        AudioSource generatorAudio = generator.AddComponent<AudioSource>();
        generatorAudio.clip = CreateToneClip(120f, 2f);
        generatorAudio.loop = true;
        generatorInteractable.SetGeneratorAudio(generatorAudio);

        GameObject talisman = CreateInteractableCube("Talisman", new Vector3(-1.2f, 0.6f, -1.5f));
        ApplyMaterial(talisman, propMaterial);
        TalismanInteractable talismanInteractable = talisman.AddComponent<TalismanInteractable>();
        talismanInteractable.SetSubtitleManager(subtitleManager);

        GameObject map = CreateInteractableCube("Map", new Vector3(0.8f, 0.6f, -1.2f));
        ApplyMaterial(map, propMaterial);
        MapInteractable mapInteractable = map.AddComponent<MapInteractable>();
        mapInteractable.SetSubtitleManager(subtitleManager);

        GameObject lightSwitch = CreateInteractableCube("LightSwitch", new Vector3(-2f, 1.4f, 0.5f));
        ApplyMaterial(lightSwitch, propMaterial);
        LightSwitchInteractable lightSwitchInteractable = lightSwitch.AddComponent<LightSwitchInteractable>();
        lightSwitchInteractable.SetTargetLight(interiorLight);

        GameObject generatorMarker = CreateMarker("GeneratorMarker", new Vector3(-6f, 1.8f, 6f), Color.yellow);
        GameObject outpostMarker = CreateMarker("OutpostMarker", new Vector3(18f, 1.8f, 18f), Color.cyan);
        ApplyMaterial(generatorMarker, markerMaterial);
        ApplyMaterial(outpostMarker, markerMaterial);

        GameObject outpost = GameObject.CreatePrimitive(PrimitiveType.Cube);
        outpost.name = "OutpostHut";
        outpost.transform.position = new Vector3(18f, 1.2f, 16f);
        outpost.transform.localScale = new Vector3(4f, 2.5f, 4f);
        ApplyMaterial(outpost, cabinMaterial);
        Collider outpostCollider = outpost.GetComponent<Collider>();
        if (outpostCollider != null)
        {
            outpostCollider.enabled = false;
        }

        GameObject outpostTrigger = new GameObject("OutpostTrigger");
        SphereCollider triggerCollider = outpostTrigger.AddComponent<SphereCollider>();
        triggerCollider.radius = 2f;
        triggerCollider.isTrigger = true;
        outpostTrigger.transform.position = new Vector3(18f, 0.5f, 18f);
        TriggerEvent triggerEvent = outpostTrigger.AddComponent<TriggerEvent>();

        AudioSource windAudio = root.AddComponent<AudioSource>();
        windAudio.clip = CreateNoiseClip(2f, 0.2f);
        windAudio.loop = true;
        windAudio.volume = 0.4f;

        AudioSource radioLoopAudio = root.AddComponent<AudioSource>();
        radioLoopAudio.clip = CreateNoiseClip(1.5f, 0.1f);
        radioLoopAudio.loop = true;
        radioLoopAudio.volume = 0.3f;

        DemoSequenceController demoSequence = root.AddComponent<DemoSequenceController>();
        demoSequence.Configure(subtitleManager, objectiveUI, interiorLight, windAudio, radioLoopAudio, generatorMarker, outpostMarker);

        AddUnityEventListener(radioInteractable, "onRadioUsed", demoSequence.HandleRadioUsed);
        AddUnityEventListener(generatorInteractable, "onGeneratorStarted", demoSequence.HandleGeneratorStarted);
        AddUnityEventListener(talismanInteractable, "onTalismanMoved", demoSequence.HandleTalismanMoved);
        AddUnityEventListener(mapInteractable, "onMapRead", demoSequence.HandleMapRead);
        AddUnityEventListener(triggerEvent, "onTriggered", demoSequence.HandleOutpostReached);

        root.transform.SetParent(null);
    }

    private static GameObject CreateInteractableCube(string name, Vector3 position)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = name;
        obj.transform.position = position;
        obj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        return obj;
    }

    private static GameObject CreateMarker(string name, Vector3 position, Color color)
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        marker.name = name;
        marker.transform.position = position;
        marker.transform.localScale = new Vector3(0.4f, 1f, 0.4f);
        ApplyMaterialColor(marker, color);

        return marker;
    }

    private static void ApplyMaterial(GameObject target, Material material)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer != null && material != null)
        {
            renderer.material = material;
        }
    }

    private static void ApplyMaterialColor(GameObject target, Color color)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }

    private static Material CreateMaterial(Color color)
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        Material material = shader != null ? new Material(shader) : new Material(Shader.Find("Standard"));
        material.color = color;
        return material;
    }

    private static AudioClip CreateNoiseClip(float durationSeconds, float volume)
    {
        int sampleRate = 44100;
        int samples = Mathf.CeilToInt(sampleRate * durationSeconds);
        float[] data = new float[samples];
        for (int i = 0; i < samples; i++)
        {
            data[i] = (Random.value * 2f - 1f) * volume;
        }

        AudioClip clip = AudioClip.Create("NoiseClip", samples, 1, sampleRate, false);
        clip.SetData(data, 0);
        return clip;
    }

    private static AudioClip CreateToneClip(float frequency, float durationSeconds)
    {
        int sampleRate = 44100;
        int samples = Mathf.CeilToInt(sampleRate * durationSeconds);
        float[] data = new float[samples];
        float increment = frequency * 2f * Mathf.PI / sampleRate;
        float phase = 0f;
        for (int i = 0; i < samples; i++)
        {
            data[i] = Mathf.Sin(phase) * 0.3f;
            phase += increment;
        }

        AudioClip clip = AudioClip.Create("ToneClip", samples, 1, sampleRate, false);
        clip.SetData(data, 0);
        return clip;
    }

    private static TextMeshProUGUI CreateText(Transform parent, string name, Vector2 anchoredPosition, int fontSize, TextAlignmentOptions alignment)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent);
        TextMeshProUGUI text = textObj.AddComponent<TextMeshProUGUI>();
        text.fontSize = fontSize;
        text.alignment = alignment;
        RectTransform rect = text.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = anchoredPosition;
        rect.sizeDelta = new Vector2(800f, 120f);
        text.text = string.Empty;
        text.enabled = false;
        return text;
    }

    private static void AddUnityEventListener(Object target, string fieldName, UnityAction action)
    {
        var field = target.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (field?.GetValue(target) is UnityEvent unityEvent)
        {
            unityEvent.AddListener(action);
        }
    }

}
