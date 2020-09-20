using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;



namespace Prime31
{
	public class iCloudEventListener : MonoBehaviour
	{
#if UNITY_IOS
		void OnEnable()
		{
			// Listen to all events for illustration purposes
			iCloudManager.keyValueStoreDidChangeEvent += keyValueStoreDidChangeEvent;
			iCloudManager.keyValueStoreDidChangeReasonEvent += keyValueStoreDidChangeReasonEvent;
			iCloudManager.ubiquityIdentityDidChangeEvent += ubiquityIdentityDidChangeEvent;
			iCloudManager.entitlementsMissingEvent += entitlementsMissingEvent;
			iCloudManager.documentStoreUpdatedEvent += documentStoreUpdatedEvent;

			// CloudKit events
			iCloudManager.cloudKitSaveRecordFailedEvent += cloudKitSaveRecordFailedEvent;
			iCloudManager.cloudKitSaveRecordSucceededEvent += cloudKitSaveRecordSucceededEvent;
			iCloudManager.cloudKitFetchRecordFailedEvent += cloudKitFetchRecordFailedEvent;
			iCloudManager.cloudKitFetchRecordSucceededEvent += cloudKitFetchRecordSucceededEvent;
			iCloudManager.cloudKitUpdateRecordFailedEvent += cloudKitUpdateRecordFailedEvent;
			iCloudManager.cloudKitUpdateRecordSucceededEvent += cloudKitUpdateRecordSucceededEvent;
			iCloudManager.cloudKitDeleteRecordFailedEvent += cloudKitDeleteRecordFailedEvent;
			iCloudManager.cloudKitDeleteRecordSucceededEvent += cloudKitDeleteRecordSucceededEvent;
		}
	
	
		void OnDisable()
		{
			// Remove all event handlers
			iCloudManager.keyValueStoreDidChangeEvent -= keyValueStoreDidChangeEvent;
			iCloudManager.keyValueStoreDidChangeReasonEvent -= keyValueStoreDidChangeReasonEvent;
			iCloudManager.ubiquityIdentityDidChangeEvent -= ubiquityIdentityDidChangeEvent;
			iCloudManager.entitlementsMissingEvent -= entitlementsMissingEvent;
			iCloudManager.documentStoreUpdatedEvent -= documentStoreUpdatedEvent;

			// CloudKit events
			iCloudManager.cloudKitSaveRecordFailedEvent -= cloudKitSaveRecordFailedEvent;
			iCloudManager.cloudKitSaveRecordSucceededEvent -= cloudKitSaveRecordSucceededEvent;
			iCloudManager.cloudKitFetchRecordFailedEvent -= cloudKitFetchRecordFailedEvent;
			iCloudManager.cloudKitFetchRecordSucceededEvent -= cloudKitFetchRecordSucceededEvent;
			iCloudManager.cloudKitUpdateRecordFailedEvent -= cloudKitUpdateRecordFailedEvent;
			iCloudManager.cloudKitUpdateRecordSucceededEvent -= cloudKitUpdateRecordSucceededEvent;
			iCloudManager.cloudKitDeleteRecordFailedEvent -= cloudKitDeleteRecordFailedEvent;
			iCloudManager.cloudKitDeleteRecordSucceededEvent -= cloudKitDeleteRecordSucceededEvent;
		}
	
	
		#region iCloud
	
		void keyValueStoreDidChangeEvent( List<object> keys )
		{
			Debug.Log( "keyValueStoreDidChangeEvent.  changed keys: " );
			foreach( var key in keys )
				Debug.Log( key );
		}


		void keyValueStoreDidChangeReasonEvent( iCloudKeyValueStoreReason reason )
		{
			Debug.Log( "keyValueStoreDidChangeReasonEvent: " + reason );
		}

		
		void ubiquityIdentityDidChangeEvent()
		{
			Debug.Log( "ubiquityIdentityDidChangeEvent" );
		}
		
		
		void entitlementsMissingEvent()
		{
			Debug.Log( "entitlementsMissingEvent" );
		}
		
		
		void documentStoreUpdatedEvent( List<iCloudManager.iCloudDocument> files )
		{
			foreach( var doc in files )
				Debug.Log( doc );
		}

		#endregion


		#region CloudKit

		void cloudKitSaveRecordFailedEvent( string error )
		{
			Debug.Log( "cloudKitSaveRecordFailedEvent: " + error );
		}


		void cloudKitSaveRecordSucceededEvent()
		{
			Debug.Log( "cloudKitSaveRecordSucceededEvent" );
		}


		void cloudKitFetchRecordFailedEvent( string error )
		{
			Debug.Log( "cloudKitFetchRecordFailedEvent: " + error );
		}


		void cloudKitFetchRecordSucceededEvent( Dictionary<string,string> dict )
		{
			Debug.Log( "cloudKitFetchRecordSucceededEvent" );
			Utils.logObject( dict );
		}


		void cloudKitUpdateRecordFailedEvent( string error )
		{
			Debug.Log( "cloudKitUpdateRecordFailedEvent: " + error );
		}


		void cloudKitUpdateRecordSucceededEvent()
		{
			Debug.Log( "cloudKitUpdateRecordSucceededEvent" );
		}


		void cloudKitDeleteRecordFailedEvent( string error )
		{
			Debug.Log( "cloudKitDeleteRecordFailedEvent: " + error );
		}


		void cloudKitDeleteRecordSucceededEvent()
		{
			Debug.Log( "cloudKitDeleteRecordSucceededEvent" );
		}

		#endregion

#endif
	}

}
	
	
