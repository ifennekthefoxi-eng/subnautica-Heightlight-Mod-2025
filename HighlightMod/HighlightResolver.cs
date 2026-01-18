using System;
using System.Runtime.CompilerServices;
using HeightlightMod;
using UnityEngine;

namespace HighlightMod
{
	// Token: 0x0200000A RID: 10
	public static class HighlightResolver
	{
		private static HighlightCategory GetCategory(GameObject go)
		{
			bool flag = go == null;
			HighlightCategory result;
			if (flag)
			{
				result = HighlightCategory.None;
			}
			else
			{
				bool flag2 = go.GetComponent<Creature>() != null;
				if (flag2)
				{
					result = HighlightCategory.Fish;
				}
				else
				{
					bool flag3 = go.GetComponent<TechFragment>() != null;
					if (flag3)
					{
						result = HighlightCategory.Scannable;
					}
					else
					{
						bool flag4 = go.GetComponent<StoryHandTarget>() != null;
						if (flag4)
						{
							result = HighlightCategory.PDA;
						}
						else
						{
							bool flag5 = go.GetComponent<Pickupable>() != null;
							if (flag5)
							{
								result = HighlightCategory.Pickupable;
							}
							else
							{
								result = HighlightCategory.None;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002604 File Offset: 0x00000804
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
	}
}
