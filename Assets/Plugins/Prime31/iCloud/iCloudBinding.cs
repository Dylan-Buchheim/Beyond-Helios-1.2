using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Prime31;


#if UNITY_IOS || UNITY_TVOS

namespace Prime31
{
	public class iCloudBinding
	{
		#region General

		[DllImport("__Internal")]
		private static extern string _iCloudUbiquityIdentityToken();

		/// <summary>
		/// Gets either ubiquityIdentityToken if it exists or returns null if it doesn't.
		/// </summary>
		/// <returns>The ubiquity identity token.</returns>
		public static string getUbiquityIdentityToken()
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudUbiquityIdentityToken();

			return string.Empty;
		}


		[DllImport("__Internal")]
		private static extern bool _iCloudWasEnabledLastLaunch();

		/// <summary>
		/// Checks whether iCloud was enabled the last time the app was launched with launched meaning applicationDidBecomeActive
		/// was called by the OS.
		/// </summary>
		/// <returns><c>true</c>, if I cloud enabled last launch was wased, <c>false</c> otherwise.</returns>
		public static bool wasICloudEnabledLastLaunch()
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudWasEnabledLastLaunch();

			return false;
		}

		#endregion


		#region Key/Value Store
		
		[DllImport("__Internal")]
		private static extern bool _iCloudSynchronize();
	
		/// <summary>
		/// Synchronizes the values to disk. This gets called automatically at launch and at shutdown so you should rarely ever need to call it.
		/// </summary>
		public static bool synchronize()
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudSynchronize();
	
			return false;
		}
		
		
		[DllImport("__Internal")]
		private static extern void _iCloudRemoveObjectForKey( string key );
	
		/// <summary>
		/// Removes the object from iCloud storage
		/// </summary>
		/// <param name="aKey">A key.</param>
		public static void removeObjectForKey( string aKey )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudRemoveObjectForKey( aKey );
		}
	
	
		[DllImport("__Internal")]
		private static extern bool _iCloudHasKey( string key );
	
		/// <summary>
		/// Checks to see if the given key exists
		/// </summary>
		/// <returns><c>true</c>, if key was hased, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		public static bool hasKey( string key )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudHasKey( key );
	
			return false;
		}
	
	
		[DllImport("__Internal")]
		private static extern string _iCloudStringForKey( string key );
	
		/// <summary>
		/// Gets the string value for the key
		/// </summary>
		/// <returns>The for key.</returns>
		/// <param name="key">Key.</param>
		public static string stringForKey( string key )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudStringForKey( key );
	
			return string.Empty;
		}
	
	
		[DllImport("__Internal")]
		private static extern string _iCloudAllKeys();
	
		/// <summary>
		/// Gets all the keys currently stored in iCloud
		/// </summary>
		/// <returns>The keys.</returns>
		public static List<object> allKeys()
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudAllKeys().listFromJson();
	
			return new List<object>();
		}
	
	
		[DllImport("__Internal")]
		private static extern void _iCloudRemoveAll();
	
		/// <summary>
		/// Removes all data stored in the key/value store
		/// </summary>
		public static void removeAll()
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudRemoveAll();
		}
	
	
		[DllImport("__Internal")]
		private static extern void _iCloudSetString( string aString, string aKey );
	
		/// <summary>
		/// Sets the string value for the key
		/// </summary>
		/// <param name="aString">A string.</param>
		/// <param name="aKey">A key.</param>
		public static void setString( string aString, string aKey )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudSetString( aString, aKey );
		}
	
	
		[DllImport("__Internal")]
		private static extern string _iCloudDictionaryForKey( string aKey );
	
		/// <summary>
		/// Gets the dictionary value for the key
		/// </summary>
		/// <returns>The for key.</returns>
		/// <param name="aKey">A key.</param>
		public static IDictionary dictionaryForKey( string aKey )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudDictionaryForKey( aKey ).dictionaryFromJson();
	
			return new Hashtable();
		}
	
	
		[DllImport("__Internal")]
		private static extern void _iCloudSetDictionary( string dict, string aKey );
	
		/// <summary>
		/// Sets the dictionary for the key
		/// </summary>
		/// <param name="dict">Dict.</param>
		/// <param name="aKey">A key.</param>
		public static void setDictionary( string dict, string aKey )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudSetDictionary( dict, aKey );
		}
	
	
		[DllImport("__Internal")]
		private static extern float _iCloudDoubleForKey( string aKey );
	
		/// <summary>
		/// Gets the float value for the key
		/// </summary>
		/// <returns>The for key.</returns>
		/// <param name="aKey">A key.</param>
		public static float doubleForKey( string aKey )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudDoubleForKey( aKey );
	
			return 0;
		}
	
	
		[DllImport("__Internal")]
		private static extern void _iCloudSetDouble( double value, string aKey );
	
		/// <summary>
		/// Sets the float value for the key
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="aKey">A key.</param>
		public static void setDouble( double value, string aKey )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudSetDouble( value, aKey );
		}
		
		
		[DllImport("__Internal")]
		private static extern int _iCloudIntForKey( string aKey );
	
		/// <summary>
		/// Gets the int value for the key
		/// </summary>
		/// <returns>The for key.</returns>
		/// <param name="aKey">A key.</param>
		public static int intForKey( string aKey )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudIntForKey( aKey );
	
			return 0;
		}
	
	
		[DllImport("__Internal")]
		private static extern void _iCloudSetInt( int value, string aKey );
	
		/// <summary>
		/// Sets the int value for the key
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="aKey">A key.</param>
		public static void setInt( int value, string aKey )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudSetInt( value, aKey );
		}
	
	
		[DllImport("__Internal")]
		private static extern bool _iCloudBoolForKey( string aKey );
	
		/// <summary>
		/// Gets the bool value for the key
		/// </summary>
		/// <returns><c>true</c>, if for key was booled, <c>false</c> otherwise.</returns>
		/// <param name="aKey">A key.</param>
		public static bool boolForKey( string aKey )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudBoolForKey( aKey );
	
			return false;
		}
	
	
		[DllImport("__Internal")]
		private static extern void _iCloudSetBool( bool value, string aKey );
	
		/// <summary>
		/// Sets the string value for the key
		/// </summary>
		/// <param name="value">If set to <c>true</c> value.</param>
		/// <param name="aKey">A key.</param>
		public static void setBool( bool value, string aKey )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudSetBool( value, aKey );
		}
		
		#endregion
		
		
		#region Document Store
		
		[DllImport("__Internal")]
		private static extern bool _iCloudDocumentStoreAvailable();
	
		/// <summary>
		/// Checks to see if the iCloud document store is available on the current device
		/// </summary>
		/// <returns><c>true</c>, if store available was documented, <c>false</c> otherwise.</returns>
		public static bool documentStoreAvailable()
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudDocumentStoreAvailable();
	
			return false;
		}
		
		
		[DllImport("__Internal")]
		private static extern string _iCloudDocumentsDirectory();
	
		/// <summary>
		/// Gets the iCloud document store directory
		/// </summary>
		/// <returns>The directory.</returns>
		public static string documentsDirectory()
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudDocumentsDirectory();
	
			return string.Empty;
		}
		
		
		[DllImport("__Internal")]
		private static extern string _iCloudDocumentsDirectoryForContainerId( string containerId );
	
		/// <summary>
		/// Gets the iCloud document store directory for a specific container identifier
		/// </summary>
		/// <returns>The directory for container identifier.</returns>
		/// <param name="containerId">Container identifier.</param>
		public static string documentsDirectoryForContainerId( string containerId )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudDocumentsDirectoryForContainerId( containerId );
	
			return string.Empty;
		}
		
		
		[DllImport("__Internal")]
		private static extern bool _iCloudIsFileInCloud( string file );
	
		/// <summary>
		/// Checks to see if a particular file is stored in iCloud
		/// </summary>
		/// <returns><c>true</c>, if file in cloud was ised, <c>false</c> otherwise.</returns>
		/// <param name="file">File.</param>
		public static bool isFileInCloud( string file )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudIsFileInCloud( file );
	
			return false;
		}
		
		
		[DllImport("__Internal")]
		private static extern bool _iCloudIsFileDownloaded( string file );
	
		/// <summary>
		/// Check to see if a file is downloaded from iCloud. If it is not it will trigger a download.
		/// </summary>
		/// <returns><c>true</c>, if file downloaded was ised, <c>false</c> otherwise.</returns>
		/// <param name="file">File.</param>
		public static bool isFileDownloaded( string file )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudIsFileDownloaded( file );
	
			return false;
		}
		
		
		[DllImport("__Internal")]
		private static extern bool _iCloudAddFile( string file );
	
		/// <summary>
		/// Adds a file to iCloud document storage
		/// </summary>
		/// <returns><c>true</c>, if file was added, <c>false</c> otherwise.</returns>
		/// <param name="file">File.</param>
		public static bool addFile( string file )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudAddFile( file );
	
			return false;
		}
		
		
		[DllImport("__Internal")]
		private static extern void _iCloudEvictFile( string file );
	
		/// <summary>
		/// Evicts the local file but does not remove it from iCloud
		/// </summary>
		/// <param name="file">File.</param>
		public static void evictFile( string file )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudEvictFile( file );
		}
	
		#endregion


		#region CloudKit

		[DllImport("__Internal")]
		private static extern string _iCloudGetDocumentsDirectory();

		/// <summary>
		/// Gets the normal, iOS Documents directory path, the same one usually provided by Application.persistantDataPath
		/// </summary>
		/// <returns>The standard documents directory.</returns>
		public static string getStandardDocumentsDirectory()
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudGetDocumentsDirectory();

			return string.Empty;
		}


		[DllImport("__Internal")]
		private static extern bool _iCloudAreiCloudCredentialsValid();

		/// <summary>
		/// Checks to see if iCloud credentials are valid. This method can be slow so beware!
		/// </summary>
		/// <returns><c>true</c>, if cloud credentials valid was areied, <c>false</c> otherwise.</returns>
		public static bool areiCloudCredentialsValid()
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudAreiCloudCredentialsValid();

			return false;
		}


		[DllImport("__Internal")]
		private static extern bool _iCloudSelectDatabase( string identifier, bool usePrivateDatabase );

		/// <summary>
		/// Selects the database that should be used for all CloudKit actions. The default container will be selected if a null containerIdentifier is passed in.
		/// </summary>
		/// <returns><c>true</c>, if database was selected, <c>false</c> otherwise.</returns>
		/// <param name="usePrivateDatabase">If set to <c>true</c> use private database.</param>
		/// <param name="containerIdentifier">Container identifier.</param>
		public static bool selectDatabase( bool usePrivateDatabase, string containerIdentifier = null )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				return _iCloudSelectDatabase( containerIdentifier, usePrivateDatabase );

			return false;
		}


		[DllImport("__Internal")]
		private static extern void _iCloudSaveRecord( string recordType, string recordId, string title, string path );

		/// <summary>
		/// Saves a record to CloudKit. Results in the cloudKitSaveRecordFailed/SucceededEvent firing.
		/// </summary>
		/// <param name="recordType">Record type.</param>
		/// <param name="recordId">Record identifier.</param>
		/// <param name="title">Title.</param>
		/// <param name="path">Path.</param>
		public static void saveRecord( string recordType, string recordId, string title, string path )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudSaveRecord( recordType, recordId, title, path );
		}


		[DllImport("__Internal")]
		private static extern void _iCloudFetchRecord( string recordType, string recordId );

		/// <summary>
		/// Fetches a record from CloudKit. Results in the cloudKitFetchRecordFailed/SucceededEvent firing.
		/// </summary>
		/// <param name="recordType">Record type.</param>
		/// <param name="recordId">Record identifier.</param>
		public static void fetchRecord( string recordType, string recordId )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudFetchRecord( recordType, recordId );
		}


		[DllImport("__Internal")]
		private static extern void _iCloudUpdateRecord( string recordType, string recordId, string title, string path );

		/// <summary>
		/// Updates a record in CloudKit. Results in the cloudKitUpdateRecordFailed/SucceededEvent firing.
		/// </summary>
		/// <param name="recordType">Record type.</param>
		/// <param name="recordId">Record identifier.</param>
		/// <param name="title">Title.</param>
		/// <param name="path">Path.</param>
		public static void updateRecord( string recordType, string recordId, string title, string path )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudUpdateRecord( recordType, recordId, title, path );
		}


		[DllImport("__Internal")]
		private static extern void _iCloudDeleteRecord( string recordId );

		/// <summary>
		/// Deletes a record from CloudKit. Results in the cloudKitDeleteRecordFailed/SucceededEvent firing.
		/// </summary>
		/// <param name="recordId">Record identifier.</param>
		public static void deleteRecord( string recordId )
		{
			if( Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.tvOS )
				_iCloudDeleteRecord( recordId );
		}

		#endregion
	
	}

}
#endif
