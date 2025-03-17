using Networking;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Text;
using UnityEngine;
public class PortalManager : MonoBehaviour
{
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
    }
    void Start()
    {
        //FoundationManager.Instance.InvokeMe();
        FoundationManager.OnConnectedToMuziServer += OnConnectedToMuziServer;


        NakamaNetworkManager nakamaManager = FindObjectOfType<NakamaNetworkManager>();
        if (nakamaManager != null)
        {
            nakamaManager.DisconnectAndDestroyMe();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnConnectedToMuziServer()
    {
        GetCityDataAsync();
    }
    async void GetCityDataAsync()
    {
        await FoundationManager.Instance.GetCityDataAsync();
    }
    
   

}
