using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

/// <summary>
/// Vibrate the XR Controller
/// </summary>
public class VibrateController : MonoBehaviour
{
    public float strongVibrate = 0.75f;
    public float weakVibrate = 0.25f;

    private HapticImpulsePlayer hapticPlayer;

    private void Awake()
    {
        hapticPlayer = GetComponent<HapticImpulsePlayer>();
    }

public void Vibrate(float amplitude, float duration)
    {
        // OpenVR currently only supports amplitude
        hapticPlayer.SendHapticImpulse(amplitude, duration);
    }

    public void VibrateWeak(float duration)
    {
        hapticPlayer.SendHapticImpulse(weakVibrate, duration);
    }

    public void VibrateStrong(float duration)
    {
        hapticPlayer.SendHapticImpulse(strongVibrate, duration);
    }
}
