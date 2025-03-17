using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using UnityEngine;
using Useractivity.Quest;
using static Useractivity.Quest.UserQuestService;
using static Useractivity.Quest.UserQuestFilterService;
using Muziverse.Proto.GameContent.Domain;

public class TWPopup_DailyLoginReward : TWBoard
{
    // Start is called before the first frame update
    public TWPopup_DailyLoginReward_Item item;
    public CanvasGroup panelContainerCanvasGroup;

    private List<TWPopup_DailyLoginReward_Item> items = new List<TWPopup_DailyLoginReward_Item>();
    IEnumerator Start()
    {
        base.InitTWBoard();
        GetLoginRewardsList();
        yield return new WaitForSeconds(0.5f);
        panelContainerCanvasGroup.alpha = 1;
    }

    async void GetLoginRewardsList()
    {
        while (!FoundationManager.IsConnectedToMuziServer)
            await Task.Delay(100);

        UserQuestFilterServiceClient client = new UserQuestFilterServiceClient(FoundationManager.channel);
        try
        {
            QuestFilterRequest request = new QuestFilterRequest
            {
                Type = QuestType.Login,
                Status = QuestStatus.Open,
                ZoneOffset = FoundationManager.MyTimeZone
            };
            var response = await client.FilterAsync(request, FoundationManager.metadata);

            foreach (var quest in response.Quests)
            {
                TWPopup_DailyLoginReward_Item i = Instantiate<TWPopup_DailyLoginReward_Item>(item, item.transform.parent);// as GameObject;
                i.Init(quest);
                i.gameObject.SetActive(true);
                items.Add(i);
            }
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }

        try
        {
            UserQuestServiceClient client2 = new UserQuestServiceClient(FoundationManager.channel);
            GetLoginQuestProgressRequest progressRequest = new GetLoginQuestProgressRequest { ZoneOffset = FoundationManager.MyTimeZone };
            var response = await client2.GetLoginQuestProgressAsync(progressRequest, FoundationManager.metadata);
            for (int i = 0; i < items.Count; i++)
            {
                if (i < response.FinishedQuests)
                    items[i].SetClaimedReward();
                else if (i > response.FinishedQuests)
                    items[i].ToggleItem(false);
                else
                {
                    var isTodayClaimed = response.IsTodayClaimed;
                    items[i].ToggleItem(!isTodayClaimed);
                }
            }
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }
    }
}
