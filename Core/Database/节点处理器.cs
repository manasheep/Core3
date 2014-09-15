// ===============================================================================
// Microsoft Data Access Application Block for .NET 3.0
//
// DaabSectionHandler.cs
//
// This file contains the implementations of the 节点处理器 
// configuration section handler.
//
// For more information see the Documentation. 
// ===============================================================================
// Release history
// VERSION	DESCRIPTION
//   2.0	Added support for 填充数据集, 更新数据集 and "Param" helper methods
//   3.0	New abstract class supporting the same methods using ADO.NET interfaces
//
// ===============================================================================
// Copyright (C) 2000-2001 Microsoft Corporation
// All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
// FITNESS FOR A PARTICULAR PURPOSE.
// ==============================================================================

using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace Core.Database
{
	/// <summary>
	/// This class is for reading the 'daabProvider' section of the App.Config file
	/// </summary>
	public class 节点处理器 : 
		IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler成员
        /// <summary>
		/// Evaluates the given XML section and returns a Hashtable that contains the results of the evaluation.
		/// </summary>
		/// <param name="parent">The configuration settings in a corresponding parent configuration section. </param>
		/// <param name="configContext">An HttpConfigurationContext when Create is called from the ASP.NET configuration system. Otherwise, this parameter is reserved and is a null reference (Nothing in Visual Basic). </param>
		/// <param name="section">The XmlNode that contains the configuration information to be handled. Provides direct access to the XML contents of the configuration section. </param>
		/// <returns>A Hashtable that contains the section's configuration settings.</returns>
		public object Create(object parent, object configContext, XmlNode section)
		{
			Hashtable ht = new Hashtable();
            XmlNodeList list = section.SelectNodes("DataProvider");
			foreach( XmlNode prov in list )
			{
				if( prov.Attributes[ "alias" ] == null )
					throw new InvalidOperationException( "The 'daabProvider' node must contain an attribute named 'alias' with the alias name for the provider." );
				if( prov.Attributes[ "assembly" ] == null )
					throw new InvalidOperationException( "The 'daabProvider' node must contain an attribute named 'assembly' with the name of the assembly containing the provider." );
				if( prov.Attributes[ "type" ] == null )
					throw new InvalidOperationException( "The 'daabProvider' node must contain an attribute named 'type' with the full name of the type for the provider." );

				ht[ prov.Attributes[ "alias" ].Value ] = new 供应者别名( prov.Attributes[ "assembly" ].Value, prov.Attributes[ "type" ].Value );
			}
			return ht;
		}

		#endregion
	}

	/// <summary>
	/// This class is for reading the '供应者别名' tag from the 'daabProviders' section of the App.Config file
	/// </summary>
	public class 供应者别名
	{
		#region 成员变量
		string _assemblyName;
		string _typeName;
		#endregion
		
		#region 构造器
		/// <summary>
		/// Constructor required by IConfigurationSectionHandler
		/// </summary>
		/// <param name="程序集名称">The Assembly where this provider can be found</param>
		/// <param name="类型名称">The type of the provider</param>
		public 供应者别名( string 程序集名称, string 类型名称 )
		{
			_assemblyName = 程序集名称;
			_typeName = 类型名称;
		}
		#endregion

		#region 属性
		/// <summary>
		/// Returns the Assembly name for this provider
		/// </summary>
		/// <值>The Assembly name for the specified provider</值>
		public string 程序集名称
		{
			get{ return _assemblyName; }
		}

		/// <summary>
		/// Returns the type name of this provider
		/// </summary>
		/// <值>The type name of the specified provider</值>
		public string 类型名称
		{
			get{ return _typeName; }
		}
		#endregion
	}
}