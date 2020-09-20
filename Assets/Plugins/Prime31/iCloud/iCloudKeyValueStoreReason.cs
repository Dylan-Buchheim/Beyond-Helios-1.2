using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_IOS || UNITY_TVOS
public enum iCloudKeyValueStoreReason
{
	ServerChange = 0,
	InitialSyncChange,
	QuotaViolationChange,
	AccountChange
}
#endif