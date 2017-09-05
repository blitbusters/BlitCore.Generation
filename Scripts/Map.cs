/*
 * BlitCore 2D Framework
 * 
 * Copyright (c) 2017 Håkan Edling 
 * hakan@tidyui.com
 * @tidyui
 * http://github.com/blitbusters
 * 
 */

namespace BlitCore.Generation
{
	/// <summary>
	/// Structure for a generated map.
	/// </summary>
	public class Map
	{
		/// <summary>
		/// Gets the map width in tiles.
		/// </summary>
		public int Width { get; private set; }

		/// <summary>
		/// Gets the map height in tiles.
		/// </summary>
		public int Height { get; private set; }

		/// <summary>
		/// Gets the available map cells.
		/// </summary>
		public CellType[,] Data { get; internal set; }

		/// <summary>
		/// Default constructor. Creates a new map.
		/// </summary>
		/// <param name="width">The width in tiles</param>
		/// <param name="height">The height in tiles</param>
		public Map(int width, int height) {
			Width = width;
			Height = height;
			Data = new CellType[width, height];

			Clear ();
		}

		/// <summary>
		/// Clears the current map data.
		/// </summary>
		public void Clear() {
			for (var x = 0; x < Width; x++) {
				for (var y = 0; y < Height; y++) {
					Data [x, y] = CellType.Open;
				}
			}
		}

		/// <summary>
		/// Calculates the wall type given the surrounding cells.
		/// </summary>
		public void CalculateWalls() {
			for (var x = 0; x < Width; x++) {
				for (var y = 0; y < Height; y++) {
					if (Data [x, y] == CellType.Dead) {
						if (y > 0 && Data [x, y - 1] == CellType.Open) {
							Data [x, y] = CellType.WallBottom;
						} else if (y < Height - 1 && Data [x, y + 1] == CellType.Open) {
							if (x > 0 && Data [x - 1, y] == CellType.Open) {
								Data [x, y] = CellType.WallTopCornerRight;
							} else if (x < Width - 1 && Data [x + 1, y] == CellType.Open) {
								Data [x, y] = CellType.WallTopCornerRight;
							} else {
								Data [x, y] = CellType.WallTop;
							}
						} else if (x > 0 && Data [x - 1, y] == CellType.Open) {
							Data [x, y] = CellType.WallRight;
						} else if (x < Width - 1 && Data [x + 1, y] == CellType.Open) {
							Data [x, y] = CellType.WallLeft;
						} 
					}
				}
			}

			for (var x = 0; x < Width; x++) {
				for (var y = 0; y < Height; y++) {
					if (Data [x, y] == CellType.Dead) {
						if (x > 0 && Data [x - 1, y] == CellType.WallTop) {
						} else if (x < Width - 1 && Data [x + 1, y] == CellType.WallTop) {
						}
					}
				}
			}
		}
	}
}
