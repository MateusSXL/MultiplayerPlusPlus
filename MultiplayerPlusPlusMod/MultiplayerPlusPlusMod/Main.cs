using UnityEngine;
using HarmonyLib;
using System.Reflection;
using UnityModManagerNet;
using MultiplayerPlusPlusMod.Controllers;

namespace MultiplayerPlusPlusMod
{
    public class Main
    {
        //public static UnityModManager.ModEntry.ModLogger logger { get; private set; }
        private static RoomController roomController;
        public static UnityModManager.ModEntry modEntry;
        private static Harmony harmony;
        public static bool enabled { get; set; }

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            //logger = modEntry.Logger;
            modEntry.OnToggle = OnToggle;
            Main.modEntry = modEntry;
            return true;
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool enabledInUMM)
        {
            if (enabled != enabledInUMM)
            {
                enabled = enabledInUMM;
                if (enabled)
                {
                    harmony = new Harmony(modEntry.Info.Id);
                    harmony.PatchAll(Assembly.GetExecutingAssembly());
                    roomController = new GameObject().AddComponent<RoomController>();
                    Object.DontDestroyOnLoad((Object)roomController);
                }
                else
                {
                    harmony.UnpatchAll(modEntry.Info.Id);
                    Object.Destroy((Object)roomController);
                }
            }
            return true;
        }
    }
}
