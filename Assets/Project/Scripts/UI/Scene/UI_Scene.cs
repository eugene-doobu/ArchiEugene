﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArchiEugene.UI
{
	public class UI_Scene : UI_Base
	{
		public override void Init()
		{
			Managers.UI.SetCanvas(gameObject, false);
		}
	}
}

