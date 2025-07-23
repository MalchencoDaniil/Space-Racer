using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private AudioMixer _audioMixer;

    [Header("Volume Controls")]
    public List<VolumeControl> volumeControls = new List<VolumeControl>();

    private void Start()
    {
        InitializeVolumeControls();
    }

    private void InitializeVolumeControls()
    {
        foreach (var control in volumeControls)
        {
            // Check if references are assigned and parameter name is valid
            if (control._slider != null && control._volumeText != null && !string.IsNullOrEmpty(control._parameterName))
            {
                float initialVolume;
                // Try to get the initial volume from the AudioMixer
                bool result = _audioMixer.GetFloat(control._parameterName, out initialVolume);

                if (result)
                {
                    // Load the saved volume if available
                    float savedVolume = LoadVolume(control);
                    control._slider.value = savedVolume;
                    OnSliderValueChanged(control, savedVolume); // Apply the loaded volume immediately
                }
                else
                {
                    // If parameter not found in mixer, use slider's current value or default
                    Debug.LogWarning($"AudioMixer parameter '{control._parameterName}' not found. Using slider default or previous value.");
                    // If you want to apply the slider's initial value from Inspector:
                    // control._slider.value = control._slider.value; // This line is actually redundant here
                    OnSliderValueChanged(control, control._slider.value); // Ensure UI text is updated
                }

                // Add listener for slider value changes
                // We need to create a local copy of control to capture its current state for the lambda
                var currentControl = control;
                control._slider.onValueChanged.AddListener(value => OnSliderValueChanged(currentControl, value));
            }
            else
            {
                // Log warnings for missing references or parameter names
                if (control._slider == null) Debug.LogWarning($"VolumeControl for parameter '{control._parameterName}' is missing a Slider reference.");
                if (control._volumeText == null) Debug.LogWarning($"VolumeControl for parameter '{control._parameterName}' is missing a Volume Text reference.");
                if (string.IsNullOrEmpty(control._parameterName)) Debug.LogWarning("A VolumeControl is missing its parameter name. This is crucial for saving/loading.");
            }
        }
    }

    public void OnSliderValueChanged(VolumeControl control, float value)
    {
        // Set the volume in the AudioMixer
        _audioMixer.SetFloat(control._parameterName, value);

        // Update the volume text display
        UpdateVolumeText(control, value);

        // Save the new volume value to PlayerPrefs
        SaveVolume(control, value);
    }

    private void UpdateVolumeText(VolumeControl control, float volumeInDecibels)
    {
        // Convert dB to a 0-1 range, then to percentage
        // Assuming -80dB is silence (0%) and 0dB is max (100%)
        // PlayerPrefsKey needs to be accessed on the control instance
        float volumePercentage = Mathf.InverseLerp(-80f, 0f, volumeInDecibels) * 100f; // Adjust -80f if your mixer uses a different minimum dB
        control._volumeText.text = Mathf.RoundToInt(volumePercentage).ToString();
    }

    // --- PlayerPrefs Saving and Loading ---

    private void SaveVolume(VolumeControl control, float volumeValue)
    {
        if (!string.IsNullOrEmpty(control._parameterName))
        {
            PlayerPrefs.SetFloat(control.PlayerPrefsKey, volumeValue);
            PlayerPrefs.Save(); // Save changes immediately
            // Debug.Log($"Saved volume for '{control._parameterName}': {volumeValue}");
        }
    }

    private float LoadVolume(VolumeControl control)
    {
        if (!string.IsNullOrEmpty(control._parameterName))
        {
            // Check if the key exists before trying to get it
            if (PlayerPrefs.HasKey(control.PlayerPrefsKey))
            {
                float savedVolume = PlayerPrefs.GetFloat(control.PlayerPrefsKey);
                // Debug.Log($"Loaded volume for '{control._parameterName}': {savedVolume}");
                return savedVolume;
            }
            else
            {
                // If no saved volume found, return the slider's initial value
                // or a default value if the slider itself hasn't been set yet.
                // Using the slider's value ensures we respect any initial Inspector settings.
                // Debug.Log($"No saved volume found for '{control._parameterName}'. Using slider's initial value.");
                return control._slider.value;
            }
        }
        else
        {
            // If parameter name is missing, return slider's default value
            return control._slider.value;
        }
    }
}