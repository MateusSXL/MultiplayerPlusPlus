namespace MultiplayerPlusPlusMod.Controllers
{
    class RoomController : MonoBehaviourSingleton<RoomController>
    {
        //Debug
        /*private string debugInfo = "";
        private GUIStyle debugInfolStyle = new GUIStyle();
        public string extraInfo { get; set; } = "";*/

        private void Start()
        {
            //debugInfolStyle.normal.textColor = Color.green;
            ShowPublicRoomsHiddenButtons();
        }

       /* private void OnGUI()
        {
            GUI.TextField(new Rect(5, 5, 500, 200), debugInfo, debugInfolStyle);
        }

        private void Update()
        {
            if(!MultiplayerManager.Instance.InRoom && Input.GetKeyDown(KeyCode.F9))
            {
                MultiplayerManager.Instance.CreateRoom(false);
            }

            try
            {
                UpdateDebugInfo();
            }
            catch (Exception e)
            {
                Main.logger.Log(e.ToString());
                Main.logger.LogException(e);
            }
        }*/

        /*private void FixedUpdate()
        {
            if (PhotonIsConnected()) roomListRefreshTimer += Time.deltaTime;
            else roomListRefreshTimer = 0f;
        }*/

        /*private void UpdateDebugInfo()
        {
            string photonInfo = "";
            if (PhotonIsConnected())
            {
                photonInfo = "Rooms : " + Photon.Pun.PhotonNetwork.CountOfRooms
                    + " | Players : " + Photon.Pun.PhotonNetwork.CountOfPlayers
                    + "\n currentItemCount : " + MultiplayerManager.Instance.menuController.roomList.listView.currentItemCount
                    + " | HeaderView.Label.text : " + MultiplayerManager.Instance.menuController.roomList.listView.HeaderView.Label.text
                    + " | HeaderView.Label.text : " + MultiplayerManager.Instance.menuController.roomList.listView.ItemPrefab.Label.text
                    ;

                *//*if (roomListRefreshTimer > 3f)
                {
                    MultiplayerManager.Instance.UpdateRoomList(false);
                    roomListRefreshTimer = 0f;
                }*//*


            }

            string roomList = "";
            foreach (Photon.Realtime.RoomInfo room in MultiplayerManager.Instance.roomList)
            {
                roomList += "\n"
                    + "        code: " + room.Name
                    + " | map: " + room.CustomProperties[(object)MultiplayerManager.MAPNAME_PROP_KEY] as string
                    + " | players: " + room.PlayerCount + "/" + room.MaxPlayers
                    ;
            }

            if (PlayerController.Instance.currentStateEnum == PlayerController.CurrentState.Setup)
                extraInfo = "";

            debugInfo = "Player State : " + PlayerController.Instance.currentStateEnum
                //+ "\n refresh timer : " + roomListRefreshTimer
                + "\n photonInfo : " + photonInfo
                + "\n rooms count : " + MultiplayerManager.Instance.roomList.Count
                + "\n rooms : " + roomList
                + "\n extraInfo : " + extraInfo
                ;
        }*/

        /*private bool PhotonIsConnected()
        {
            return Photon.Pun.PhotonNetwork.IsConnected;
        }*/

        private void ShowPublicRoomsHiddenButtons()
        {
            foreach (MultiplayerMainMenu.ButtonVisibilityDef buttonVisibilityDef in MultiplayerManager.Instance.menuController.mainMenu.options)
            {
                if(buttonVisibilityDef.name == "Browse Rooms Button" || buttonVisibilityDef.name == "Create Room Button")
                    buttonVisibilityDef.showOnlyWithDebugCheats = false;
                /*Main.logger.Log("[Button] name: " + buttonVisibilityDef.name 
                    + " | showInGameMode: " + buttonVisibilityDef.showInGameMode
                    + " | showInLobby: " + buttonVisibilityDef.showInLobby
                    + " | showInPrivateRoom: " + buttonVisibilityDef.showInPrivateRoom
                    + " | showInPublicRoom: " + buttonVisibilityDef.showInPublicRoom
                    + " | showNotForMasterClient: " + buttonVisibilityDef.showNotForMasterClient
                    + " | showOnlyForMasterClient: " + buttonVisibilityDef.showOnlyForMasterClient
                    + " | showOnlyWithDebugCheats: " + buttonVisibilityDef.showOnlyWithDebugCheats
                    + " | showWhenDisconnected: " + buttonVisibilityDef.showWhenDisconnected)
                    ;*/
            }
        }
    }
}
