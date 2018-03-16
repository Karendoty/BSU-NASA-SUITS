﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech; 

public class MenuManager : MonoBehaviour {
    private KeywordRecognizer _keywordRecognizer = null; 
    private readonly Dictionary<string, System.Action> _keywords = new Dictionary<string, System.Action>();
    private bool _visible = false; 

	// Use this for initialization
	void Start () {
        _keywords.Add("Menu", ToggleVisibility);

        _keywordRecognizer = new KeywordRecognizer(_keywords.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        _keywordRecognizer.Start(); 
    }

    private void ToggleVisibility()
    {
        var holoMenu = GameObject.Find("Menu");
        if (holoMenu == null) return;

        _visible = !_visible;
        var rend = holoMenu.GetComponent<Renderer>();
        if (rend != null) rend.enabled = _visible;

        holoMenu.transform.localScale = Vector3.one * 0.3f;
        holoMenu.transform.position =
            Camera.main.transform.position + Camera.main.transform.forward; 
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction; 
        if (_keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke(); 
        }
    }
	
}
