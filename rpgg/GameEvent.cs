using Godot;
using System;

public class GameEvent
{
	public int baseAttack = -1;
	public String imgPath = "";
	
	public GameEvent() {}
	
	public GameEvent(int baseAttack_, String imgPath_)
   {
		baseAttack = baseAttack_;
		imgPath = imgPath_;
   }
}
