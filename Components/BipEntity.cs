/* 
*  Copyright (c) 2002 FulcrumWeb. All rights reserved.
*/

using System;

namespace Bip.Components
{
	public abstract class BipEntity : IDisposable
	{
		protected Database Db = new Database();
		
		public BipEntity(){}
		public void Dispose() 
		{
			Db.Dispose();
		}

		public abstract void New();
		public abstract int  Create();
		public abstract void Load(int id);
		public abstract void Update();
		public abstract void Delete();
	}
}
