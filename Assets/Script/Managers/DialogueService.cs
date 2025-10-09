using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking; // for Android/WebGL StreamingAssets

public sealed class DialogueService : MonoBehaviour
{
    public static DialogueService Instance { get; private set; }

    [SerializeField] private string _defaultLanguage = "en";
    public DialogueDictionary Dictionary { get; } = new DialogueDictionary();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        string lang = PlayerPrefs.GetString("language", _defaultLanguage);
        StartCoroutine(LoadLanguageCoroutine(lang));
    }

    public void SetLanguage(string lang)
    {
        PlayerPrefs.SetString("language", lang);
        StartCoroutine(LoadLanguageCoroutine(lang));
    }

    private IEnumerator LoadLanguageCoroutine(string lang)
    {
        string file = $"dialogue_{lang}.json";
        string path = Path.Combine(Application.streamingAssetsPath, "Dialogue", file);

        string json;
#if UNITY_ANDROID || UNITY_WEBGL
        using (var req = UnityWebRequest.Get(path))
        {
            yield return req.SendWebRequest();
            if (req.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to load {path}: {req.error}");
                yield break;
            }
            json = req.downloadHandler.text;
        }
#else
        Debug.Log($"Path: {path}");
        json = File.ReadAllText(path);
        yield return null;
#endif

        Dictionary.FromJson(json);
        // Optionally: raise an event here to notify UI of language change
    }

    public string GetLine(int id)
    {
        return Dictionary.TryGetLine(id, out var line) ? line : $"[Missing {id}]";
    }
}
