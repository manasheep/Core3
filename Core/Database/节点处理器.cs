// ===============================================================================
// Microsoft Data Access Application Block for .NET 3.0
//
// DaabSectionHandler.cs
//
// This file contains the implementations of the �ڵ㴦���� 
// configuration section handler.
//
// For more information see the Documentation. 
// ===============================================================================
// Release history
// VERSION	DESCRIPTION
//   2.0	Added support for ������ݼ�, �������ݼ� and "Param" helper methods
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
	public class �ڵ㴦���� : 
		IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler��Ա
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

				ht[ prov.Attributes[ "alias" ].Value ] = new ��Ӧ�߱���( prov.Attributes[ "assembly" ].Value, prov.Attributes[ "type" ].Value );
			}
			return ht;
		}

		#endregion
	}

	/// <summary>
	/// This class is for reading the '��Ӧ�߱���' tag from the 'daabProviders' section of the App.Config file
	/// </summary>
	public class ��Ӧ�߱���
	{
		#region ��Ա����
		string _assemblyName;
		string _typeName;
		#endregion
		
		#region ������
		/// <summary>
		/// Constructor required by IConfigurationSectionHandler
		/// </summary>
		/// <param name="��������">The Assembly where this provider can be found</param>
		/// <param name="��������">The type of the provider</param>
		public ��Ӧ�߱���( string ��������, string �������� )
		{
			_assemblyName = ��������;
			_typeName = ��������;
		}
		#endregion

		#region ����
		/// <summary>
		/// Returns the Assembly name for this provider
		/// </summary>
		/// <ֵ>The Assembly name for the specified provider</ֵ>
		public string ��������
		{
			get{ return _assemblyName; }
		}

		/// <summary>
		/// Returns the type name of this provider
		/// </summary>
		/// <ֵ>The type name of the specified provider</ֵ>
		public string ��������
		{
			get{ return _typeName; }
		}
		#endregion
	}
}