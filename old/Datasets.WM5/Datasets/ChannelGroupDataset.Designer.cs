﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.CompactFramework.Design.Data, Version 2.0.50727.3053.
// 
namespace Nucleo.GoodGuide.Datasets.Datasets {
    using System;
    using System.Data;
    
    
    /// <summary>
    ///Represents a strongly typed in-memory cache of data.
    ///</summary>
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [global::System.Xml.Serialization.XmlRootAttribute("ChannelGroupDataset")]
    public partial class ChannelGroupDataset : global::System.Data.DataSet {
        
        private ChannelGroupDataTable tableChannelGroup;
        
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ChannelGroupDataset() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ChannelGroupDataTable ChannelGroup {
            get {
                return this.tableChannelGroup;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override global::System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public new global::System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public new global::System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override global::System.Data.DataSet Clone() {
            ChannelGroupDataset cln = ((ChannelGroupDataset)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["ChannelGroup"] != null)) {
                    base.Tables.Add(new ChannelGroupDataTable(ds.Tables["ChannelGroup"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
            this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable) {
            this.tableChannelGroup = ((ChannelGroupDataTable)(base.Tables["ChannelGroup"]));
            if ((initTable == true)) {
                if ((this.tableChannelGroup != null)) {
                    this.tableChannelGroup.InitVars();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "ChannelGroupDataset";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/ChannelGroupDataset.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableChannelGroup = new ChannelGroupDataTable();
            base.Tables.Add(this.tableChannelGroup);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeChannelGroup() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
            ChannelGroupDataset ds = new ChannelGroupDataset();
            global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
            global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
            global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
            if (xs.Contains(dsSchema.TargetNamespace)) {
                global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                try {
                    global::System.Xml.Schema.XmlSchema schema = null;
                    dsSchema.Write(s1);
                    for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                        schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                        s2.SetLength(0);
                        schema.Write(s2);
                        if ((s1.Length == s2.Length)) {
                            s1.Position = 0;
                            s2.Position = 0;
                            for (; ((s1.Position != s1.Length) 
                                        && (s1.ReadByte() == s2.ReadByte())); ) {
                                ;
                            }
                            if ((s1.Position == s1.Length)) {
                                return type;
                            }
                        }
                    }
                }
                finally {
                    if ((s1 != null)) {
                        s1.Close();
                    }
                    if ((s2 != null)) {
                        s2.Close();
                    }
                }
            }
            xs.Add(dsSchema);
            return type;
        }
        
        public delegate void ChannelGroupRowChangeEventHandler(object sender, ChannelGroupRowChangeEvent e);
        
        /// <summary>
        ///Represents the strongly named DataTable class.
        ///</summary>
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class ChannelGroupDataTable : global::System.Data.DataTable, global::System.Collections.IEnumerable {
            
            private global::System.Data.DataColumn columnId;
            
            private global::System.Data.DataColumn columnName;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ChannelGroupDataTable() {
                this.TableName = "ChannelGroup";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal ChannelGroupDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn IdColumn {
                get {
                    return this.columnId;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn NameColumn {
                get {
                    return this.columnName;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ChannelGroupRow this[int index] {
                get {
                    return ((ChannelGroupRow)(this.Rows[index]));
                }
            }
            
            public event ChannelGroupRowChangeEventHandler ChannelGroupRowChanging;
            
            public event ChannelGroupRowChangeEventHandler ChannelGroupRowChanged;
            
            public event ChannelGroupRowChangeEventHandler ChannelGroupRowDeleting;
            
            public event ChannelGroupRowChangeEventHandler ChannelGroupRowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddChannelGroupRow(ChannelGroupRow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ChannelGroupRow AddChannelGroupRow(int Id, string Name) {
                ChannelGroupRow rowChannelGroupRow = ((ChannelGroupRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        Id,
                        Name};
                rowChannelGroupRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowChannelGroupRow);
                return rowChannelGroupRow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual global::System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override global::System.Data.DataTable Clone() {
                ChannelGroupDataTable cln = ((ChannelGroupDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataTable CreateInstance() {
                return new ChannelGroupDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnId = base.Columns["Id"];
                this.columnName = base.Columns["Name"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnId = new global::System.Data.DataColumn("Id", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnId);
                this.columnName = new global::System.Data.DataColumn("Name", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnName);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ChannelGroupRow NewChannelGroupRow() {
                return ((ChannelGroupRow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new ChannelGroupRow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Type GetRowType() {
                return typeof(ChannelGroupRow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.ChannelGroupRowChanged != null)) {
                    this.ChannelGroupRowChanged(this, new ChannelGroupRowChangeEvent(((ChannelGroupRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.ChannelGroupRowChanging != null)) {
                    this.ChannelGroupRowChanging(this, new ChannelGroupRowChangeEvent(((ChannelGroupRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.ChannelGroupRowDeleted != null)) {
                    this.ChannelGroupRowDeleted(this, new ChannelGroupRowChangeEvent(((ChannelGroupRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.ChannelGroupRowDeleting != null)) {
                    this.ChannelGroupRowDeleting(this, new ChannelGroupRowChangeEvent(((ChannelGroupRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveChannelGroupRow(ChannelGroupRow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                ChannelGroupDataset ds = new ChannelGroupDataset();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "ChannelGroupDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        
        /// <summary>
        ///Represents strongly named DataRow class.
        ///</summary>
        public partial class ChannelGroupRow : global::System.Data.DataRow {
            
            private ChannelGroupDataTable tableChannelGroup;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal ChannelGroupRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableChannelGroup = ((ChannelGroupDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public int Id {
                get {
                    try {
                        return ((int)(this[this.tableChannelGroup.IdColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'Id\' in table \'ChannelGroup\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableChannelGroup.IdColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string Name {
                get {
                    if (this.IsNameNull()) {
                        return string.Empty;
                    }
                    else {
                        return ((string)(this[this.tableChannelGroup.NameColumn]));
                    }
                }
                set {
                    this[this.tableChannelGroup.NameColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsIdNull() {
                return this.IsNull(this.tableChannelGroup.IdColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetIdNull() {
                this[this.tableChannelGroup.IdColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsNameNull() {
                return this.IsNull(this.tableChannelGroup.NameColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetNameNull() {
                this[this.tableChannelGroup.NameColumn] = global::System.Convert.DBNull;
            }
        }
        
        /// <summary>
        ///Row event argument class
        ///</summary>
        public class ChannelGroupRowChangeEvent : global::System.EventArgs {
            
            private ChannelGroupRow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ChannelGroupRowChangeEvent(ChannelGroupRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ChannelGroupRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}
namespace ChannelGroupDatasetUtil {
    using System;
    using System.Data;
    
    
    public partial class DesignerUtil {
        
        public static bool IsDesignTime() {
            // Determine if this instance is running against .NET Framework by using the MSCoreLib PublicKeyToken
            System.Reflection.Assembly mscorlibAssembly = typeof(int).Assembly;
            if ((mscorlibAssembly != null)) {
                if (mscorlibAssembly.FullName.ToUpper().EndsWith("B77A5C561934E089")) {
                    return true;
                }
            }
            return false;
        }
        
        public static bool IsRunTime() {
            // Determine if this instance is running against .NET Compact Framework by using the MSCoreLib PublicKeyToken
            System.Reflection.Assembly mscorlibAssembly = typeof(int).Assembly;
            if ((mscorlibAssembly != null)) {
                if (mscorlibAssembly.FullName.ToUpper().EndsWith("969DB8053D3322AC")) {
                    return true;
                }
            }
            return false;
        }
    }
}
