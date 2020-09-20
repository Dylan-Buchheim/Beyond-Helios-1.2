#if ( UNITY_IOS || UNITY_TVOS ) && !UNITY_EDITOR
#define USE_NATIVE_API
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;



#pragma warning disable 0162, 0649

namespace Prime31
{
	public class P31Prefs
	{
		/// <summary>
		/// indicates how data should be retrieved when iCloud is available. If false, any calls to getX will
		/// always be passed to iCloud. If true, any calls to getX will only be passed to iCloud if the key
		/// exists in iCloud. If the key does not exist in iCloud the call will fallback to PlayerPrefs.
		/// </summary>
		public static bool shouldOnlyGetFromCloudWhenKeyExists = false;

		/// <summary>
		/// gets whether the iCloud document store is available
		/// </summary>
		/// <value><c>true</c> if iCloud document store available; otherwise, <c>false</c>.</value>
		public static bool iCloudDocumentStoreAvailable { get { return _iCloudDocumentStoreAvailable; } }
		static bool _iCloudDocumentStoreAvailable;

		/// <summary>
		/// gets whether iCloud is available
		/// </summary>
		/// <value><c>true</c> if iCloud is available; otherwise, <c>false</c>.</value>
		public static bool iCloudIsAvailable { get { return _iCloudIsAvailable; } }
		static bool _iCloudIsAvailable;


#if USE_NATIVE_API
		static P31Prefs()
		{
			// listen for a ubiquity identity change so we stay up to date with if iCloud is enabled or not
			iCloudManager.ubiquityIdentityDidChangeEvent += ubiquityIdentityDidChange;
			ubiquityIdentityDidChange();
		}


		static void ubiquityIdentityDidChange()
		{
			_iCloudDocumentStoreAvailable = iCloudBinding.documentStoreAvailable();
			#if !UNITY_TVOS
			_iCloudIsAvailable = !string.IsNullOrEmpty( iCloudBinding.getUbiquityIdentityToken() );
			#else
			_iCloudIsAvailable = true;
			#endif
		}
#endif


		public static bool synchronize()
		{
#if USE_NATIVE_API
			return iCloudBinding.synchronize();
#endif
			PlayerPrefs.Save();
			return true;
		}


		public static bool hasKey( string key )
		{
#if USE_NATIVE_API
			if( _iCloudIsAvailable )
				return iCloudBinding.hasKey( key );
#endif
			return PlayerPrefs.HasKey( key );
		}


		public static List<object> allKeys()
		{
#if USE_NATIVE_API
			if( _iCloudIsAvailable )
				return iCloudBinding.allKeys();
#endif
			return new List<object>();
		}


		public static void removeObjectForKey( string key )
		{
#if USE_NATIVE_API
			iCloudBinding.removeObjectForKey( key );
#endif
			PlayerPrefs.DeleteKey( key );
		}


		public static void removeAll()
		{
#if USE_NATIVE_API
			iCloudBinding.removeAll();
#endif
			PlayerPrefs.DeleteAll();
		}


		public static void setInt( string key, int val )
		{
#if USE_NATIVE_API
			iCloudBinding.setInt( val, key );
#endif
			PlayerPrefs.SetInt( key, val );
		}


		public static int getInt( string key )
		{
#if USE_NATIVE_API
			if( _iCloudIsAvailable && ( !shouldOnlyGetFromCloudWhenKeyExists || hasKey( key ) ) )
				return iCloudBinding.intForKey( key );
#endif
			return PlayerPrefs.GetInt( key );
		}


		public static void setFloat( string key, float val )
		{
#if USE_NATIVE_API
			iCloudBinding.setDouble( val, key );
#endif
			PlayerPrefs.SetFloat( key, val );
		}


		public static float getFloat( string key )
		{
#if USE_NATIVE_API
			if( _iCloudIsAvailable && ( !shouldOnlyGetFromCloudWhenKeyExists || hasKey( key ) ) )
				return iCloudBinding.doubleForKey( key );
#endif
			return PlayerPrefs.GetFloat( key );
		}


		public static void setString( string key, string val )
		{
#if USE_NATIVE_API
			iCloudBinding.setString( val, key );
#endif
			PlayerPrefs.SetString( key, val );
		}


		public static string getString( string key )
		{
#if USE_NATIVE_API
			if( _iCloudIsAvailable && ( !shouldOnlyGetFromCloudWhenKeyExists || hasKey( key ) ) )
				return iCloudBinding.stringForKey( key );
#endif
			return PlayerPrefs.GetString( key );
		}


		public static void setBool( string key, bool val )
		{
#if USE_NATIVE_API
			iCloudBinding.setBool( val, key );
#endif
			PlayerPrefs.SetInt( key, val ? 1 : 0 );
		}


		public static bool getBool( string key )
		{
#if USE_NATIVE_API
			if( _iCloudIsAvailable && ( !shouldOnlyGetFromCloudWhenKeyExists || hasKey( key ) ) )
				return iCloudBinding.boolForKey( key );
#endif
			return PlayerPrefs.GetInt( key, 0 ) == 1;
		}


		public static void setDictionary( string key, Dictionary<string,object> val )
		{
			var json = Prime31.Json.encode( val );

#if USE_NATIVE_API
			iCloudBinding.setDictionary( json, key );
#endif
			PlayerPrefs.SetString( key, json );
		}


		public static IDictionary getDictionary( string key )
		{
#if USE_NATIVE_API
			if( _iCloudIsAvailable && ( !shouldOnlyGetFromCloudWhenKeyExists || hasKey( key ) ) )
				return iCloudBinding.dictionaryForKey( key );
#endif
			var str = PlayerPrefs.GetString( key );
			return str.dictionaryFromJson();
		}

	}

}
#pragma warning restore 0162, 0649
