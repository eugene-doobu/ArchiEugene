using System;
using System.Collections.Generic;

namespace ArchiEugene.Azure
{
    [Serializable]
    public class AzureInfo
    {
        public string storageAccount;
        public string accessKey;
        public string userPropContainer;
    }
    
    [Serializable]
    public class AzureInfoData : ILoader<int, AzureInfo>
    {
        public List<AzureInfo> azureInfos = new List<AzureInfo>();
        
        public Dictionary<int, AzureInfo> MakeDict()
        {
            var index = 0; // 0-based index
            var dict = new Dictionary<int, AzureInfo>();
            foreach (var azureInfo in azureInfos)
            {
                dict.Add(index, azureInfo);
                index++;
            }
            return dict;
        }

        public AzureInfo GetData() => azureInfos[0];
    }
}