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

namespace StudioAdvanced
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="StudioAdvanced")]
	public partial class SystemParametersDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSystemParameter(SystemParameter instance);
    partial void UpdateSystemParameter(SystemParameter instance);
    partial void DeleteSystemParameter(SystemParameter instance);
    #endregion
		
		public SystemParametersDataContext() : 
				base(global::StudioAdvanced.Properties.Settings.Default.StudioAdvancedConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SystemParametersDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SystemParametersDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SystemParametersDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SystemParametersDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SystemParameter> SystemParameters
		{
			get
			{
				return this.GetTable<SystemParameter>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SystemParameters")]
	public partial class SystemParameter : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ParameterID = default(int);
		
		private string _Description;
		
		private string _Value;
		
		private string _MiscDescription;
		
		private System.DateTime _Created;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnValueChanging(string value);
    partial void OnValueChanged();
    partial void OnMiscDescriptionChanging(string value);
    partial void OnMiscDescriptionChanged();
    partial void OnCreatedChanging(System.DateTime value);
    partial void OnCreatedChanged();
    #endregion
		
		public SystemParameter()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ParameterID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public int ParameterID
		{
			get
			{
				return this._ParameterID;
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", AutoSync=AutoSync.OnInsert, DbType="VarChar(500) NOT NULL", CanBeNull=false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Value", AutoSync=AutoSync.OnInsert, DbType="VarChar(200) NOT NULL", CanBeNull=false)]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if ((this._Value != value))
				{
					this.OnValueChanging(value);
					this.SendPropertyChanging();
					this._Value = value;
					this.SendPropertyChanged("Value");
					this.OnValueChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MiscDescription", AutoSync=AutoSync.OnInsert, DbType="VarChar(500)")]
		public string MiscDescription
		{
			get
			{
				return this._MiscDescription;
			}
			set
			{
				if ((this._MiscDescription != value))
				{
					this.OnMiscDescriptionChanging(value);
					this.SendPropertyChanging();
					this._MiscDescription = value;
					this.SendPropertyChanged("MiscDescription");
					this.OnMiscDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Created", DbType="DateTime NOT NULL")]
		public System.DateTime Created
		{
			get
			{
				return this._Created;
			}
			set
			{
				if ((this._Created != value))
				{
					this.OnCreatedChanging(value);
					this.SendPropertyChanging();
					this._Created = value;
					this.SendPropertyChanged("Created");
					this.OnCreatedChanged();
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