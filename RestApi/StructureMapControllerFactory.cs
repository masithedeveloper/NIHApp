﻿using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace NIHApp.RestApi
{
	public class StructureMapControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				return base.GetControllerInstance(requestContext, null);
			}
			return (IController)ObjectFactory.GetInstance(controllerType);
		}
	}
}