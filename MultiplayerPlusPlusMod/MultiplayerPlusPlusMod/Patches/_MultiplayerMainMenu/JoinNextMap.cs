using HarmonyLib;

namespace MultiplayerPlusPlusMod.Patches._MultiplayerMainMenu
{
    [HarmonyPatch(typeof(MultiplayerMainMenu), "JoinNextMap")]
    class JoinNextMap
    {
        private static bool Prefix()
        {
            MultiplayerManager.Instance.menuController.ViewRoomList();
            if (Photon.Pun.PhotonNetwork.InRoom)
                Controllers.RoomController.Instance.RefreshRoomListWhileInRoom();

            return false;
        }
    }
}
