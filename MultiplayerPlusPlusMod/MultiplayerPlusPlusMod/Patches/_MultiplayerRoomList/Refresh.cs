using HarmonyLib;

namespace MultiplayerPlusPlusMod.Patches._MultiplayerRoomList
{
    [HarmonyPatch(typeof(MultiplayerRoomList), "Refresh")]
    class Refresh
    {
        private static bool Prefix(MultiplayerRoomList __instance)
        {
            MultiplayerManager.Instance.UpdateRoomList(false);
            __instance.UpdateList();
            return false;
        }
    }
}
