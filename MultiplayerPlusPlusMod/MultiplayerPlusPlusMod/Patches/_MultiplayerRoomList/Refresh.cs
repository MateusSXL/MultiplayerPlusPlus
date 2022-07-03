using HarmonyLib;
using Photon.Pun;

namespace MultiplayerPlusPlusMod.Patches._MultiplayerRoomList
{
    [HarmonyPatch(typeof(MultiplayerRoomList), "Refresh")]
    class Refresh
    {
        private static bool Prefix(MultiplayerRoomList __instance)
        {
            if (PhotonNetwork.InRoom) 
                Controllers.RoomController.Instance.RefreshRoomListWhileInRoom();
            else
            {
                MultiplayerManager.Instance.UpdateRoomList(false);
                __instance.UpdateList();
            }

            return false;
        }
    }
}
