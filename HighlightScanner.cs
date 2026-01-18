using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HeightlightMod;
using HighlightMod;
using UnityEngine;

// Token: 0x02000007 RID: 7
public static class HighlightScanner
{
	public static HighlightCategory GetHighlightCategory(GameObject go)
	{
		bool flag = go == null;
		HighlightCategory result;
		if (flag)
		{
			result = HighlightCategory.None;
		}
		else
		{
			foreach (ValueTuple<Func<GameObject, bool>, HighlightCategory> valueTuple in HighlightScanner.Rules)
			{
				Func<GameObject, bool> item = valueTuple.Item1;
				HighlightCategory item2 = valueTuple.Item2;
				bool flag2 = item(go);
				if (flag2)
				{
					return item2;
				}
			}
			result = HighlightCategory.None;
		}
		return result;
	}
	private static readonly List<ValueTuple<Func<GameObject, bool>, HighlightCategory>> Rules = new List<ValueTuple<Func<GameObject, bool>, HighlightCategory>>
	{
		new ValueTuple<Func<GameObject, bool>, HighlightCategory>((GameObject g) => g.GetComponent<TechFragment>() != null || g.name.ToLower().Contains("fragment"), HighlightCategory.Scannable),
		new ValueTuple<Func<GameObject, bool>, HighlightCategory>((GameObject g) => g.GetComponent<StoryHandTarget>() != null || g.name.ToLower().Contains("pda"), HighlightCategory.PDA),
		new ValueTuple<Func<GameObject, bool>, HighlightCategory>((GameObject g) => g.GetComponent<Creature>() != null && !g.name.ToLower().Contains("fishschool"), HighlightCategory.Fish),
		new ValueTuple<Func<GameObject, bool>, HighlightCategory>((GameObject g) => g.GetComponent<Pickupable>() != null && g.GetComponent<TechFragment>() == null && !g.name.ToLower().Contains("fragment") && !g.name.ToLower().Contains("fishschool") && g.name.ToLower().Contains("pda") && g.GetComponent<StoryHandTarget>() == null && g.GetComponent<Creature>() == null && !HighlightUtility.IsToolOrPlaceable(g), HighlightCategory.Pickupable)
	};
}
