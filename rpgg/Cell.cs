using Godot;
using System;

public class Cell
{
	private bool isValid = true;
	public Vector2 position = new Vector2(-1, -1);
	public float size = -1;
	public bool isGreen = false;
	public GameEvent gameEvent = new GameEvent();
	
	public Cell() {}
	
	public Cell(Vector2 position_, float size_, bool isGreen_, GameEvent e)
   {
		position = position_;
		size = size_;
		isGreen = isGreen_;
		gameEvent = e;
   }

	public bool is_valid() {
		return isValid;
	}
	public bool is_green(){
		return false;
	}
}
