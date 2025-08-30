using Godot;
using System;

using System.Collections.Generic;

public class MapManager
{
	private Dictionary<int, String> geographyImgPath = new Dictionary<int, String>();
	
	public MapManager() {
		
	}
	
	public Cell get_cell(float x, float y)
	{
		Cell n = new Cell();
		n.position = new Vector2(448f,192f);
		return n;
	}
	
	public String get_geography_img_path(int courseIdx)
	{
		return geographyImgPath[courseIdx];
	}
}
