The script handling networking is NetworkManager.cs
To init hosting, call NetworkManager.Host(), which hosts on the port RemotePort
To connect to a server, call NetworkManager.Connect(), which connects to RemoteAddress:RemotePort
When a client connects, the ConnectionEvent event is fired
To send data, call NetworkManager.Send(string data), with the data to send, which also invokes the SendEvent event
When data is recieved, the MesageEvent event is invoked, passing in the string recieved
To disconnect from the server, call NetworkManager.Disconnect(), which calls the DisconnnectionEvent on other clients