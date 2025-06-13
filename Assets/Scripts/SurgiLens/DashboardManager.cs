using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DashboardManager : MonoBehaviour
{
    [SerializeField] private Button _spawnDatasetButton;
    
    [SerializeField] private Button _toggleUIButton;
    [SerializeField] private GameObject _dashboardPanel;
    
    [SerializeField] private GameObject _volumePrefab;
    private VolumeDataControl _volumeDataControl;

    private string _datasetPath;
    private string _datasetName;

    private void Awake()
    {
        _datasetPath = Path.Combine(Application.dataPath, "StreamingAssets");
        
        _spawnDatasetButton?.onClick.AddListener(SpawnDataset);
        _toggleUIButton?.onClick.AddListener(() =>
        {
            _dashboardPanel.SetActive(!_dashboardPanel.activeSelf);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnDataset();
        }
    }
    
    private void SpawnDataset()
    {
        if (_volumeDataControl != null)
            return;
        
        Camera mainCamera = Camera.main;
        Vector3 rot = mainCamera.transform.rotation.eulerAngles;
        rot.y += 90;
        rot.x= 0;
        rot.z = 0;

        GameObject spawned = Instantiate(_volumePrefab, mainCamera.transform.position+(mainCamera.transform.forward), Quaternion.Euler(rot));

        _volumeDataControl = spawned.GetComponent<VolumeDataControl>();
        _volumeDataControl.LoadDatasetAsync(_datasetPath, null, _datasetName, mainCamera);
    }
}
