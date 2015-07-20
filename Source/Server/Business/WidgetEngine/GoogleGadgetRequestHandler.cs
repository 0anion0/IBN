﻿using System;
using System.Collections.Generic;
using System.Text;
using Mediachase.Ibn.Core.Business;
using Mediachase.Ibn.Data;

namespace Mediachase.IBN.Business.WidgetEngine
{
	/// <summary>
	/// Represents google gadget request handler.
	/// </summary>
	public class GoogleGadgetRequestHandler : BusinessObjectRequestHandler
	{
		#region Const
		#endregion

		#region Fields
		#endregion

		#region .Ctor
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomizationItemArgumentRequestHandler"/> class.
		/// </summary>
		public GoogleGadgetRequestHandler()
		{
		}
		#endregion

		#region Properties
		#endregion

		#region Methods

		#region CreateEntityObject
		/// <summary>
		/// Creates the entity object.
		/// </summary>
		/// <param name="metaClassName">Name of the meta class.</param>
		/// <param name="primaryKeyId">The primary key id.</param>
		/// <returns></returns>
		protected override EntityObject CreateEntityObject(string metaClassName, PrimaryKeyId? primaryKeyId)
		{
			if (metaClassName == GoogleGadgetEntity.ClassName)
			{
				GoogleGadgetEntity retVal = new GoogleGadgetEntity();
				retVal.PrimaryKeyId = primaryKeyId;
				return retVal;
			}

			return base.CreateEntityObject(metaClassName, primaryKeyId);
		}
		#endregion

		#region Load
		protected override void Load(BusinessContext context)
		{
			// solve problem if item was load in LocalDiskEntityObjectPlugin
			if (context.Response == null)
			{
				base.Load(context);
			}
		}
		#endregion


		#endregion
	}
}
