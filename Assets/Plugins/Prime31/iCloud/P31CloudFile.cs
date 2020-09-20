using UnityEngine;
using System.Collections;
using System.IO;


#if UNITY_IOS || UNITY_TVOS
namespace Prime31
{
	public class P31CloudFile
	{
		private static LocalFileManager _file;


		static P31CloudFile()
		{
			if( P31Prefs.iCloudDocumentStoreAvailable )
				_file = new CloudFileManager( iCloudBinding.documentsDirectory() );
			else
				_file = new LocalFileManager( Application.persistentDataPath );
		}


		// Checks to see if a file exists on the local device
		public static bool exists( string file )
		{
			return _file.exists( file );
		}


		// Deletes the file
		public static bool delete( string file )
		{
			return _file.delete( file );
		}


		// Writes the file. If iCloud is available it will automatically be written there as well
		public static bool writeAllBytes( string file, byte[] bytes )
		{
			return _file.writeAllBytes( file, bytes );
		}


		// Writes all the text to a file. If iCloud is available it will automatically be written there as well
		public static bool writeAllText( string file, string text )
		{
			return _file.writeAllText( file, text );
		}


		// Reads all the lines from a file
		public static string[] readAllLines( string file )
		{
			return _file.readAllLines( file );
		}


		// Reads all the bytes from a file
		public static byte[] readAllBytes( string file )
		{
			return _file.readAllBytes( file );
		}


		// Lists all the locally available files
		public static string[] listAllFiles()
		{
			return _file.listAllFiles();
		}



		#region File facades for standard file system and iCloud

		internal class LocalFileManager
		{
			protected string basePath;


			public LocalFileManager( string basePath )
			{
				this.basePath = basePath;
			}


			public bool exists( string file )
			{
				return File.Exists( Path.Combine( basePath, file ) );
			}


			public bool delete( string file )
			{
				File.Delete( Path.Combine( basePath, file ) );
				return true;
			}


			public virtual bool writeAllBytes( string file, byte[] bytes )
			{
				File.WriteAllBytes( Path.Combine( basePath, file ), bytes );
				return true;
			}


			public virtual bool writeAllText( string file, string text )
			{
				File.WriteAllText( Path.Combine( basePath, file ), text );
				return true;
			}


			public byte[] readAllBytes( string file )
			{
				var path = Path.Combine( basePath, file );
				if( File.Exists( path ) )
					return File.ReadAllBytes( path );
				return null;
			}


			public string[] readAllLines( string file )
			{
				var path = Path.Combine( basePath, file );
				if( File.Exists( path ) )
					return File.ReadAllLines( path );
				return null;
			}


			public string[] listAllFiles()
			{
				return Directory.GetFiles( basePath );
			}
		}


		internal class CloudFileManager : LocalFileManager
		{
			public CloudFileManager( string basePath ) : base( basePath )
			{}


			public override bool writeAllBytes( string file, byte[] bytes )
			{
				// is the file in iCloud already? If not, we will need to add it later
				var fileExists = exists( file );

				if( !fileExists )
				{
					// we write to the Documents directory first always
					File.WriteAllBytes( Path.Combine( Application.persistentDataPath, file ), bytes );

					// now we add it to iCloud
					return iCloudBinding.addFile( file );
				}

				// pass this on to the super class because we are doing a direct write
				return base.writeAllBytes( file, bytes );
			}


			public override bool writeAllText( string file, string text )
			{
				// is the file in iCloud already? If not, we will need to add it later
				var fileExists = exists( file );

				if( !fileExists )
				{
					// we write to the Documents directory first always
					File.WriteAllText( Path.Combine( Application.persistentDataPath, file ), text );

					// now we add it to iCloud
					return iCloudBinding.addFile( file );
				}

				// pass this on to the super class because we are doing a direct write
				return base.writeAllText( file, text );
			}
		}

		#endregion

	}
}
#endif