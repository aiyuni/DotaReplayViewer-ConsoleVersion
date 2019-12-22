TestSend()
{
	Send, -
	sleep, 2000
	Click, X1
	sleep, 2000
	Click, X2
	sleep, 2000
	
	return
}

WatchThisReplay(id, playerSlot)
{
	Send playdemo
	sleep, 1000
	Send {Space}
	sleep, 2000
	SendRaw "C:/Program Files (x86)/Steam/steamapps/common/dota 2 beta/game/dota/replays/%id%.dem"  ;C:\Program Files (x86)\Steam\steamapps\common\dota 2 beta\game\dota\replays
	sleep, 2000
	Send {Enter}
	sleep, 15000
	/*SendRaw dota_spectator_increasereplayspeed 8	
	sleep, 1000
	Send {Enter}
	SendRaw dota_spectator_increasereplayspeed 8
	Send {Enter}	
	sleep, 1000
	SendRaw dota_spectator_increasereplayspeed 8	
	Send {Enter}
	sleep, 1000
	SendRaw dota_spectator_increasereplayspeed 8	
	Send {Enter}
	sleep, 1000
	SendRaw dota_spectator_increasereplayspeed 8	
	sleep, 1000
	Send {Enter}
	*/
	Loop, %playerSlot%
	{
		SendRaw dota_spectator_selectnexthero
		sleep, 300
		Send {Enter}
		sleep, 300
	}
	SendRaw dota_spectator_mode 3
	sleep, 1000
	Send {Enter}
	Click, X2
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


