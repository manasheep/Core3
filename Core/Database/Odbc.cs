// ===============================================================================
// Microsoft Data Access Application Block for .NET 3.0
//
// Odbc.cs
//
// This file contains the implementations of the AdoHelper supporting Odbc.
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
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;

namespace Core.Database
{
	/// <summary>
	/// The Odbc class is intended to encapsulate high performance, scalable best practices for 
	/// common uses of the Odbc ADO.NET provider.  It is created using the abstract factory in AdoHelper
	/// </summary>
	public class Odbc : 数据库访问器
	{
		// used for correcting Call syntax for stored procedures in ODBC
		static Regex _regExpr = new Regex( @"\{.*call|CALL\s\w+.*}", RegexOptions.Compiled );
		
		/// <summary>
		/// Create an Odbc Helper.  Needs to be a default constructor so that the Factory can create it
		/// </summary>
		public Odbc()
		{
		}

		#region 重写
		/// <summary>
		/// Returns an array of OdbcParameters of the specified 大小
		/// </summary>
		/// <param name="大小">大小 of the array</param>
		/// <returns>The array of OdbcParameters</returns>
		protected override IDataParameter[] 获得数据参数(int 大小)
		{
			return new OdbcParameter[大小];
		}

		/// <summary>
		/// Returns an OdbcConnection object for the given 连接 string
		/// </summary>
		/// <param name="连接字符串">The 连接 string to be used to create the 连接</param>
		/// <returns>An OdbcConnection object</returns>
		public override IDbConnection 获取连接( string 连接字符串 )
		{
			return new OdbcConnection( 连接字符串 );
		}

		/// <summary>
		/// Returns an OdbcDataAdapter object
		/// </summary>
		/// <returns>The OdbcDataAdapter</returns>
		public override IDbDataAdapter 获取数据适配器()
		{
			return new OdbcDataAdapter(); 
		}

		/// <summary>
		/// Calls the CommandBuilder.DeriveParameters method for the specified provider, doing any setup and cleanup necessary
		/// </summary>
		/// <param name="命令">The IDbCommand referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the Parameters collection of the IDbCommand. </param>
		public override void 获得参数( IDbCommand 命令 )
		{
			bool mustCloseConnection = false;

			if( !( 命令 is OdbcCommand ) )
				throw new ArgumentException( "The 命令 provided is not a OdbcCommand instance.", "命令" );
			if (命令.Connection.State != ConnectionState.Open) 
			{
				命令.Connection.Open();
				mustCloseConnection = true;
			}

			OdbcCommandBuilder.DeriveParameters( (OdbcCommand)命令 );

			if (mustCloseConnection)
			{
				命令.Connection.Close();
			}
		}

		/// <summary>
		/// Returns an OdbcParameter object
		/// </summary>
		/// <returns>The OdbcParameter object</returns>
		public override IDataParameter 获取参数()
		{
			return new OdbcParameter(); 
		}
		
		/// <summary>
		/// This cleans up the parameter syntax for an ODBC call.  This was split out from PrepareCommand so that it could be called independently.
		/// </summary>
		/// <param name="命令">An IDbCommand object containing the CommandText to clean.</param>
		public override void 清除参数规则(IDbCommand 命令)
		{
			string call = " call ";

			if( 命令.CommandType == CommandType.StoredProcedure )
			{
				if( !_regExpr.Match( 命令.CommandText ).Success && // It does not like like { call sp_name() }
					命令.CommandText.Trim().IndexOf( " " ) == -1 ) // If there's only a stored procedure name
				{
					// If there's only a stored procedure name
                    StringBuilder par = new StringBuilder();
					if( 命令.Parameters.Count != 0 )
					{
						bool isFirst = true;
						bool hasParameters = false;

						for( int i = 0; i < 命令.Parameters.Count; i++ )
						{
							OdbcParameter p = 命令.Parameters[i] as OdbcParameter;
							if( p.Direction != ParameterDirection.ReturnValue )
							{
								if( isFirst )
								{
									isFirst = false;
									par.Append( "(?" );
								}
								else
								{
									par.Append( ",?" );
								}
								hasParameters = true;
							}
							else
							{
								call = " ? = call ";
							}
						}
						if (hasParameters)
						{
							par.Append( ")" );
						}
					}
					命令.CommandText = "{" + call + 命令.CommandText + par.ToString() + " }";
				}
			}
		}

		/// <summary>
		/// Execute an IDbCommand (that returns a resultset) against the provided IDbConnection. 
		/// </summary>
		/// <example>
		/// <code>
		/// XmlReader r = helper.获得XML读取器(命令);
		/// </code></example>
		/// <param name="命令">The IDbCommand to execute</param>
		/// <returns>An XmlReader containing the resultset generated by the 命令</returns>
		public override XmlReader 获得XML读取器(IDbCommand 命令)
		{
			bool mustCloseConnection = false;

			if (命令.Connection.State != ConnectionState.Open) 
			{
				命令.Connection.Open();
				mustCloseConnection = true;
			}

			清除参数规则(命令);

			OdbcDataAdapter da = new OdbcDataAdapter((OdbcCommand)命令);
			DataSet ds = new DataSet();

			da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
			da.Fill(ds);

			StringReader stream = new StringReader(ds.GetXml());
			
			if (mustCloseConnection)
			{
				命令.Connection.Close();
			}

			return new XmlTextReader(stream);
		}

		/// <summary>
		/// Provider specific code to set up the updating/ed event handlers used by 更新数据集
		/// </summary>
		/// <param name="数据适配器">DataAdapter to attach the event handlers to</param>
		/// <param name="更新行处理器">The handler to be called when a row is updating</param>
		/// <param name="更新行完毕处理器">The handler to be called when a row is updated</param>
		protected override void 添加更新事件处理器(IDbDataAdapter 数据适配器, 更新行处理器 更新行处理器, 更新行完毕处理器 更新行完毕处理器)
		{
			if (更新行处理器 != null)
			{
				this.m_rowUpdating = 更新行处理器;
				((OdbcDataAdapter)数据适配器).RowUpdating +=  new OdbcRowUpdatingEventHandler(RowUpdating);
			}

			if (更新行完毕处理器 != null)
			{
				this.m_rowUpdated = 更新行完毕处理器;
				((OdbcDataAdapter)数据适配器).RowUpdated +=  new OdbcRowUpdatedEventHandler(RowUpdated);
			}
		}

		/// <summary>
		/// Handles the 更新行 event
		/// </summary>
		/// <param name="obj">The object that published the event</param>
		/// <param name="e">The OdbcRowUpdatingEventArgs</param>
		protected void RowUpdating(object obj, OdbcRowUpdatingEventArgs e)
		{
			base.更新行(obj, e);
		}

		/// <summary>
		/// Handles the 更新行完毕 event
		/// </summary>
		/// <param name="obj">The object that published the event</param>
		/// <param name="e">The OdbcRowUpdatedEventArgs</param>
		protected void RowUpdated(object obj, OdbcRowUpdatedEventArgs e)
		{
			base.更新行完毕(obj, e);
		}
		
		/// <summary>
		/// Handle any provider-specific issues with BLOBs here by "washing" the IDataParameter and returning a new one that is set up appropriately for the provider.
		/// </summary>
		/// <param name="连接">The IDbConnection to use in cleansing the parameter</param>
        /// <param name="参数">The parameter before cleansing</param>
		/// <returns>The parameter after it's been cleansed.</returns>
		protected override IDataParameter 获取冒泡参数(IDbConnection 连接, IDataParameter 参数)
		{
			// nothing special needed for ODBC...so far as we know now.
			return 参数;
		}
		#endregion
	}
}