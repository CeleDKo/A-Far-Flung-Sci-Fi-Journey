using UnityEngine;

public class WebView : MonoBehaviour
{
    public static WebView instance;
    public int Times;
    private void Awake()
    {
        instance = this;
    }
    public void DefindAndOpen()
    {
        PlayerPrefs.SetString("End", "True");
        PlayerPrefs.SetString("WasShowed", "1");

        string url;

        if (PlayerPrefs.HasKey("CacheU"))
            url = PlayerPrefs.GetString("CacheU");
        else
            url = FirebaseInitialize.instance.GetStringByKey("info");

        OpenWebView(url);
    }
    [SerializeField] UniWebView webView;
    [SerializeField] SafeAreaWebView safeArea;

    public void OpenWebView(string Url)
    {
        webView.Frame = new Rect(0, 0, Screen.width, Screen.height);

        webView.Load(Url);
        webView.Show();
        webView.EmbeddedToolbar.Hide();
        UniWebView.SetJavaScriptEnabled(true);

        webView.OnPageFinished += (view, statusCode, url) =>
        {
            if (!PlayerPrefs.HasKey("CacheU"))
            {
                if (Times == 1)
                {
                    if (!url.Contains("widgets-04"))
                        PlayerPrefs.SetString("CacheU", url);
                }
                Times++;
            }
        };

        webView.OnOrientationChanged += (view, orientation) =>
        {
            safeArea.RefreshRectTransform();
            webView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        };

        webView.OnShouldClose += (view) =>
        {
            return false;
        };
    }
}