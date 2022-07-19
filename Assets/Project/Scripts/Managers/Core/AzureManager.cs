using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azure.StorageServices;

namespace ArchiEugene.Azure
{
    public class AzureManager
    {
        private readonly string AZURE_INFO_JSON_NAME = "AzureInfo";
        
        private string _storageAccount;
        private string _accessKey;
        
        private string _userPropContainer;

        private StorageServiceClient _client;
        private BlobService _blobService;

        public void Init()
        {
            var loader = Managers.Data.LoadPersistentJson<AzureInfoData, int, AzureInfo>(AZURE_INFO_JSON_NAME);
            if (loader == null)
            {
                Debug.LogError("[Azure] Azure 계정정보가 없습니다");
            }
            else
            {
                var data = loader.GetData();
                _storageAccount = data.storageAccount;
                _accessKey = data.accessKey;
                _userPropContainer = data.userPropContainer;
            }
        }
    }
}
