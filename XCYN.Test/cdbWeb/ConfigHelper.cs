using System.Configuration;

/// <summary>
/// 配置文件辅助类
/// </summary>
public class ConfigHelper
{
    /// <summary>
    /// 获得指定配置节点的值
    /// 节点不存在时返回 null
    /// </summary>
    public static string ReadAppConfig(string strKey)
    {
        if (ConfigurationManager.AppSettings[strKey] == null)
            return null;
        return ConfigurationManager.AppSettings[strKey].ToString();
    }

    /// <summary>
    /// 读 ConnectionStrings 节点 ConnectionName 的连接字符串
    /// 节点不存在时返回 null
    /// </summary>
    public static string ReadConnectionStringConfig(string ConnectionName)
    {
        if (ConfigurationManager.ConnectionStrings[ConnectionName] == null)
            return null;
        return ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString.ToString();
    }

    /// <summary>
    /// 修改指定配置节点的值
    /// 节点不存在则添加
    /// </summary>
    public static void WriteAppConfig(string newKey, string newValue)
    {
        // 打开配置文件
        //Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        if (ConfigurationManager.AppSettings[newKey] != null)
            config.AppSettings.Settings.Remove(newKey);
        // 添加新的键-值对
        config.AppSettings.Settings.Add(newKey, newValue);
        // 保存对配置文件的更改
        config.Save(ConfigurationSaveMode.Minimal);
        ConfigurationManager.RefreshSection("appSettings");
    }

    /// <summary>
    /// 写 ConnectionStrings 节点 ConnectionName 的连接字符串
    /// 节点不存在则添加
    /// </summary>
    public static void WriteConnectionStringConfig(string newName, string newConString, string newProvideName)
    {
        // 打开配置文件
        //Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        if (ConfigurationManager.ConnectionStrings[newName] != null)
            config.ConnectionStrings.ConnectionStrings.Remove(newName);
        // 添加新的连接字符串
        config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(newName, newConString, newProvideName));
        // 保存对配置文件的更改
        config.Save(ConfigurationSaveMode.Minimal);
        ConfigurationManager.RefreshSection("connectionStrings");
    }

    /// <summary>
    /// 重新读取配置文件
    /// </summary>
    public static void RefreshConfigs()
    {
        ConfigurationManager.RefreshSection("connectionStrings");
        ConfigurationManager.RefreshSection("appSettings");
    }
}