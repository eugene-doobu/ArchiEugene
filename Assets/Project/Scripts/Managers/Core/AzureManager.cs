using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azure.StorageServices;
using Newtonsoft.Json;
using RESTClient;

namespace ArchiEugene.Azure
{
    public class AzureManager
    {
        private readonly string AZURE_INFO_JSON_NAME = "AzureInfo";
        
        private string _storageAccount;
        private string _accessKey;
        private string _userID;
        
        private string _userPropContainer;

        private StorageServiceClient _client;
        private BlobService _blobService;

        public void Init()
        {
            GetAzureAccountInfo();
            
            _client = StorageServiceClient.Create(_storageAccount, _accessKey);
            _blobService = _client.GetBlobService();

            GetAzureData();
        }

        private void GetAzureData()
        {
            GetUserPropData();
        }

        private void GetUserPropData()
        {
            if (_blobService == null || _userID == string.Empty) return;
            string resourceFullPath = $"{_userPropContainer}/{_userID}/{Define.PROP_JSON_NAME}.json";

            Managers.Instance.StartCoroutine(_blobService.GetTextBlob(GetUserPropDataComplete, resourceFullPath));
        }

        private void GetUserPropDataComplete(RestResponse response)
        {
            if (response.IsError)
            {
                Debug.Log($"{response.ErrorMessage} Error putting blob:{response.Content}");
                return;
            }
            Managers.UserProp.InitUserPropData();
            Debug.Log($"GetUserPropDataComplete\n---\n{response.Content}");
        }

        public void SaveUserData<T>(T data, string filename)
        {
            if (_blobService == null || _userID == string.Empty) return;
            
            string fullFileName = $"{filename}.json";
            string resourceFullPath = $"{_userPropContainer}/{_userID}";
            string jsonData = JsonConvert.SerializeObject(data);
            Managers.Instance.StartCoroutine(_blobService.PutTextBlob(SaveUserDataComplete, jsonData, resourceFullPath, fullFileName));
        }

        private void SaveUserDataComplete(RestResponse response)
        {
            if (response.IsError)
            {
                Debug.Log($"{response.ErrorMessage} Error putting blob:{response.Content}");
                return;
            }
            Debug.Log($"Put blob status: {response.StatusCode}");
        }

        private void GetAzureAccountInfo()
        {
            var loader = Managers.Data.LoadPersistentJson<AzureInfoData, int, AzureInfo>(AZURE_INFO_JSON_NAME);
            if (loader == null)
            {
                Debug.LogError("[Azure] Azure 계정정보가 없습니다");
                return;
            }
            var data = loader.GetData();
            _storageAccount = data.storageAccount;
            _accessKey = data.accessKey;
            _userID = data.userId;
            _userPropContainer = data.userPropContainer;
        }
    }
}
