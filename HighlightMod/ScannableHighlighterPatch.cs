using System;
using System.Collections;
using System.Collections.Generic; // ← ADD THIS
using HarmonyLib;
using HeightlightMod;
using UnityEngine;
using UWE;
using Nautilus.Utility;
using System.Linq;


namespace HighlightMod
{
	[HarmonyPatch]
	public static class ScannableHighlighterPatch
	{
		private static readonly HashSet<GameObject> HighlightedObjects = new HashSet<GameObject>();
		private static readonly HashSet<GameObject> SeenObjects = new HashSet<GameObject>();
		
		// Token: 0x06000018 RID: 24 RVA: 0x00002724 File Offset: 0x00000924
		public static Color? GetColorForCategory(HighlightCategory category)
		{
			
			HighlightOptions instance = HighlightOptions.Instance;
			bool flag = instance == null;
			Color? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				if (!true)
				{
				}
				Color? color;
				switch (category)
				{
				case HighlightCategory.Fish:
					color = (instance.HighlightFish ? new Color?(instance.FishColor) : null);
					break;
				case HighlightCategory.Scannable:
					color = (instance.HighlightScannable ? new Color?(instance.ScannableColor) : null);
					break;
				case HighlightCategory.Pickupable:
					color = (instance.HighlightPickupable ? new Color?(instance.PickupableColor) : null);
					break;
				case HighlightCategory.PDA:
					color = (instance.HighlightPDA ? new Color?(instance.PDAColor) : null);
					break;
				default:
					color = null;
					break;
				}
				if (!true)
				{
				}
				Color? color2 = color;
				bool flag2 = color2 != null;
				if (flag2)
				{
					Color value = color2.Value;
					value.a = instance.HighlightOpacity;
					result = new Color?(value);
				}
				else
				{
					result = color2;
				}
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002844 File Offset: 0x00000A44
		public static HighlightCategory GetCategory(GameObject go)
		{
			bool flag = go == null;
			HighlightCategory result;
			if (flag)
			{
				result = HighlightCategory.None;
			}
			else
			{
				ValueTuple<Func<GameObject, bool>, HighlightCategory>[] array = new ValueTuple<Func<GameObject, bool>, HighlightCategory>[4];
				array[0] = new ValueTuple<Func<GameObject, bool>, HighlightCategory>((GameObject g) => g.GetComponent<TechFragment>() != null || g.name.ToLower().Contains("fragment"), HighlightCategory.Scannable);
				array[1] = new ValueTuple<Func<GameObject, bool>, HighlightCategory>((GameObject g) => g.GetComponent<StoryHandTarget>() != null || g.name.ToLower().Contains("pda"), HighlightCategory.PDA);
				array[2] = new ValueTuple<Func<GameObject, bool>, HighlightCategory>((GameObject g) => g.GetComponent<Creature>() != null && !g.name.ToLower().Contains("fishschool"), HighlightCategory.Fish);
				array[3] = new ValueTuple<Func<GameObject, bool>, HighlightCategory>((GameObject g) => g.GetComponent<Pickupable>() != null && g.GetComponent<TechFragment>() == null && !g.name.ToLower().Contains("fragment") && !g.name.ToLower().Contains("fishschool") && g.GetComponent<StoryHandTarget>() == null && g.GetComponent<Creature>() == null && !HighlightUtility.IsToolOrPlaceable(g), HighlightCategory.Pickupable);
				ValueTuple<Func<GameObject, bool>, HighlightCategory>[] array2 = array;
				foreach (ValueTuple<Func<GameObject, bool>, HighlightCategory> valueTuple in array2)
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

		// Token: 0x0600001A RID: 26 RVA: 0x00002964 File Offset: 0x00000B64
		/*private static void ApplyHighlight(GameObject go)
		{
			if (go == null || !HighlightUtility.IsHighlightable(go))
				return;

			HighlightCategory category = GetCategory(go);
			if (category == HighlightCategory.None)
				return;

			Color? color = GetColorForCategory(category);
			if (color != null)
			{
				ObjectHighlighterLight.ApplyTo(go, color.Value, 0.035f);
				HighlightedObjects.Add(go);
			}
		}*/

		private static void ApplyHighlight(GameObject go)
		{
			if (go == null || !HighlightUtility.IsHighlightable(go))
				return;

			HighlightCategory category = GetCategory(go);

			// ❗ If category disabled or invalid → CLEAR
			if (category == HighlightCategory.None)
			{
				ClearHighlight(go);
				return;
			}

			Color? color = GetColorForCategory(category);

			// ❗ Toggle OFF → CLEAR
			if (color == null)
			{
				ClearHighlight(go);
				return;
			}

			ObjectHighlighterLight.ApplyTo(go, color.Value, OutlineWidth);
			HighlightedObjects.Add(go);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000029CA File Offset: 0x00000BCA
		private static IEnumerator DelayedApply(GameObject go)
		{
			yield return null;
			ScannableHighlighterPatch.ApplyHighlight(go);
			yield break;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000029DC File Offset: 0x00000BDC
		[HarmonyPatch(typeof(Pickupable), "Awake")]
		[HarmonyPostfix]
		private static void Pickupable_Awake(Pickupable __instance)
		{
			bool flag = __instance == null;
			if (!flag)
			{
				ScannableHighlighterPatch.ApplyHighlight(__instance.gameObject);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A04 File Offset: 0x00000C04
		[HarmonyPatch(typeof(TechFragment), "Start")]
		[HarmonyPostfix]
		private static void TechFragment_Start(TechFragment __instance)
		{
			bool flag = __instance == null;
			if (!flag)
			{
				ScannableHighlighterPatch.ApplyHighlight(__instance.gameObject);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A2C File Offset: 0x00000C2C
		[HarmonyPatch(typeof(Creature), "Start")]
		[HarmonyPostfix]
		private static void Creature_Start(Creature __instance)
		{
			bool flag = __instance == null;
			if (!flag)
			{
				UWE.CoroutineHost.StartCoroutine(ScannableHighlighterPatch.DelayedApply(__instance.gameObject));
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A58 File Offset: 0x00000C58
		[HarmonyPatch(typeof(StoryHandTarget), "OnHandHover")]
		[HarmonyPostfix]
		private static void StoryHandTarget_Hover(StoryHandTarget __instance, GUIHand hand)
		{
			bool flag = __instance == null;
			if (!flag)
			{
				ScannableHighlighterPatch.ApplyHighlight(__instance.gameObject);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A80 File Offset: 0x00000C80
		public static void ClearHighlight(GameObject go)
		{
			if (go == null) return;
			ObjectHighlighterLight.ClearFrom(go);
			HighlightedObjects.Remove(go);
		}

		public static void RefreshAllHighlights()
		{
			// Manual copy to avoid ISet<> and LINQ
			var temp = new List<GameObject>();
			foreach (var go in HighlightedObjects)
			{
				temp.Add(go);
			}

			foreach (var go in temp)
			{
				if (go == null)
				{
					HighlightedObjects.Remove(go);
					continue;
				}

				ObjectHighlighterLight.ClearFrom(go);
				ApplyHighlight(go);
			}
		}

		// Token: 0x0400000C RID: 12
		private const float OutlineWidth = 0.035f;
	}
}
