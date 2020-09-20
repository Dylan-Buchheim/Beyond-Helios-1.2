using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Prime31;



#if UNITY_IOS || UNITY_TVOS
namespace Prime31
{
	public class iCloudManager : AbstractManager
	{
		/// <summary>
		/// Fired when a change is synced from iCloud. Includes a list of all the keys that were changed.
		/// </summary>
		public static event Action<List<object>> keyValueStoreDidChangeEvent;

		/// <summary>
		/// Fired when a change is synced from iCloud that DOES NOT contain any changed data. When data changes
		/// the keyValueStoreDidChangeEvent will fire.
		/// </summary>
		public static event Action<iCloudKeyValueStoreReason> keyValueStoreDidChangeReasonEvent;

		/// <summary>
		/// Fired when the ubiquity identity changes. You can use iCloudBinding.getUbiquityIdentityToken to get the current token
		/// </summary>
		public static event Action ubiquityIdentityDidChangeEvent;

		/// <summary>
		/// Fired when a synchronize fails and the console has a missing entitlements message in it
		/// </summary>
		public static event Action entitlementsMissingEvent;

		/// <summary>
		/// Fired when the document store changes. Includes a list of all files and if they are downloaded.
		/// </summary>
		public static event Action<List<iCloudDocument>> documentStoreUpdatedEvent;

		/// <summary>
		/// Fired when a CloudKit save fails
		/// </summary>
		public static event Action<string> cloudKitSaveRecordFailedEvent;

		/// <summary>
		/// Fired when a CloudKit save succeeds
		/// </summary>
		public static event Action cloudKitSaveRecordSucceededEvent;

		/// <summary>
		/// Fired when a CloudKit fetch fails
		/// </summary>
		public static event Action<string> cloudKitFetchRecordFailedEvent;

		/// <summary>
		/// Fired when a CloudKit fetch succeeds. The default keys in the dictionary will be recordType, recordId, title and fileUrl.
		/// </summary>
		public static event Action<Dictionary<string,string>> cloudKitFetchRecordSucceededEvent;

		/// <summary>
		/// Fired when a CloudKit update fails
		/// </summary>
		public static event Action<string> cloudKitUpdateRecordFailedEvent;

		/// <summary>
		/// Fired when a CloudKit update succeeds
		/// </summary>
		public static event Action cloudKitUpdateRecordSucceededEvent;

		/// <summary>
		/// Fired when a CloudKit delete fails
		/// </summary>
		public static event Action<string> cloudKitDeleteRecordFailedEvent;

		/// <summary>
		/// Fired when a CloudKit delete succeeds
		/// </summary>
		public static event Action cloudKitDeleteRecordSucceededEvent;


		public class iCloudDocument
		{
			public string filename;
			public bool isDownloaded;
			public DateTime contentChangedDate;
			public string downloadStatus;
			public string path;


			public iCloudDocument( Dictionary<string,object> dict )
			{
				if( dict.ContainsKey( "filename" ) )
					filename = dict["filename"].ToString();

				if( dict.ContainsKey( "isDownloaded" ) )
					isDownloaded = bool.Parse( dict["isDownloaded"].ToString() );

				var intermediate = new System.DateTime( 1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc );

				if( dict.ContainsKey( "contentChangedDate" ) )
				{
					var timeSinceEpoch = double.Parse( dict["contentChangedDate"].ToString() );
					contentChangedDate = intermediate.AddSeconds( timeSinceEpoch );
				}

				if( dict.ContainsKey( "downloadStatus" ) )
					downloadStatus = dict["downloadStatus"].ToString();

				if( dict.ContainsKey( "path" ) )
					path = dict["path"].ToString();
			}


			public static List<iCloudDocument> fromJSON( string json )
			{
				var list = json.listFromJson();
				var files = new List<iCloudDocument>( list.Count );

				foreach( Dictionary<string,object> dict in list )
					files.Add( new iCloudDocument( dict ) );

				return files;
			}


			public override string ToString()
			{
				 return string.Format( "<iCloudDocument> filename: {0}, isDownloaded: {1}, contentChangedDate: {2}", filename, isDownloaded, contentChangedDate );
			}
		}


		static iCloudManager()
		{
			AbstractManager.initialize( typeof( iCloudManager ) );
		}


		#region iCloud

		void ubiquityIdentityDidChange( string param )
		{
			ubiquityIdentityDidChangeEvent.fire();
		}


		void keyValueStoreDidChange( string param )
		{
			if( keyValueStoreDidChangeEvent != null )
			{
				var obj = param.listFromJson();
				keyValueStoreDidChangeEvent( obj );
			}
		}


		void keyValueStoreChangeReason( string reason )
		{
			if( keyValueStoreDidChangeReasonEvent != null )
			{
				int reasonInt;
				if( int.TryParse( reason, out reasonInt ) )
					keyValueStoreDidChangeReasonEvent( (iCloudKeyValueStoreReason)reasonInt );
			}
		}


		void entitlementsMissing( string empty )
		{
			if( entitlementsMissingEvent != null )
				entitlementsMissingEvent();
		}


		void documentStoreUpdated( string json )
		{
			if( documentStoreUpdatedEvent != null )
			{
				var files = iCloudDocument.fromJSON( json );
				documentStoreUpdatedEvent( files );
			}
		}

		#endregion


		#region CloudKit

		void cloudKitSaveRecordFailed( string error )
		{
			cloudKitSaveRecordFailedEvent.fire( error );
		}


		void cloudKitSaveRecordSucceeded( string empty )
		{
			cloudKitSaveRecordSucceededEvent.fire();
		}


		void cloudKitFetchRecordFailed( string error )
		{
			cloudKitFetchRecordFailedEvent.fire( error );
		}


		void cloudKitFetchRecordSucceeded( string json )
		{
			cloudKitFetchRecordSucceededEvent.fire( Json.decode<Dictionary<string,string>>( json ) );
		}


		void cloudKitUpdateRecordFailed( string error )
		{
			cloudKitUpdateRecordFailedEvent.fire( error );
		}


		void cloudKitUpdateRecordSucceeded( string empty )
		{
			cloudKitUpdateRecordSucceededEvent.fire();
		}


		void cloudKitDeleteRecordFailed( string error )
		{
			cloudKitDeleteRecordFailedEvent.fire( error );
		}


		void cloudKitDeleteRecordSucceeded( string empty )
		{
			cloudKitDeleteRecordSucceededEvent.fire();
		}

		#endregion

	}
}
#endif
