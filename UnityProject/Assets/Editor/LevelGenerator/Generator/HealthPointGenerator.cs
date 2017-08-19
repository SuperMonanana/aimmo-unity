﻿using UnityEditor;
using UnityEngine;
using MapFeatures;

namespace GeneratorNS {
	public class HealthPointGenerator : SpriteGenerator
	{
		public HealthPointGenerator (float x, float y) : base (x, y, @"""sprite"": {}") 
		{
		}

		public override IMapFeatureManager GetManager ()
		{
			return new GameObject ().AddComponent<HealthPointManager> ();
		}
	}
}
