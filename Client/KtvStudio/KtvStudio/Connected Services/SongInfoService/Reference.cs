﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace KtvStudio.SongInfoService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SongInfo", Namespace="http://schemas.datacontract.org/2004/07/DataModel")]
    [System.SerializableAttribute()]
    public partial class SongInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DubbingUrlField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LanguageTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MVUrlField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MusicNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MusicNameInitialsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RecordNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> ReleaseDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SingerIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SingerNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Category {
            get {
                return this.CategoryField;
            }
            set {
                if ((object.ReferenceEquals(this.CategoryField, value) != true)) {
                    this.CategoryField = value;
                    this.RaisePropertyChanged("Category");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DubbingUrl {
            get {
                return this.DubbingUrlField;
            }
            set {
                if ((object.ReferenceEquals(this.DubbingUrlField, value) != true)) {
                    this.DubbingUrlField = value;
                    this.RaisePropertyChanged("DubbingUrl");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LanguageType {
            get {
                return this.LanguageTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.LanguageTypeField, value) != true)) {
                    this.LanguageTypeField = value;
                    this.RaisePropertyChanged("LanguageType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MVUrl {
            get {
                return this.MVUrlField;
            }
            set {
                if ((object.ReferenceEquals(this.MVUrlField, value) != true)) {
                    this.MVUrlField = value;
                    this.RaisePropertyChanged("MVUrl");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MusicName {
            get {
                return this.MusicNameField;
            }
            set {
                if ((object.ReferenceEquals(this.MusicNameField, value) != true)) {
                    this.MusicNameField = value;
                    this.RaisePropertyChanged("MusicName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MusicNameInitials {
            get {
                return this.MusicNameInitialsField;
            }
            set {
                if ((object.ReferenceEquals(this.MusicNameInitialsField, value) != true)) {
                    this.MusicNameInitialsField = value;
                    this.RaisePropertyChanged("MusicNameInitials");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RecordNumber {
            get {
                return this.RecordNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.RecordNumberField, value) != true)) {
                    this.RecordNumberField = value;
                    this.RaisePropertyChanged("RecordNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ReleaseDate {
            get {
                return this.ReleaseDateField;
            }
            set {
                if ((this.ReleaseDateField.Equals(value) != true)) {
                    this.ReleaseDateField = value;
                    this.RaisePropertyChanged("ReleaseDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SingerId {
            get {
                return this.SingerIdField;
            }
            set {
                if ((this.SingerIdField.Equals(value) != true)) {
                    this.SingerIdField = value;
                    this.RaisePropertyChanged("SingerId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingerName {
            get {
                return this.SingerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerNameField, value) != true)) {
                    this.SingerNameField = value;
                    this.RaisePropertyChanged("SingerName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SingerInfo", Namespace="http://schemas.datacontract.org/2004/07/DataModel")]
    [System.SerializableAttribute()]
    public partial class SingerInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SingerClickNumField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SingerEnglishNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SingerInitialsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SingerIntroduceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SingerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SingerNationalityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SingerOtherNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SingerPhotoUrlField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SingerSexField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingerClickNum {
            get {
                return this.SingerClickNumField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerClickNumField, value) != true)) {
                    this.SingerClickNumField = value;
                    this.RaisePropertyChanged("SingerClickNum");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingerEnglishName {
            get {
                return this.SingerEnglishNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerEnglishNameField, value) != true)) {
                    this.SingerEnglishNameField = value;
                    this.RaisePropertyChanged("SingerEnglishName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingerInitials {
            get {
                return this.SingerInitialsField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerInitialsField, value) != true)) {
                    this.SingerInitialsField = value;
                    this.RaisePropertyChanged("SingerInitials");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingerIntroduce {
            get {
                return this.SingerIntroduceField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerIntroduceField, value) != true)) {
                    this.SingerIntroduceField = value;
                    this.RaisePropertyChanged("SingerIntroduce");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingerName {
            get {
                return this.SingerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerNameField, value) != true)) {
                    this.SingerNameField = value;
                    this.RaisePropertyChanged("SingerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingerNationality {
            get {
                return this.SingerNationalityField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerNationalityField, value) != true)) {
                    this.SingerNationalityField = value;
                    this.RaisePropertyChanged("SingerNationality");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingerOtherName {
            get {
                return this.SingerOtherNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerOtherNameField, value) != true)) {
                    this.SingerOtherNameField = value;
                    this.RaisePropertyChanged("SingerOtherName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingerPhotoUrl {
            get {
                return this.SingerPhotoUrlField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerPhotoUrlField, value) != true)) {
                    this.SingerPhotoUrlField = value;
                    this.RaisePropertyChanged("SingerPhotoUrl");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SingerSex {
            get {
                return this.SingerSexField;
            }
            set {
                if ((object.ReferenceEquals(this.SingerSexField, value) != true)) {
                    this.SingerSexField = value;
                    this.RaisePropertyChanged("SingerSex");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SongInfoService.ISongInfoManagement")]
    public interface ISongInfoManagement {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetAllSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/GetAllSongInfoResponse")]
        System.Data.DataTable GetAllSongInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetAllSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/GetAllSongInfoResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetAllSongInfoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/AddSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/AddSongInfoResponse")]
        int AddSongInfo(KtvStudio.SongInfoService.SongInfo songInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/AddSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/AddSongInfoResponse")]
        System.Threading.Tasks.Task<int> AddSongInfoAsync(KtvStudio.SongInfoService.SongInfo songInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/UpdateSongeInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/UpdateSongeInfoResponse")]
        int UpdateSongeInfo(KtvStudio.SongInfoService.SongInfo songInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/UpdateSongeInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/UpdateSongeInfoResponse")]
        System.Threading.Tasks.Task<int> UpdateSongeInfoAsync(KtvStudio.SongInfoService.SongInfo songInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/DeleteSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/DeleteSongInfoResponse")]
        int DeleteSongInfo(KtvStudio.SongInfoService.SongInfo songInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/DeleteSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/DeleteSongInfoResponse")]
        System.Threading.Tasks.Task<int> DeleteSongInfoAsync(KtvStudio.SongInfoService.SongInfo songInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSongInfoBySingerId", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSongInfoBySingerIdResponse")]
        System.Data.DataTable GetSongInfoBySingerId(string singerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSongInfoBySingerId", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSongInfoBySingerIdResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetSongInfoBySingerIdAsync(string singerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetAllCategorySource", ReplyAction="http://tempuri.org/ISongInfoManagement/GetAllCategorySourceResponse")]
        System.Data.DataTable GetAllCategorySource();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetAllCategorySource", ReplyAction="http://tempuri.org/ISongInfoManagement/GetAllCategorySourceResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetAllCategorySourceAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetCategorySourceByFatherId", ReplyAction="http://tempuri.org/ISongInfoManagement/GetCategorySourceByFatherIdResponse")]
        System.Data.DataTable GetCategorySourceByFatherId(string fatherId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetCategorySourceByFatherId", ReplyAction="http://tempuri.org/ISongInfoManagement/GetCategorySourceByFatherIdResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetCategorySourceByFatherIdAsync(string fatherId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceByInitial", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceByInitialResponse")]
        System.Data.DataTable GetSingerFilterSourceByInitial(string nationality, string sex, string initial);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceByInitial", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceByInitialResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetSingerFilterSourceByInitialAsync(string nationality, string sex, string initial);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceByRank", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceByRankResponse")]
        System.Data.DataTable GetSingerFilterSourceByRank(string nationality, string sex);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceByRank", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceByRankResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetSingerFilterSourceByRankAsync(string nationality, string sex);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceAllNationalityByIniti" +
            "al", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceAllNationalityByIniti" +
            "alResponse")]
        System.Data.DataTable GetSingerFilterSourceAllNationalityByInitial(string initial);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceAllNationalityByIniti" +
            "al", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceAllNationalityByIniti" +
            "alResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetSingerFilterSourceAllNationalityByInitialAsync(string initial);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceAllNationalityByRank", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceAllNationalityByRankR" +
            "esponse")]
        System.Data.DataTable GetSingerFilterSourceAllNationalityByRank();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceAllNationalityByRank", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSingerFilterSourceAllNationalityByRankR" +
            "esponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetSingerFilterSourceAllNationalityByRankAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSingerInfoById", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSingerInfoByIdResponse")]
        KtvStudio.SongInfoService.SingerInfo GetSingerInfoById(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetSingerInfoById", ReplyAction="http://tempuri.org/ISongInfoManagement/GetSingerInfoByIdResponse")]
        System.Threading.Tasks.Task<KtvStudio.SongInfoService.SingerInfo> GetSingerInfoByIdAsync(string id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISongInfoManagementChannel : KtvStudio.SongInfoService.ISongInfoManagement, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SongInfoManagementClient : System.ServiceModel.ClientBase<KtvStudio.SongInfoService.ISongInfoManagement>, KtvStudio.SongInfoService.ISongInfoManagement {
        
        public SongInfoManagementClient() {
        }
        
        public SongInfoManagementClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SongInfoManagementClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SongInfoManagementClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SongInfoManagementClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataTable GetAllSongInfo() {
            return base.Channel.GetAllSongInfo();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetAllSongInfoAsync() {
            return base.Channel.GetAllSongInfoAsync();
        }
        
        public int AddSongInfo(KtvStudio.SongInfoService.SongInfo songInfo) {
            return base.Channel.AddSongInfo(songInfo);
        }
        
        public System.Threading.Tasks.Task<int> AddSongInfoAsync(KtvStudio.SongInfoService.SongInfo songInfo) {
            return base.Channel.AddSongInfoAsync(songInfo);
        }
        
        public int UpdateSongeInfo(KtvStudio.SongInfoService.SongInfo songInfo) {
            return base.Channel.UpdateSongeInfo(songInfo);
        }
        
        public System.Threading.Tasks.Task<int> UpdateSongeInfoAsync(KtvStudio.SongInfoService.SongInfo songInfo) {
            return base.Channel.UpdateSongeInfoAsync(songInfo);
        }
        
        public int DeleteSongInfo(KtvStudio.SongInfoService.SongInfo songInfo) {
            return base.Channel.DeleteSongInfo(songInfo);
        }
        
        public System.Threading.Tasks.Task<int> DeleteSongInfoAsync(KtvStudio.SongInfoService.SongInfo songInfo) {
            return base.Channel.DeleteSongInfoAsync(songInfo);
        }
        
        public System.Data.DataTable GetSongInfoBySingerId(string singerId) {
            return base.Channel.GetSongInfoBySingerId(singerId);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetSongInfoBySingerIdAsync(string singerId) {
            return base.Channel.GetSongInfoBySingerIdAsync(singerId);
        }
        
        public System.Data.DataTable GetAllCategorySource() {
            return base.Channel.GetAllCategorySource();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetAllCategorySourceAsync() {
            return base.Channel.GetAllCategorySourceAsync();
        }
        
        public System.Data.DataTable GetCategorySourceByFatherId(string fatherId) {
            return base.Channel.GetCategorySourceByFatherId(fatherId);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetCategorySourceByFatherIdAsync(string fatherId) {
            return base.Channel.GetCategorySourceByFatherIdAsync(fatherId);
        }
        
        public System.Data.DataTable GetSingerFilterSourceByInitial(string nationality, string sex, string initial) {
            return base.Channel.GetSingerFilterSourceByInitial(nationality, sex, initial);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetSingerFilterSourceByInitialAsync(string nationality, string sex, string initial) {
            return base.Channel.GetSingerFilterSourceByInitialAsync(nationality, sex, initial);
        }
        
        public System.Data.DataTable GetSingerFilterSourceByRank(string nationality, string sex) {
            return base.Channel.GetSingerFilterSourceByRank(nationality, sex);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetSingerFilterSourceByRankAsync(string nationality, string sex) {
            return base.Channel.GetSingerFilterSourceByRankAsync(nationality, sex);
        }
        
        public System.Data.DataTable GetSingerFilterSourceAllNationalityByInitial(string initial) {
            return base.Channel.GetSingerFilterSourceAllNationalityByInitial(initial);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetSingerFilterSourceAllNationalityByInitialAsync(string initial) {
            return base.Channel.GetSingerFilterSourceAllNationalityByInitialAsync(initial);
        }
        
        public System.Data.DataTable GetSingerFilterSourceAllNationalityByRank() {
            return base.Channel.GetSingerFilterSourceAllNationalityByRank();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetSingerFilterSourceAllNationalityByRankAsync() {
            return base.Channel.GetSingerFilterSourceAllNationalityByRankAsync();
        }
        
        public KtvStudio.SongInfoService.SingerInfo GetSingerInfoById(string id) {
            return base.Channel.GetSingerInfoById(id);
        }
        
        public System.Threading.Tasks.Task<KtvStudio.SongInfoService.SingerInfo> GetSingerInfoByIdAsync(string id) {
            return base.Channel.GetSingerInfoByIdAsync(id);
        }
    }
}