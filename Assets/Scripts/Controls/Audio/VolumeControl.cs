using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct VolumeControl
{
    [Header("UI References")]
    public Slider _slider;
    public TextMeshProUGUI _volumeText;

    [Header("Audio Mixer Settings")]
    public string _parameterName; 
    public string PlayerPrefsKey => $"Volume_{_parameterName}";
}