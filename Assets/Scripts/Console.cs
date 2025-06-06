using Microsoft.MixedReality.OpenXR.BasicSample;
using Microsoft.MixedReality.Toolkit;
using QRTracking;
using System.Linq;
using UnityEngine;

public class Console : MonoBehaviour
{
    [SerializeField] QRCodesManager _qrCodeManager;
    [SerializeField] GameObject _prefab;
    [SerializeField] QRCodesVisualizer _qrVisualizer;
    [SerializeField] MixedRealityToolkitConfigurationProfile _wireFrame;
    [SerializeField] MixedRealityToolkitConfigurationProfile _default;
    [SerializeField] MixedRealityToolkitConfigurationProfile _diagnostics;
    [SerializeField] MixedRealityToolkitConfigurationProfile _noSpatial;
    [SerializeField] MixedRealityToolkitConfigurationProfile _noSkybox;
    [SerializeField] MixedRealityToolkit _toolkit;
    [SerializeField] ErrorNotifier _errorNotifier;


    public enum MRTKModule
    {
        WireFrame,Default,Diagnostics,NoSpatial,NoSkybox
    }
    
    public void SpawnModel(int index)
    {
        try
        {
            int clampedIndex = Mathf.Clamp(index, 0, HandMenu.Instance.AllDatasetButtons.Count - 1);
            HandMenu.Instance.AllDatasetButtons[clampedIndex].LoadDataset();
            HandMenu.Instance.AllDatasetButtons[clampedIndex].SetButtonState(DatasetButton.LoadButtonState.Active);
        }
        catch
        {
            _errorNotifier.AddErrorMessageToUser("Hand menu must be initialized before spawning anything");
        }
    }
    public void SetQr(int index)
    {
        HandMenu.Instance.OnAnyQrActivated(index);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void SetModule(MRTKModule module)
    {
        if (module == MRTKModule.WireFrame)
            _toolkit.ActiveProfile = _wireFrame;
        else if (module == MRTKModule.Default)
            _toolkit.ActiveProfile = _default;
        else if (module == MRTKModule.Diagnostics)
            _toolkit.ActiveProfile = _diagnostics;
        else if (module == MRTKModule.NoSpatial)
            _toolkit.ActiveProfile = _noSpatial;
        else if(module==MRTKModule.NoSkybox)
            _toolkit.ActiveProfile= _noSkybox;
    }
    
    public void Disconnect()
    {
        FindObjectOfType<AppRemotingSample>().OnDisconnectButtonPressed();
    }
    
    public void ResetObjectsTransform()
    {
        FindObjectsOfType<VolumeDataControl>().ToList().ForEach(x=>x.ResetAllTransforms());
    }
    public void SetVolumePosition(Vector3 position,int volumeIndex)
    {
        FindObjectsOfType<VolumeDataControl>().ToList()[volumeIndex].SetVolumePosition(position);
    }
}
