TestSend()
{
	Send, -
	Sleep, 2000
	Click, X1
	Sleep, 2000
	Send, \
	Sleep, 2000
	return
}

WatchThisReplay(id, playerSlot)
{
	Send playdemo
	Sleep, 1000
	Send {Space}
	Sleep, 2000
	SendRaw "/replays/%id%.dem"  
	Sleep, 2000
	Send {Enter}
	Sleep, 15000
	/*
	SendRaw dota_spectator_increasereplayspeed 8	
	Sleep, 1000
	Send {Enter}
	SendRaw dota_spectator_increasereplayspeed 8
	Send {Enter}	
	Sleep, 1000
	SendRaw dota_spectator_increasereplayspeed 8	
	Send {Enter}
	Sleep, 1000
	SendRaw dota_spectator_increasereplayspeed 8	
	Send {Enter}
	Sleep, 1000
	SendRaw dota_spectator_increasereplayspeed 8	
	Sleep, 1000
	Send {Enter}
	*/
	Loop, %playerSlot%
	{
		SendRaw dota_spectator_selectnexthero
		Sleep, 300
		Send {Enter}
		Sleep, 300
	}
	SendRaw dota_spectator_mode 3
	Sleep, 1000
	Send {Enter}
	Send, \
	return
}

StartStream()
{
	Send ^{[}
	return
}

StopStream()
{
	Send ^{]}
	return
}


