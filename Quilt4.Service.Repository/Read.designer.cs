﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quilt4.Service.SqlRepository
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Quilt4")]
	public partial class ReadDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertIssueRead(IssueRead instance);
    partial void UpdateIssueRead(IssueRead instance);
    partial void DeleteIssueRead(IssueRead instance);
    #endregion
		
		public ReadDataContext() : 
				base(global::Quilt4.Service.SqlRepository.Properties.Settings.Default.Quilt4ConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public ReadDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ReadDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ReadDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public ReadDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<IssueRead> IssueReads
		{
			get
			{
				return this.GetTable<IssueRead>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="Query.IssueRead")]
	public partial class IssueRead : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IssueId;
		
		private System.Guid _IssueKey;
		
		private string _Level;
		
		private int _Ticket;
		
		private string _Message;
		
		private string _StackTrace;
		
		private string _Type;
		
		private string _ProjectName;
		
		private string _ApplicationName;
		
		private string _VersionNumber;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIssueIdChanging(int value);
    partial void OnIssueIdChanged();
    partial void OnIssueKeyChanging(System.Guid value);
    partial void OnIssueKeyChanged();
    partial void OnLevelChanging(string value);
    partial void OnLevelChanged();
    partial void OnTicketChanging(int value);
    partial void OnTicketChanged();
    partial void OnMessageChanging(string value);
    partial void OnMessageChanged();
    partial void OnStackTraceChanging(string value);
    partial void OnStackTraceChanged();
    partial void OnTypeChanging(string value);
    partial void OnTypeChanged();
    partial void OnProjectNameChanging(string value);
    partial void OnProjectNameChanged();
    partial void OnApplicationNameChanging(string value);
    partial void OnApplicationNameChanged();
    partial void OnVersionNumberChanging(string value);
    partial void OnVersionNumberChanged();
    #endregion
		
		public IssueRead()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IssueId", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IssueId
		{
			get
			{
				return this._IssueId;
			}
			set
			{
				if ((this._IssueId != value))
				{
					this.OnIssueIdChanging(value);
					this.SendPropertyChanging();
					this._IssueId = value;
					this.SendPropertyChanged("IssueId");
					this.OnIssueIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IssueKey", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid IssueKey
		{
			get
			{
				return this._IssueKey;
			}
			set
			{
				if ((this._IssueKey != value))
				{
					this.OnIssueKeyChanging(value);
					this.SendPropertyChanging();
					this._IssueKey = value;
					this.SendPropertyChanged("IssueKey");
					this.OnIssueKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Level]", Storage="_Level", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if ((this._Level != value))
				{
					this.OnLevelChanging(value);
					this.SendPropertyChanging();
					this._Level = value;
					this.SendPropertyChanged("Level");
					this.OnLevelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Ticket", DbType="Int NOT NULL")]
		public int Ticket
		{
			get
			{
				return this._Ticket;
			}
			set
			{
				if ((this._Ticket != value))
				{
					this.OnTicketChanging(value);
					this.SendPropertyChanging();
					this._Ticket = value;
					this.SendPropertyChanged("Ticket");
					this.OnTicketChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Message", DbType="NVarChar(MAX)")]
		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				if ((this._Message != value))
				{
					this.OnMessageChanging(value);
					this.SendPropertyChanging();
					this._Message = value;
					this.SendPropertyChanged("Message");
					this.OnMessageChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StackTrace", DbType="NVarChar(MAX)")]
		public string StackTrace
		{
			get
			{
				return this._StackTrace;
			}
			set
			{
				if ((this._StackTrace != value))
				{
					this.OnStackTraceChanging(value);
					this.SendPropertyChanging();
					this._StackTrace = value;
					this.SendPropertyChanged("StackTrace");
					this.OnStackTraceChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProjectName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ProjectName
		{
			get
			{
				return this._ProjectName;
			}
			set
			{
				if ((this._ProjectName != value))
				{
					this.OnProjectNameChanging(value);
					this.SendPropertyChanging();
					this._ProjectName = value;
					this.SendPropertyChanged("ProjectName");
					this.OnProjectNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ApplicationName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string ApplicationName
		{
			get
			{
				return this._ApplicationName;
			}
			set
			{
				if ((this._ApplicationName != value))
				{
					this.OnApplicationNameChanging(value);
					this.SendPropertyChanging();
					this._ApplicationName = value;
					this.SendPropertyChanged("ApplicationName");
					this.OnApplicationNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_VersionNumber", DbType="VarChar(128) NOT NULL", CanBeNull=false)]
		public string VersionNumber
		{
			get
			{
				return this._VersionNumber;
			}
			set
			{
				if ((this._VersionNumber != value))
				{
					this.OnVersionNumberChanging(value);
					this.SendPropertyChanging();
					this._VersionNumber = value;
					this.SendPropertyChanged("VersionNumber");
					this.OnVersionNumberChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591