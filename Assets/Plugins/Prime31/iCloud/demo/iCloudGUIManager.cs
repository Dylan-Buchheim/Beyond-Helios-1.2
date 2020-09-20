using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;
using System.IO;



namespace Prime31
{
	public class iCloudGUIManager : MonoBehaviourGUI
	{
	#if UNITY_IOS || UNITY_TVOS
		string _filename = "myCloudFile.txt";
		string _cloudKitLastRecordId;
		string _cloudKitLastFilename;

		string _keyForInt = "theInt";
		string _keyForString = "theString";
		string _keyForBool = "theBool";
		string _keyForFloat = "theFloat";
		string _keyForDict = "theDict";


		void OnGUI()
		{
			beginColumn();


			if( button( "Synchronize" ) )
			{
				// Note: synchronize is called automatically for you when iCloud initializes and your app closes so in practice you will not need to call it
				var didSync = P31Prefs.synchronize();
				Debug.Log( "did synchronize: " + didSync );
			}


			if( button( "Get the UbiquityIdentityToken" ) )
			{
				var token = iCloudBinding.getUbiquityIdentityToken();
				Debug.Log( "ubiquityIdentityToken: " + token );
			}


			if( button( "Was iCloud Enabled Last Launch?" ) )
			{
				Debug.Log( "was enabled last launch? " + iCloudBinding.wasICloudEnabledLastLaunch() );
			}


			if( button( "Get Custom iCloud Container" ) )
			{
				var container = iCloudBinding.documentsDirectoryForContainerId( "iCloud.com.prime31.shared-container" );
				Debug.Log( "custom iCloud container: " + container );
			}


			if( button( "Set Int 29" ) )
			{
				P31Prefs.setInt( _keyForInt, 29 );
				Debug.Log( "int: " + P31Prefs.getInt( _keyForInt ) );
			}


			if( button( "Set string 'word'" ) )
			{
				P31Prefs.setString( _keyForString, "word" );
				Debug.Log( "string: " + P31Prefs.getString( _keyForString ) );
			}


			if( button( "Set Bool" ) )
			{
				P31Prefs.setBool( _keyForBool, true );
				Debug.Log( "bool: " + P31Prefs.getBool( _keyForBool ) );
			}


			if( button( "Set Float 13.68" ) )
			{
				P31Prefs.setFloat( _keyForFloat, 13.68f );
				Debug.Log( "float: " + P31Prefs.getFloat( _keyForFloat ) );
			}


			if( button( "Set Dictionary" ) )
			{
				var dict = new Dictionary<string,object>();
				dict.Add( "aFloat", 25.5f );
				dict.Add( "aString", "dogma" );
				dict.Add( "anInt", 16 );
				P31Prefs.setDictionary( _keyForDict, dict );
				Prime31.Utils.logObject( P31Prefs.getDictionary( _keyForDict ) );
			}


			if( button( "Get All" ) )
			{
				if( P31Prefs.hasKey( _keyForInt ) )
					Debug.Log( "int: " + P31Prefs.getInt( _keyForInt ) );

				if( P31Prefs.hasKey( _keyForString ) )
					Debug.Log( "string: " + P31Prefs.getString( _keyForString ) );

				if( P31Prefs.hasKey( _keyForBool ) )
					Debug.Log( "bool: " + P31Prefs.getBool( _keyForBool ) );

				if( P31Prefs.hasKey( _keyForDict ) )
					Prime31.Utils.logObject( P31Prefs.getDictionary( _keyForDict ) );

				if( P31Prefs.hasKey( _keyForFloat ) )
					Debug.Log( "float: " + P31Prefs.getFloat( _keyForFloat ) );
			}


			if( button( "Remove All Values" ) )
			{
				P31Prefs.removeAll();
			}


			endColumn( true );

			if( toggleButtonState( "Show CloudKit Buttons" ) )
				documentStoreButtons();
			else
				cloudKitButtons();

			GUILayout.Space( 30 );
			toggleButton( "Show CloudKit Buttons", "Show Document Store Buttons" );

			endColumn();
		}


		void documentStoreButtons()
		{
			if( button( "Is Document Store Available" ) )
			{
				Debug.Log( "Is document store available: " + P31Prefs.iCloudDocumentStoreAvailable );
			}


			if( button( "Is File in iCloud?" ) )
			{
				Debug.Log( "Is file in iCloud: " + iCloudBinding.isFileInCloud( _filename ) );
			}


			if( button( "Is File Downloaded?" ) )
			{
				Debug.Log( "Is file downloaded: " + iCloudBinding.isFileDownloaded( _filename ) );
			}


			if( button( "Save File to iCloud" ) )
			{
				// make up a file name
				_filename = string.Format( "myCloudFile{0}.txt", Random.Range( 0, 10000 ) );

				var didSave = P31CloudFile.writeAllText( _filename, "going to write some text" );
				Debug.Log( "Did write file to iCloud: " + didSave );
			}


			if( button( "Modify File" ) )
			{
				var didSave = P31CloudFile.writeAllText( _filename, "changed up the text and now its this" );
				Debug.Log( "Did write file to iCloud: " + didSave );
			}


			if( button( "Get Contents of File" ) )
			{
				var lines = P31CloudFile.readAllLines( _filename );
				Debug.Log( "File contents: " + string.Join( "", lines ) );
			}


			if( button( "Evict File" ) )
			{
				iCloudBinding.evictFile( _filename );
			}
		}


		void cloudKitButtons()
		{
			if( button( "Print Documents Folder Location" ) )
			{
				Debug.Log( "standard documents folder: " + iCloudBinding.getStandardDocumentsDirectory() );
			}


			if( button( "Check iCloud Credentials" ) )
			{
				Debug.Log( "are iCloud credentials valid: " + iCloudBinding.areiCloudCredentialsValid() );
			}


			if( button( "Select Private Database" ) )
			{
				iCloudBinding.selectDatabase( true );
			}


			if( button( "Select Public Database" ) )
			{
				iCloudBinding.selectDatabase( false );
			}


			if( button( "Save File to CloudKit" ) )
			{
				writeTextFileForCloudKitDemo( "Some demo text" );
				iCloudBinding.saveRecord( "FileStore", _cloudKitLastRecordId, "Extra string data", _cloudKitLastFilename );
			}


			if( _cloudKitLastRecordId != null && button( "Fetch File from CloudKit" ) )
			{
				iCloudBinding.fetchRecord( "FileStore", _cloudKitLastRecordId );
			}


			if( _cloudKitLastRecordId != null && button( "Update File in CloudKit" ) )
			{
				var previousCloudKitRecordId = _cloudKitLastRecordId;
				writeTextFileForCloudKitDemo( "Updated demo text" );
				_cloudKitLastRecordId = previousCloudKitRecordId;
				iCloudBinding.updateRecord( "FileStore", _cloudKitLastRecordId, "Updated string data", _cloudKitLastFilename );
			}


			if( _cloudKitLastRecordId != null && button( "Delete File from CloudKit" ) )
			{
				iCloudBinding.deleteRecord( _cloudKitLastRecordId );
				_cloudKitLastRecordId = null;
			}
		}


		void writeTextFileForCloudKitDemo( string text )
		{
			_cloudKitLastRecordId = "record-id-" + Random.Range( 0, 100 );
			_cloudKitLastFilename = Path.Combine( iCloudBinding.getStandardDocumentsDirectory(), _cloudKitLastRecordId );
			File.WriteAllText( _cloudKitLastFilename, text );
		}

#endif
	}

}
