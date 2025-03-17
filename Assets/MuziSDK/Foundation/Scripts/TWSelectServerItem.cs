using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Matchmaker.Allocation;
using static Matchmaker.Allocation.MatchMakerAllocationService;
using System.Threading.Tasks;
using Grpc.Core;
using Muziverse.Proto.GameContent.Domain;

public class TWSelectServerItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Image imageIcon;
    public TMP_Text textName;
    public TMP_Text textDesciption;
    public Image imageBadge;
    public TMP_Text textBadgeLabel;
    GameContentIslandInstance fleetmodel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public async void OnClickAsync()
    {
        //TWFastLoading twfast = TW.AddFastLoading();
        var client = new MatchMakerAllocationServiceClient(FoundationManager.channel);
        try
        {
            MatchMakerAllocationResponse reply = await client.GetAllocationAsync(new MatchMakerAllocationRequest
            {
                Type = RequestType.ServerConnect,
                FleetId = fleetmodel.Code
            }, FoundationManager.metadata);
            FoundationManager.cityMatchMakerResponse = reply;
            
            //twfast.ClickX();
            TW.AddLoading().LoadScene("Main");
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log("Sent:Type:" + RequestType.ServerConnect + " Code:" + fleetmodel.Code + " Authorization:" + FoundationManager.metadata.GetValue("Authorization"));
            Debug.Log(j.errorMessage);
            //twfast.ClickX();
            TW.I.AddWarning("", "Cannot allocate server: " + j.errorMessage);

        }



    }
    public void Init(GameContentIslandInstance fleetmodel)
    {
        this.fleetmodel = fleetmodel;
        textName.text = fleetmodel.IslandInstanceName;
        textDesciption.text = fleetmodel.Code;


    }

}
