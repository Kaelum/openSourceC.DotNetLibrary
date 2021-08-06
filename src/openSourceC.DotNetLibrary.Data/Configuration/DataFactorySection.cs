using System;
using System.Configuration;

namespace openSourceC.DotNetLibrary.Configuration
{
	/// <summary>
	///		Summary description for DataFactorySection.
	/// </summary>
	public class DataFactorySection : DbFactorySectionBase
	{
		private static DataFactorySection _configSection = null;
		private static object _configSectionLock = new object();


		#region Instance

		/// <summary>
		///		Gets the singleton instance of <see cref="DataFactorySection"/>.
		/// </summary>
		public static DataFactorySection Instance
		{
			get
			{
				if (_configSection == null)
				{
					lock (_configSectionLock)
					{
						if (_configSection == null)
						{
							_configSection = ConfigurationSectionManager.GetConfigurationSection<DataFactorySection>(false);

							if (_configSection == null)
							{
								throw new ConfigurationErrorsException($"Configuration section for type=\"{typeof(DataFactorySection).FullName}\" not found.");
							}
						}
					}
				}

				return _configSection;
			}
		}

		#endregion
	}
}
