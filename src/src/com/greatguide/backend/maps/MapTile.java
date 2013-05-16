package com.greatguide.backend.maps;

import android.graphics.Bitmap;

public class MapTile
{
	// Made public for simplicity
	public int x;
	public int y;
	public Bitmap img;

	public MapTile(int x, int y, Bitmap img)
	{
		this.x = x;
		this.y = y;
		this.img = img;
	}
}