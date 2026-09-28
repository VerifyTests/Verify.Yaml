public static class ModuleInitializer
{
    #region enable

    [ModuleInitializer]
    public static void Init() =>
        VerifyYaml.Initialize();

    #endregion

    [ModuleInitializer]
    public static void InitOther()
    {
        // Date scrubbing depends on the current culture's date format
        var culture = CultureInfo.GetCultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.CurrentCulture = culture;
        VerifierSettings.InitializePlugins();
    }
}