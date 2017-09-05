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
	/// The different parameters that can be passed to the
	/// automata generator.
	/// </summary>
	public class AutomataParams
	{
		/// <summary>
		/// Gets/sets how many passes should be run be the generator.
		/// </summary>
		public int Passes { get; set; }

		/// <summary>
		/// Gets/sets the birth threshold.
		/// </summary>
		public int BirthLimit { get; set; }

		/// <summary>
		/// Gets/sets the death threshold.
		/// </summary>
		public int DeathLimit { get; set; }

		/// <summary>
		/// Gets/sets the chance for spawning a dead cell.
		/// </summary>
		public float InitialSpawn { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public AutomataParams() {
			Passes = 3;
			BirthLimit = 4;
			DeathLimit = 3;
			InitialSpawn = 0.45f;
		}
	}
}
