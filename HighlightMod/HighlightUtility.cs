using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace HighlightMod
{
	public static class HighlightUtility
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002DEC File Offset: 0x00000FEC
		public static bool IsHighlightable(GameObject go)
		{
			bool flag = go == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Pickupable component = go.GetComponent<Pickupable>();
				bool flag2 = component != null && HighlightUtility.IsInInventory(go);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = Player.main != null && Inventory.main != null;
					if (flag3)
					{
						PlayerTool heldTool = Inventory.main.GetHeldTool();
						bool flag4 = heldTool != null && (go == heldTool || go.transform.IsChildOf(heldTool.transform));
						if (flag4)
						{
							return false;
						}
					}
					foreach (string value in HighlightUtility.SkipChildNames)
					{
						bool flag5 = go.name.Contains(value);
						if (flag5)
						{
							return false;
						}
					}
					bool flag6 = go.GetComponent<Vehicle>() != null;
					if (flag6)
					{
						result = false;
					}
					else
					{
						bool flag7 = go.GetComponent<StorageContainer>() != null;
						if (flag7)
						{
							result = false;
						}
						else
						{
							bool flag8 = HighlightUtility.IsToolOrPlaceable(go);
							result = !flag8;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002F10 File Offset: 0x00001110
		public static bool IsToolOrPlaceable(GameObject go)
		{
			bool flag = go == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Transform transform = go.transform;
				while (transform != null)
				{
					foreach (string value in HighlightUtility.ToolNames)
					{
						bool flag2 = transform.name.Contains(value);
						if (flag2)
						{
							return true;
						}
					}
					bool flag3 = transform.GetComponent<PlaceTool>() != null;
					if (flag3)
					{
						return true;
					}
					transform = transform.parent;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002FA0 File Offset: 0x000011A0
		public static bool IsInInventory(GameObject go)
		{
			bool flag = go == null || Inventory.main == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Pickupable component = go.GetComponent<Pickupable>();
				bool flag2 = component == null;
				result = (!flag2 && Inventory.main.Contains(component));
			}
			return result;
		}

		// Token: 0x04000017 RID: 23
		private static readonly string[] ToolNames = new string[]
		{
			"SeaGlide",
			"Flashlight",
			"PropulsionCannon",
			"RepairTool",
			"Scanner",
			"Welder",
			"Builder",
			"SmallStorage",
			"Beacon",
			"Gravsphere",
			"Flare",
			"PlagueKnife",
			"InfectionSampler",
			"InfectionTracker",
			"AirStrikeDevice",
			"TerraformerBuilder",
			"Terraformer",
			"AirBladder",
			"ledlight",
			"PathfinderTool",
			"RepulsionCannon",
			"Knife",
			"HeatBlade",
			"AlienRifleWeapon",
			"StasisRifle"
		};

		// Token: 0x04000018 RID: 24
		private static readonly string[] SkipChildNames = new string[]
		{
			"MapCircle",
			"Light",
			"Glow",
			"Beam",
			"Effect"
		};
	}
}
