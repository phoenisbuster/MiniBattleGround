using Grpc.Core;
using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using static User.Wallet.UserWalletService;

public class WalletManager : MonoBehaviour
{
    public static WalletManager instance;
    //static public event Action<WalletResponse> OnWalletJustUpdated;

    private readonly string goldSymbolString = "MUZI-GOLD";
    private readonly string diamondSymbolString = "MUZI-DIAMOND";

    void Awake()
    {
        instance = this;
        //Debug.Log("a123");
    }
    void Start()
    {
        FoundationManager.OnConnectedToMuziServer += OnConnectedToMuziServer;
    }
    private void OnDestroy()
    {
        FoundationManager.OnConnectedToMuziServer -= OnConnectedToMuziServer;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnConnectedToMuziServer()
    {
        GetWalletInfo();
    }
    public async void GetWalletInfo()
    {
        if (!FoundationManager.isUsingMuziServer)
        {
            Debug.Log("[TOANSTT TEST] Skip getting wallet");
            return;
        }

        UserWalletServiceClient client = new UserWalletServiceClient(FoundationManager.channel);
        try
        {
            WalletResponse walletResponse = await client.GetWalletAsync(new Google.Protobuf.WellKnownTypes.Empty(), FoundationManager.metadata);
            BigInteger[] balanceArray = new BigInteger[2];
            if (walletResponse.Currencies.Count > 0)
            {
                foreach(var c in walletResponse.Currencies)
                {
                    string value = c.TotalBalance;
                    if (c.Symbol == goldSymbolString)
                        balanceArray[0] = BigInteger.Parse(value.Substring(0, value.IndexOf('.') > 0 ? value.IndexOf('.') : value.Length));
                    else if (c.Symbol == diamondSymbolString)
                        balanceArray[1] = BigInteger.Parse(value.Substring(0, value.IndexOf('.') > 0 ? value.IndexOf('.') : value.Length));
                }
            }
            MenuPanelManager.OnCurrenciesChanged?.Invoke(balanceArray[0], balanceArray[1]);
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage  + "   accessTokenEnd: " + FoundationManager.AccessToken.Substring(FoundationManager.AccessToken.Length-10,10));
            Debug.Log("token:" + FoundationManager.AccessToken);
        }
    }
}
