using System;
using System.Configuration;
using System.Reflection;

namespace openSourceC.StandardLibrary.Configuration
{
	/// <summary>
	///		Summary description for ConfigurationSectionManager.
	/// </summary>
	public static class ConfigurationSectionManager
	{
		private static bool? _isRunningUnderIIS;

		private static System.Configuration.Configuration _configuration = null;
		private static readonly object _configurationLock = new object();


		#region Public Properties

		/// <summary>
		///		Gets the <see cref="T:System.Configuration.Configuration"/> of the currently running
		///		application.
		///	</summary>
		public static System.Configuration.Configuration ApplicationConfiguration
		{
			get
			{
				if (_configuration == null)
				{
					lock (_configurationLock)
					{
						if (_configuration == null)
						{
							try
							{
								if (IsRunningUnderIIS)
								{
									Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

									foreach (Assembly assembly in loadedAssemblies)
									{
										if (!"System.Web".Equals(assembly.GetName().Name, StringComparison.InvariantCultureIgnoreCase)) { continue; }

										try
										{
											Type type = assembly.GetType("System.Web.Configuration.WebConfigurationManager", true, true);
											MethodInfo method = type.GetMethod("OpenWebConfiguration", new Type[] { typeof(string) });

											_configuration = (System.Configuration.Configuration)method.Invoke(null, new object[] { "/" });
										}
										catch (Exception) { }

										break;
									}
								}

								if (_configuration == null)
								{
									_configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
								}
							}
							catch (InvalidOperationException) { throw; }
							catch (Exception ex)
							{
								throw new ConfigurationErrorsException(string.Format("Unable to process configuration file. (IIS: {0})", IsRunningUnderIIS), ex);
							}
						}
					}
				}

				return _configuration;
			}
		}

		/// <summary>
		///		Gets or sets a value indicating that the current application is running under IIS.
		/// </summary>
		public static bool IsRunningUnderIIS
		{
			get
			{
				if (!_isRunningUnderIIS.HasValue)
				{
					throw new InvalidOperationException("Unknown configuration environment.  The static property 'IsRunningUnderIIS' must be set before first use.");
				}

				return _isRunningUnderIIS.Value;
			}

			set { _isRunningUnderIIS = value; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		///		Gets the specified configuration section from the configuration file.
		/// </summary>
		/// <typeparam name="TConfigurationSection">The type of the configuration section.</typeparam>
		/// <param name="allowAssignableFrom"><b>true</b> to allow the matching of configuration
		///		sections that can be assigned to an instance of <typeparamref name="TConfigurationSection"/>,
		///		or <b>false</b> for an exact match.  (Default: false)</param>
		/// <returns>
		///		A <see cref="T:TConfigurationSection"/> object.
		/// </returns>
		public static TConfigurationSection GetConfigurationSection<TConfigurationSection>(bool allowAssignableFrom = false)
			where TConfigurationSection : ConfigurationSection
		{
			return GetConfigurationSection<TConfigurationSection>(ApplicationConfiguration.RootSectionGroup, null, allowAssignableFrom);
		}

		/// <summary>
		///		Gets the specified configuration section group from the configuration file.
		/// </summary>
		/// <typeparam name="TConfigurationSectionGroup">The type of the configuration section group.</typeparam>
		/// <param name="allowAssignableFrom"><b>true</b> to allow the matching of configuration
		///		sections that can be assigned to an instance of <typeparamref name="TConfigurationSectionGroup"/>,
		///		or <b>false</b> for an exact match.  (Default: false)</param>
		/// <returns>
		///		A <see cref="T:TConfigurationSectionGroup"/> object.
		/// </returns>
		public static TConfigurationSectionGroup GetConfigurationSectionGroup<TConfigurationSectionGroup>(bool allowAssignableFrom = false)
			where TConfigurationSectionGroup : ConfigurationSectionGroup
		{
			return GetConfigurationSectionGroup<TConfigurationSectionGroup>(ApplicationConfiguration.RootSectionGroup, null, allowAssignableFrom);
		}

		#endregion

		#region Private Methods

		private static TConfigurationSection GetConfigurationSection<TConfigurationSection>(ConfigurationSectionGroup sectionGroup, TConfigurationSection configSection, bool allowAssignableFrom)
			where TConfigurationSection : ConfigurationSection
		{
			foreach (ConfigurationSection section in sectionGroup.Sections)
			{
				if (
					allowAssignableFrom
					? typeof(TConfigurationSection).IsAssignableFrom(section.GetType())
					: typeof(TConfigurationSection).Equals(section.GetType())
				)
				{
					if (configSection != null)
					{
						throw new ApplicationException("Multiple configuration sections found.");
					}

					configSection = (TConfigurationSection)section;
				}
			}

			foreach (ConfigurationSectionGroup subsectionGroup in sectionGroup.SectionGroups)
			{
				configSection = GetConfigurationSection(subsectionGroup, configSection, allowAssignableFrom);
			}

			return configSection;
		}

		private static TConfigurationSectionGroup GetConfigurationSectionGroup<TConfigurationSectionGroup>(ConfigurationSectionGroup sectionGroup, TConfigurationSectionGroup configSectionGroup, bool allowAssignableFrom)
			where TConfigurationSectionGroup : ConfigurationSectionGroup
		{
			foreach (ConfigurationSectionGroup subsectionGroup in sectionGroup.SectionGroups)
			{
				if (
					allowAssignableFrom
					? typeof(TConfigurationSectionGroup).IsAssignableFrom(subsectionGroup.GetType())
					: typeof(TConfigurationSectionGroup).Equals(subsectionGroup.GetType())
				)
				{
					if (configSectionGroup != null)
					{
						throw new ApplicationException("Multiple configuration section groups found.");
					}

					configSectionGroup = (TConfigurationSectionGroup)subsectionGroup;
				}

				configSectionGroup = GetConfigurationSectionGroup(subsectionGroup, configSectionGroup, allowAssignableFrom);
			}

			return configSectionGroup;
		}

		#endregion
	}
}
