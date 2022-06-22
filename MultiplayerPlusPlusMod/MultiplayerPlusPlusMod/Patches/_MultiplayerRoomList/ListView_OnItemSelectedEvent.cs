using HarmonyLib;

namespace MultiplayerPlusPlusMod.Patches._MultiplayerRoomList
{
    [HarmonyPatch(typeof(MultiplayerRoomList), "ListView_OnItemSelectedEvent")]
    class ListView_OnItemSelectedEvent
    {
        private static bool Prefix(IndexPath index)
        {
            if (MultiplayerManager.Instance.InRoom)
                MultiplayerManager.Instance.LeaveRoom();

            MultiplayerManager.Instance.JoinSpecificRoom(MultiplayerManager.Instance.roomList[index[0]].Name);
            return false;
        }
    }
}
