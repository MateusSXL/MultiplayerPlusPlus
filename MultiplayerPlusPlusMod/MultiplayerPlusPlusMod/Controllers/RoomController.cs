using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
//using UnityEngine;

namespace MultiplayerPlusPlusMod.Controllers
{
    class RoomController : MonoBehaviourSingleton<RoomController>, IConnectionCallbacks, ILobbyCallbacks
    {
        private LoadBalancingClient client;
        //Debug
        /*private string debugInfo = "";
        private GUIStyle debugInfolStyle = new GUIStyle();
        public string extraInfo { get; set; } = "";*/

        private void Start()
        {
            client = new LoadBalancingClient();
            client.AddCallbackTarget(this);
            ShowPublicRoomsHiddenButtons();
            //debugInfolStyle.normal.textColor = Color.green;
        }

        /*private void OnGUI()
        {
            GUI.TextField(new Rect(5, 15, 500, 200), debugInfo, debugInfolStyle);
        }*/

        private void Update()
        {
            if(client.IsConnected) client.Service();
            //UpdateDebugInfo();
        }

        /*private void UpdateDebugInfo()
        {
            string photonInfo = "";
            if (PhotonNetwork.IsConnected)
            {
                photonInfo = "Rooms : " + PhotonNetwork.CountOfRooms
                    + " | Players : " + PhotonNetwork.CountOfPlayers
                    + "\n MasterServerAddress : " + PhotonNetwork.NetworkingClient.MasterServerAddress
                    + " | AppVersion : " + PhotonNetwork.NetworkingClient.AppVersion
                    + " | CloudRegion : " + PhotonNetwork.NetworkingClient.CloudRegion
                    ;


            }

            *//*string roomList = "";
            foreach (RoomInfo room in MultiplayerManager.Instance.roomList)
            {
                roomList += "\n"
                    + "        code: " + room.Name
                    + " | map: " + room.CustomProperties[(object)MultiplayerManager.MAPNAME_PROP_KEY] as string
                    + " | players: " + room.PlayerCount + "/" + room.MaxPlayers
                    ;
            }*//*

            if (PlayerController.Instance.currentStateEnum == PlayerController.CurrentState.Setup)
                extraInfo = "";

            debugInfo = "Player State : " + PlayerController.Instance.currentStateEnum
                + "\n photonInfo : " + photonInfo
                + "\n rooms count : " + MultiplayerManager.Instance.roomList.Count
                //+ "\n rooms : " + roomList
                + "\n extraInfo : " + extraInfo
                ;
        }*/

        private void ShowPublicRoomsHiddenButtons()
        {
            foreach (MultiplayerMainMenu.ButtonVisibilityDef buttonVisibilityDef in MultiplayerManager.Instance.menuController.mainMenu.options)
            {
                if(buttonVisibilityDef.name == "Browse Rooms Button" || buttonVisibilityDef.name == "Create Room Button")
                    buttonVisibilityDef.showOnlyWithDebugCheats = false;
            }
        }

        public void RefreshRoomListWhileInRoom()
        {
            if (PhotonNetwork.InRoom && !client.IsConnected)
            {
                client.AppId = PhotonNetwork.NetworkingClient.AppId;
                client.AppVersion = PhotonNetwork.NetworkingClient.AppVersion;
                client.MasterServerAddress = PhotonNetwork.NetworkingClient.MasterServerAddress;
                client.ConnectToRegionMaster(PhotonNetwork.NetworkingClient.CloudRegion);
            }

        }

        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
            client.OpJoinLobby(MultiplayerManager.TYPEDLOBBY);
        }

        public void OnJoinedLobby()
        {
            client.OpGetGameList(MultiplayerManager.TYPEDLOBBY, "TRUE");
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            if (PhotonNetwork.InRoom)
                roomList.Remove(roomList.Find(room => room.Name == PhotonNetwork.CurrentRoom.Name));

            MultiplayerManager.Instance.roomList = roomList;
            MultiplayerManager.Instance.menuController.roomList.UpdateList();
            client.Disconnect(DisconnectCause.None);
        }

        public void OnDisconnected(DisconnectCause cause)
        {
        }

        public void OnLeftLobby()
        {
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }
    }
}
