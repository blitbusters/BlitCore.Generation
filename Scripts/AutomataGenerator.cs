/*
 * BlitCore 2D Framework
 * 
 * Copyright (c) 2017 Håkan Edling 
 * hakan@tidyui.com
 * @tidyui
 * http://github.com/blitbusters
 * 
 */

using System.Collections;
using UnityEngine;

namespace BlitCore.Generation
{		
	public static class AutomataGenerator
	{
		/// <summary>
		/// Generates a new random map with the given parameters.
		/// </summary>
		/// <param name="width">Map width in cells</param>
		/// <param name="height">Map height in cells</param>
		/// <param name="config">Configuration</param>
		/// <returns>The map</returns>
		public static Map Generate(int width, int height, AutomataParams config = null) {
			if (config == null)
				config = new AutomataParams ();

			var map = new Map (width, height);

			// Initialize map
			for (var x = 0; x < map.Width; x++) {
				for (var y = 0; y < map.Height; y++) {
					if (Random.Range (0.0f, 1.0f) < config.InitialSpawn)
						map.Data [x, y] = CellType.Dead;
				}
			}

			for (var n = 0; n < config.Passes; n++) {
				map = Calculate (map, config);
			}
			return map;
		}

		/// <summary>
		/// Gets the number of alive neighbours.
		/// </summary>
		/// <param name="map">The map</param>
		/// <param name="x">The x pos</param>
		/// <param name="y">The y pos</param>
		/// <returns>The number of alive neighbours</returns>
		private static int CountAliveNeighbours (Map map, int x, int y) {
			var count = 0;

			for (var xOffset = -1; xOffset < 2; xOffset++) {
				for (var yOffset = - 1; yOffset < 2; yOffset++) {
					int xPos = x + xOffset;
					int yPos = y + yOffset;

					if (xOffset != 0 || yOffset != 0) {
						// Is this an edge cell
						if (xPos < 0 || yPos < 0 || xPos >= map.Width || yPos >= map.Height) {
							count++;
						}
						// Check neighbours
						else if (map.Data [xPos, yPos] == CellType.Dead) {
							count++;
						}
					}
				}
			}
			return count;
		}

		/// <summary>
		/// Performs a map calculation pass.
		/// </summary>
		/// <param name="oldMap">The old map</param>
		/// <param name="config">The confi</param>
		/// <returns>The processed map</returns>
		private static Map Calculate(Map oldMap, AutomataParams config){
			var data = new CellType [oldMap.Width, oldMap.Height];

			for(int x = 0; x < oldMap.Width; x++){
				for(int y = 0; y < oldMap.Height; y++){
					int alive = CountAliveNeighbours(oldMap, x, y);

					if (oldMap.Data [x, y] == CellType.Dead) {
						if (alive < config.DeathLimit){
							data [x, y] = CellType.Open;
						} else {
							data [x, y] = CellType.Dead;
						}
					} else {
						if (alive > config.BirthLimit) {
							data [x, y] = CellType.Dead;
						} else {
							data [x, y] = CellType.Open;
						}
					}
				}
			}
			oldMap.Data = data;
			return oldMap;
		}
	}
}
