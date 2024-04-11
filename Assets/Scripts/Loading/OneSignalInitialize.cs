using OneSignalSDK;
using UnityEngine;

public class OneSignalInitialize : MonoBehaviour
{
    public string APP_ID;
    public static OneSignalInitialize Instance;
    [SerializeField] private bool _wasApprove = false;
    private void Awake()
    {
        Instance = this;

        if (PlayerPrefs.HasKey("InitPushes"))
            _wasApprove = true;
        else
            _wasApprove = false;
    }

    void Start()
    {
        if (_wasApprove)
            InitOneSignal();
    }

    public void InitOneSignalWithPrompPushes()
    {
        OneSignal.Initialize(APP_ID);
        NotificationsRequestPermission();
        PlayerPrefs.SetInt("InitPushes", 1);
    }
    public void NotificationsRequestPermission()
    {
        OneSignal.Notifications.RequestPermissionAsync(true);
    }

    public void InitOneSignal()
    {
        OneSignal.Initialize(APP_ID);
    }
}