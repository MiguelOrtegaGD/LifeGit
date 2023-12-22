using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] List<ButtonSound> _buttons = new List<ButtonSound>();

    private void Start()
    {
        InitializeComponent();
    }
    public void InitializeComponent()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            EventTrigger _currentTrigger = _buttons[i].Button.gameObject.AddComponent<EventTrigger>();

            for (int a = 0; a < _buttons[i].Events.Count; a++)
            {
                string _clipName = _buttons[i].Events[a].Clip;
                EventTrigger.Entry _newEntry = new EventTrigger.Entry();

                _newEntry.eventID = _buttons[i].Events[a].EventType;

                _newEntry.callback.AddListener(delegate
                {
                    AudioController.Instance.PlayMainSfx(_clipName);
                });

                _currentTrigger.triggers.Add(_newEntry);
            }
        }
    }
}

[Serializable]
public class ButtonSound
{
    [SerializeField] Button _button;
    [SerializeField] List<EventSound> _events = new List<EventSound>();

    public Button Button { get => _button; set => _button = value; }
    public List<EventSound> Events { get => _events; set => _events = value; }
}

[Serializable]
public class EventSound
{
    [SerializeField] EventTriggerType _eventType;
    [SerializeField] string _clip;

    public EventTriggerType EventType { get => _eventType; set => _eventType = value; }
    public string Clip { get => _clip; set => _clip = value; }
}
