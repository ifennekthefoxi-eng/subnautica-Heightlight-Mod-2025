using System;
using System.Runtime.CompilerServices;
using HighlightMod;
using UnityEngine;

[DisallowMultipleComponent]
public class ObjectHighlighterLight : MonoBehaviour
{
	// Token: 0x06000006 RID: 6 RVA: 0x000020A2 File Offset: 0x000002A2
	private void Awake()
	{
		this.CacheRenderers();
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020AC File Offset: 0x000002AC
	private void CacheRenderers()
	{
		this.renderers = base.GetComponentsInChildren<Renderer>(true);
		this.originalMaterials = new Material[this.renderers.Length][];
		for (int i = 0; i < this.renderers.Length; i++)
		{
			this.originalMaterials[i] = this.renderers[i].sharedMaterials;
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
	public void Apply(Color color, float width = 0.025f)
	{
		bool flag = !HighlightUtility.IsHighlightable(base.gameObject);
		if (!flag)
		{
			this.EnsureRimMaterial();
			HighlightOptions instance = HighlightOptions.Instance;
			float a = (instance != null) ? instance.HighlightOpacity : 1f;
			color.a = a;
			this.rimMaterial.color = color;
			foreach (Renderer renderer in this.renderers)
			{
				bool flag2 = renderer == null;
				if (!flag2)
				{
					bool flag3 = renderer.GetComponent<Light>() || renderer.GetComponent<ParticleSystem>();
					if (!flag3)
					{
						Material[] sharedMaterials = renderer.sharedMaterials;
						bool flag4 = Array.Exists<Material>(sharedMaterials, (Material m) => m == this.rimMaterial);
						if (!flag4)
						{
							Material[] array2 = new Material[sharedMaterials.Length + 1];
							sharedMaterials.CopyTo(array2, 0);
							array2[array2.Length - 1] = this.rimMaterial;
							renderer.materials = array2;
						}
					}
				}
			}
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000220C File Offset: 0x0000040C
	public void Clear()
	{
		bool flag = this.renderers == null || this.originalMaterials == null;
		if (!flag)
		{
			for (int i = 0; i < this.renderers.Length; i++)
			{
				bool flag2 = this.renderers[i] != null;
				if (flag2)
				{
					this.renderers[i].sharedMaterials = this.originalMaterials[i];
				}
			}
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002278 File Offset: 0x00000478
	private void EnsureRimMaterial()
	{
		bool flag = this.rimMaterial != null;
		if (!flag)
		{
			Shader shader = Shader.Find("Unlit/Color");
			bool flag2 = shader == null;
			if (!flag2)
			{
				this.rimMaterial = new Material(shader);
				this.rimMaterial.EnableKeyword("_RIMLIGHT");
				this.rimMaterial.SetFloat("_RimIntensity", 1.5f);
				this.rimMaterial.SetFloat("_RimPower", 3f);
				this.rimMaterial.SetFloat("_SpecIntensity", 0f);
				this.rimMaterial.SetFloat("_Glossiness", 0f);
			}
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002328 File Offset: 0x00000528
	public static void ApplyTo(GameObject go, Color color, float width = 0.025f)
	{
		bool flag = !HighlightUtility.IsHighlightable(go);
		if (!flag)
		{
			ObjectHighlighterLight objectHighlighterLight = go.GetComponent<ObjectHighlighterLight>();
			bool flag2 = !objectHighlighterLight;
			if (flag2)
			{
				objectHighlighterLight = go.AddComponent<ObjectHighlighterLight>();
			}
			objectHighlighterLight.Apply(color, width);
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000236C File Offset: 0x0000056C
	public static void ClearFrom(GameObject go)
	{
		bool flag = go == null;
		if (!flag)
		{
			ObjectHighlighterLight component = go.GetComponent<ObjectHighlighterLight>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.Clear();
			}
		}
	}

	// Token: 0x04000004 RID: 4
	private Renderer[] renderers;

	// Token: 0x04000005 RID: 5
	private Material[][] originalMaterials;

	// Token: 0x04000006 RID: 6
	private Material rimMaterial;

	// Token: 0x04000007 RID: 7
	private const float DefaultRimWidth = 0.025f;
}
