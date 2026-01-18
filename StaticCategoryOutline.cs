using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class StaticCategoryOutline : MonoBehaviour
{
	// Token: 0x06000013 RID: 19 RVA: 0x00002504 File Offset: 0x00000704
	public void Apply(Color color)
	{
		bool flag = !this.highlighter;
		if (flag)
		{
			this.highlighter = (base.gameObject.GetComponent<ObjectHighlighterLight>() ?? base.gameObject.AddComponent<ObjectHighlighterLight>());
		}
		this.highlighter.Apply(color, 0.025f);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002558 File Offset: 0x00000758
	public void Disable()
	{
		bool flag = this.highlighter;
		if (flag)
		{
			this.highlighter.Clear();
		}
	}
	private ObjectHighlighterLight highlighter;
}
