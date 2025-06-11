using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardManager : MonoBehaviour
{
    [SerializeField] private GameObject _volumePrefab;
    private VolumeDataControl _volumeDataControl;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera mainCamera = Camera.main;
            Vector3 rot = mainCamera.transform.rotation.eulerAngles;
            rot.y += 90;
            rot.x= 0;
            rot.z = 0;

            GameObject spawned = Instantiate(_volumePrefab, mainCamera.transform.position+(mainCamera.transform.forward), Quaternion.Euler(rot));

            _volumeDataControl = spawned.GetComponent<VolumeDataControl>();
            _volumeDataControl.LoadDatasetAsync(DatasetPath, ThumbnailTexture, DatasetName.text, mainCamera); 
        }
    }
}
