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
    [global::System.Xml.Serialization.XmlRootAttribute("ItineraryDestinationDataset")]
    public partial class ItineraryDestinationDataset : global::System.Data.DataSet {
        
        private ItineraryDestinationDataTable tableItineraryDestination;
        
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ItineraryDestinationDataset() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public ItineraryDestinationDataTable ItineraryDestination {
            get {
                return this.tableItineraryDestination;
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
            ItineraryDestinationDataset cln = ((ItineraryDestinationDataset)(base.Clone()));
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
                if ((ds.Tables["ItineraryDestination"] != null)) {
                    base.Tables.Add(new ItineraryDestinationDataTable(ds.Tables["ItineraryDestination"]));
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
            this.tableItineraryDestination = ((ItineraryDestinationDataTable)(base.Tables["ItineraryDestination"]));
            if ((initTable == true)) {
                if ((this.tableItineraryDestination != null)) {
                    this.tableItineraryDestination.InitVars();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "ItineraryDestinationDataset";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/ItineraryDestinationDataset.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableItineraryDestination = new ItineraryDestinationDataTable();
            base.Tables.Add(this.tableItineraryDestination);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeItineraryDestination() {
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
            ItineraryDestinationDataset ds = new ItineraryDestinationDataset();
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
        
        public delegate void ItineraryDestinationRowChangeEventHandler(object sender, ItineraryDestinationRowChangeEvent e);
        
        /// <summary>
        ///Represents the strongly named DataTable class.
        ///</summary>
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class ItineraryDestinationDataTable : global::System.Data.DataTable, global::System.Collections.IEnumerable {
            
            private global::System.Data.DataColumn columnId;
            
            private global::System.Data.DataColumn columnItineraryDayId;
            
            private global::System.Data.DataColumn columnDestinationId;
            
            private global::System.Data.DataColumn columnDestinationOrder;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ItineraryDestinationDataTable() {
                this.TableName = "ItineraryDestination";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal ItineraryDestinationDataTable(global::System.Data.DataTable table) {
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
            public global::System.Data.DataColumn ItineraryDayIdColumn {
                get {
                    return this.columnItineraryDayId;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn DestinationIdColumn {
                get {
                    return this.columnDestinationId;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public global::System.Data.DataColumn DestinationOrderColumn {
                get {
                    return this.columnDestinationOrder;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ItineraryDestinationRow this[int index] {
                get {
                    return ((ItineraryDestinationRow)(this.Rows[index]));
                }
            }
            
            public event ItineraryDestinationRowChangeEventHandler ItineraryDestinationRowChanging;
            
            public event ItineraryDestinationRowChangeEventHandler ItineraryDestinationRowChanged;
            
            public event ItineraryDestinationRowChangeEventHandler ItineraryDestinationRowDeleting;
            
            public event ItineraryDestinationRowChangeEventHandler ItineraryDestinationRowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddItineraryDestinationRow(ItineraryDestinationRow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ItineraryDestinationRow AddItineraryDestinationRow(int Id, int ItineraryDayId, int DestinationId, short DestinationOrder) {
                ItineraryDestinationRow rowItineraryDestinationRow = ((ItineraryDestinationRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        Id,
                        ItineraryDayId,
                        DestinationId,
                        DestinationOrder};
                rowItineraryDestinationRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowItineraryDestinationRow);
                return rowItineraryDestinationRow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual global::System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override global::System.Data.DataTable Clone() {
                ItineraryDestinationDataTable cln = ((ItineraryDestinationDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataTable CreateInstance() {
                return new ItineraryDestinationDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnId = base.Columns["Id"];
                this.columnItineraryDayId = base.Columns["ItineraryDayId"];
                this.columnDestinationId = base.Columns["DestinationId"];
                this.columnDestinationOrder = base.Columns["DestinationOrder"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnId = new global::System.Data.DataColumn("Id", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnId);
                this.columnItineraryDayId = new global::System.Data.DataColumn("ItineraryDayId", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnItineraryDayId);
                this.columnDestinationId = new global::System.Data.DataColumn("DestinationId", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnDestinationId);
                this.columnDestinationOrder = new global::System.Data.DataColumn("DestinationOrder", typeof(short), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnDestinationOrder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ItineraryDestinationRow NewItineraryDestinationRow() {
                return ((ItineraryDestinationRow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new ItineraryDestinationRow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override global::System.Type GetRowType() {
                return typeof(ItineraryDestinationRow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.ItineraryDestinationRowChanged != null)) {
                    this.ItineraryDestinationRowChanged(this, new ItineraryDestinationRowChangeEvent(((ItineraryDestinationRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.ItineraryDestinationRowChanging != null)) {
                    this.ItineraryDestinationRowChanging(this, new ItineraryDestinationRowChangeEvent(((ItineraryDestinationRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.ItineraryDestinationRowDeleted != null)) {
                    this.ItineraryDestinationRowDeleted(this, new ItineraryDestinationRowChangeEvent(((ItineraryDestinationRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.ItineraryDestinationRowDeleting != null)) {
                    this.ItineraryDestinationRowDeleting(this, new ItineraryDestinationRowChangeEvent(((ItineraryDestinationRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveItineraryDestinationRow(ItineraryDestinationRow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                ItineraryDestinationDataset ds = new ItineraryDestinationDataset();
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
                attribute2.FixedValue = "ItineraryDestinationDataTable";
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
        public partial class ItineraryDestinationRow : global::System.Data.DataRow {
            
            private ItineraryDestinationDataTable tableItineraryDestination;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal ItineraryDestinationRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableItineraryDestination = ((ItineraryDestinationDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public int Id {
                get {
                    try {
                        return ((int)(this[this.tableItineraryDestination.IdColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'Id\' in table \'ItineraryDestination\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableItineraryDestination.IdColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public int ItineraryDayId {
                get {
                    try {
                        return ((int)(this[this.tableItineraryDestination.ItineraryDayIdColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ItineraryDayId\' in table \'ItineraryDestination\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableItineraryDestination.ItineraryDayIdColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public int DestinationId {
                get {
                    try {
                        return ((int)(this[this.tableItineraryDestination.DestinationIdColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'DestinationId\' in table \'ItineraryDestination\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableItineraryDestination.DestinationIdColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public short DestinationOrder {
                get {
                    try {
                        return ((short)(this[this.tableItineraryDestination.DestinationOrderColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'DestinationOrder\' in table \'ItineraryDestination\' is DBNull" +
                                ".", e);
                    }
                }
                set {
                    this[this.tableItineraryDestination.DestinationOrderColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsIdNull() {
                return this.IsNull(this.tableItineraryDestination.IdColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetIdNull() {
                this[this.tableItineraryDestination.IdColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsItineraryDayIdNull() {
                return this.IsNull(this.tableItineraryDestination.ItineraryDayIdColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetItineraryDayIdNull() {
                this[this.tableItineraryDestination.ItineraryDayIdColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsDestinationIdNull() {
                return this.IsNull(this.tableItineraryDestination.DestinationIdColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetDestinationIdNull() {
                this[this.tableItineraryDestination.DestinationIdColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsDestinationOrderNull() {
                return this.IsNull(this.tableItineraryDestination.DestinationOrderColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetDestinationOrderNull() {
                this[this.tableItineraryDestination.DestinationOrderColumn] = global::System.Convert.DBNull;
            }
        }
        
        /// <summary>
        ///Row event argument class
        ///</summary>
        public class ItineraryDestinationRowChangeEvent : global::System.EventArgs {
            
            private ItineraryDestinationRow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ItineraryDestinationRowChangeEvent(ItineraryDestinationRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public ItineraryDestinationRow Row {
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
namespace ItineraryDestinationDatasetUtil {
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
