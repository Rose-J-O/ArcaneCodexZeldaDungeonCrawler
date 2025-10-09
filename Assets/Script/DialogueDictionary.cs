using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// Plain C# container for id -> line. JSON-friendly via wrapper structs.
/// You can load one per language at runtime with your GameManager later.
[Serializable]
public class DialogueDictionary
{
    [Serializable]
    public struct Entry
    {
        public int id;
        public string line;
    }

    [Serializable]
    private class EntryList
    {
        public List<Entry> entries = new List<Entry>();
    }

    private readonly Dictionary<int, string> _byId = new Dictionary<int, string>();
    private readonly Dictionary<string, int> _byLine = new Dictionary<string, int>(StringComparer.Ordinal);

    public IReadOnlyDictionary<int, string> ById => _byId;

    public bool TryGetLine(int id, out string line)
    {
        return _byId.TryGetValue(id, out line);
    }

    public bool TryGetId(string line, out int id)
    {
        return _byLine.TryGetValue(line, out id);
    }

    public int GetOrAdd(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            throw new ArgumentException("Line must not be empty.", nameof(line));

        if (_byLine.TryGetValue(line, out int existing))
            return existing;

        // Next available ID: 0 if empty; otherwise (max existing + 1) to keep IDs unique and increasing.
        int next = _byId.Count == 0 ? 0 : _byId.Keys.Max() + 1;
        _byId[next] = line;
        _byLine[line] = next;
        return next;
    }

    public void Clear()
    {
        _byId.Clear();
        _byLine.Clear();
    }

    // ---------- JSON IO (Unity JsonUtility; uses list wrapper) ----------

    public string ToJson(bool prettyPrint = true)
    {
        var wrapper = new EntryList
        {
            entries = _byId.Select(kv => new Entry { id = kv.Key, line = kv.Value }).ToList()
        };
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    public void FromJson(string json)
    {
        Clear();
        if (string.IsNullOrWhiteSpace(json))
            return;

        var wrapper = JsonUtility.FromJson<EntryList>(json);
        if (wrapper?.entries == null) return;

        foreach (var e in wrapper.entries)
        {
            if (!_byId.ContainsKey(e.id))
                _byId.Add(e.id, e.line ?? string.Empty);

            if (!string.IsNullOrEmpty(e.line) && !_byLine.ContainsKey(e.line))
                _byLine.Add(e.line, e.id);
        }
    }
}
