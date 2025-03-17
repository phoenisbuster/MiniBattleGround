using System.Collections.Generic;
using Nakama.TinyJson;
using UnityEngine;

/// <summary>
/// A static class that creates JSON string network messages.
/// </summary>
public static class MatchDataJson
{   
    /// <summary>
    /// Creates a network message containing velocity and position.
    /// </summary>
    /// <param name="velocity">The velocity to send.</param>
    /// <param name="position">The position to send.</param>
    /// <returns>A JSONified string containing velocity and position data.</returns>
    public static string VelocityAndPosition(Vector3 velocity, Vector3 position, Quaternion facing)
    {
        var values = new Dictionary<string, string>
        {
            { "velocity.x", velocity.x.ToString() },
            //{ "velocity.y", velocity.y.ToString() },
            { "velocity.z", velocity.z.ToString() },
            { "position.x", position.x.ToString() },
            //{ "position.y", position.y.ToString() },
            { "position.z", position.z.ToString() },
            { "rotation.y", facing.eulerAngles.y.ToString()}
        };

        return values.ToJson();
    }
    
    /// <summary>
    /// Creates a network message containing player input.
    /// </summary>
    public static string Input(float horizontalInput, float verticalInput, bool jump)
    {
        var values = new Dictionary<string, string>
        {
            { "horizontalInput", horizontalInput.ToString() },
            { "verticalInput", verticalInput.ToString() },
            { "jump", jump.ToString() }
        };

        return values.ToJson();
    }

    /// <summary>
    /// Creates a network message specifying that the player died and the position when they died.
    /// </summary>
    /// <param name="position">The position on death.</param>
    /// <returns>A JSONified string containing the player's position on death.</returns>
    public static string Died(Vector3 position)
    {
        var values = new Dictionary<string, string>
        {
            { "position.x", position.x.ToString() },
            { "position.y", position.y.ToString() },
            { "position.z", position.z.ToString()}
        };

        return values.ToJson();
    }

    /// <summary>
    /// Creates a network message specifying that the player respawned and at what spawn point.
    /// </summary>
    /// <param name="spawnIndex">The spawn point.</param>
    /// <returns>A JSONified string containing the player's respawn point.</returns>
    public static string Respawned(int spawnIndex)
    {
        var values = new Dictionary<string, string>
        {
            { "spawnIndex", spawnIndex.ToString() },
        };

        return values.ToJson();
    }

    /// <summary>
    /// Creates a network message indicating a new round should begin and who won the previous round.
    /// </summary>
    /// <param name="winnerPlayerName">The winning player's name.</param>
    /// <returns>A JSONified string containing the winning players name.</returns>
    public static string StartNewRound(string winnerPlayerName)
    {
        var values = new Dictionary<string, string>
        {
            { "winningPlayerName", winnerPlayerName }
        };        
        return values.ToJson();
    }
}
