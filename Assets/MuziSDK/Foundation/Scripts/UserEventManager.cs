using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserEventManager : Singleton<UserEventManager>
{
    public List<UserEventItem> events = new List<UserEventItem>(); 
    public Action<UserEventItem> UserJustDoAnEvent; 
    public void AddAEvent(UserEventType type, string ExtraString="", int ExtraInt=-1)
    {
        UserEventItem cevent = new UserEventItem { type = type, ExtraString = ExtraString, ExtraInt = ExtraInt };
        //if ((int)type >= 100)
        //{
        //    events.Add(cevent);
        //}
        UserJustDoAnEvent?.Invoke(cevent);
        
        
    }
}
public struct UserEventItem
{
    public UserEventType type;
    public string ExtraString;
    public int ExtraInt;
}
public enum UserEventType 
{
    NULL = 0,
    DANCE = 1,
    JUMP = 2,

    GOTO_DANCING_HALL = 100,
    GOTO_GAME_CENTER = 101,
    GOTO_GAME_BATTLE_GROUND = 102,
    GOTO_CONCERT_HALL = 103,
    GOTO_LUXURY_APARTMENT = 104,
    FINISH_A_MINIGAME = 105,
    ADD_A_NEW_FRIEND = 106,
    USE_AN_ITEM = 107,
    CHANGE_CLOTH = 108,
    MEET_AN_NPC=109
}