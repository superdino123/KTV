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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SongInfoService.ISongInfoManagement")]
    public interface ISongInfoManagement {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetAllSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/GetAllSongInfoResponse")]
        System.Data.DataTable GetAllSongInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetAllSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/GetAllSongInfoResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetAllSongInfoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/AddSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/AddSongInfoResponse")]
        int AddSongInfo(KtvMusic.SongInfoService.SongInfo songInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/AddSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/AddSongInfoResponse")]
        System.Threading.Tasks.Task<int> AddSongInfoAsync(KtvMusic.SongInfoService.SongInfo songInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/UpdateSongeInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/UpdateSongeInfoResponse")]
        int UpdateSongeInfo(KtvMusic.SongInfoService.SongInfo songInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/UpdateSongeInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/UpdateSongeInfoResponse")]
        System.Threading.Tasks.Task<int> UpdateSongeInfoAsync(KtvMusic.SongInfoService.SongInfo songInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/DeleteSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/DeleteSongInfoResponse")]
        int DeleteSongInfo(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/DeleteSongInfo", ReplyAction="http://tempuri.org/ISongInfoManagement/DeleteSongInfoResponse")]
        System.Threading.Tasks.Task<int> DeleteSongInfoAsync(string id);
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/AddSongRecord", ReplyAction="http://tempuri.org/ISongInfoManagement/AddSongRecordResponse")]
        int AddSongRecord(KtvMusic.SongInfoService.SongRecord[] records);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/AddSongRecord", ReplyAction="http://tempuri.org/ISongInfoManagement/AddSongRecordResponse")]
        System.Threading.Tasks.Task<int> AddSongRecordAsync(KtvMusic.SongInfoService.SongRecord[] records);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetAllSongRecord", ReplyAction="http://tempuri.org/ISongInfoManagement/GetAllSongRecordResponse")]
        System.Data.DataTable GetAllSongRecord();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/GetAllSongRecord", ReplyAction="http://tempuri.org/ISongInfoManagement/GetAllSongRecordResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetAllSongRecordAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/UpdateNewSongRank", ReplyAction="http://tempuri.org/ISongInfoManagement/UpdateNewSongRankResponse")]
        int UpdateNewSongRank(System.Collections.Generic.Dictionary<string, double> newRank);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISongInfoManagement/UpdateNewSongRank", ReplyAction="http://tempuri.org/ISongInfoManagement/UpdateNewSongRankResponse")]
        System.Threading.Tasks.Task<int> UpdateNewSongRankAsync(System.Collections.Generic.Dictionary<string, double> newRank);
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
        
        public int AddSongInfo(KtvMusic.SongInfoService.SongInfo songInfo) {
            return base.Channel.AddSongInfo(songInfo);
        }
        
        public System.Threading.Tasks.Task<int> AddSongInfoAsync(KtvMusic.SongInfoService.SongInfo songInfo) {
            return base.Channel.AddSongInfoAsync(songInfo);
        }
        
        public int UpdateSongeInfo(KtvMusic.SongInfoService.SongInfo songInfo) {
            return base.Channel.UpdateSongeInfo(songInfo);
        }
        
        public System.Threading.Tasks.Task<int> UpdateSongeInfoAsync(KtvMusic.SongInfoService.SongInfo songInfo) {
            return base.Channel.UpdateSongeInfoAsync(songInfo);
        }
        
        public int DeleteSongInfo(string id) {
            return base.Channel.DeleteSongInfo(id);
        }
        
        public System.Threading.Tasks.Task<int> DeleteSongInfoAsync(string id) {
            return base.Channel.DeleteSongInfoAsync(id);
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
        
        public int AddSongRecord(KtvMusic.SongInfoService.SongRecord[] records) {
            return base.Channel.AddSongRecord(records);
        }
        
        public System.Threading.Tasks.Task<int> AddSongRecordAsync(KtvMusic.SongInfoService.SongRecord[] records) {
            return base.Channel.AddSongRecordAsync(records);
        }
        
        public System.Data.DataTable GetAllSongRecord() {
            return base.Channel.GetAllSongRecord();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetAllSongRecordAsync() {
            return base.Channel.GetAllSongRecordAsync();
        }
        
        public int UpdateNewSongRank(System.Collections.Generic.Dictionary<string, double> newRank) {
            return base.Channel.UpdateNewSongRank(newRank);
        }
        
        public System.Threading.Tasks.Task<int> UpdateNewSongRankAsync(System.Collections.Generic.Dictionary<string, double> newRank) {
            return base.Channel.UpdateNewSongRankAsync(newRank);
        }
    }
}
