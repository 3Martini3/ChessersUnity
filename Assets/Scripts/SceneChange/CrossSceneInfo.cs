using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// stores server info, transfer id betweeen scenes
/// </summary>
public static class CrossSceneInfo
{
    public static bool onlineGame { get; set; }
    public static string IP { get; set; }
    public static int Port { get; set; }
    public static int myId { get; set; }
    public static string playerId { get; set; }
}
