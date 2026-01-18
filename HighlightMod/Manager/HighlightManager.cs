using System;
using HeightlightMod;
using UnityEngine;

namespace HighlightMod.Manager
{
	// Token: 0x0200000E RID: 14
	public class HighlightManager : MonoBehaviour
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00003118 File Offset: 0x00001318
		private void Start()
		{
			base.InvokeRepeating("Scan", 1f, 2f);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003134 File Offset: 0x00001334
		private void Scan()
		{
			bool flag = Player.main == null;
			if (!flag)
			{
				Collider[] array = Physics.OverlapSphere(
				Player.main.transform.position,
				45f,
				-1,
				QueryTriggerInteraction.Collide
				);
				foreach (Collider collider in array)
				{
					GameObject gameObject = collider.gameObject;
					bool flag2 = gameObject == null;
					if (!flag2)
					{
						bool flag3 = gameObject.transform.parent != null && gameObject.transform.parent.GetComponent<Inventory>() != null;
						if (!flag3)
						{
							bool flag4 = Inventory.main != null;
							if (flag4)
							{
								PlayerTool heldTool = Inventory.main.GetHeldTool();
								bool flag5 = heldTool != null && (gameObject == heldTool || gameObject.transform.IsChildOf(heldTool.transform));
								if (flag5)
								{
									goto IL_20C;
								}
							}
							string[] array3 = new string[]
							{
								"Seaglide",
								"Flashlight",
								"PropulsionCannon",
								"RepairTool",
								"Scanner",
								"LaserCutter",
								"StasisRifle",
								"Builder",
								"PDA",
								"Seamoth",
								"Exosuit",
								"Seatruck",
								"Effect",
								"Light",
								"Beam",
								"Map"
							};
							bool flag6 = false;
							foreach (string value in array3)
							{
								bool flag7 = gameObject.name.Contains(value);
								if (flag7)
								{
									flag6 = true;
									break;
								}
							}
							bool flag8 = flag6;
							if (!flag8)
							{
								HighlightCategory highlightCategory = HighlightScanner.GetHighlightCategory(gameObject);
								bool flag9 = highlightCategory == HighlightCategory.None;
								if (!flag9)
								{
									Color? colorForCategory = ScannableHighlighterPatch.GetColorForCategory(highlightCategory);
									bool flag10 = colorForCategory == null;
									if (!flag10)
									{
										ObjectHighlighterLight.ApplyTo(gameObject, colorForCategory.Value, 0.025f);
									}
								}
							}
						}
					}
					IL_20C:;
				}
			}
		}

		// Token: 0x04000019 RID: 25
		private const float ScanRadius = 45f;

		// Token: 0x0400001A RID: 26
		private const float ScanInterval = 2f;

		// Token: 0x0400001B RID: 27
		private const float OutlineWidth = 0.025f;
	}
}
